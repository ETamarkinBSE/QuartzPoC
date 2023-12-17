using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Temporalio.Activities;
using Temporalio.Exceptions;

namespace TemporalSampleApp
{
    public record Purchase(string ItemID, string UserID);
    public class PurchaseActivities
    {
        private readonly HttpClient client = new();

        [Activity]
        public async Task DoPurchaseAsync(Purchase purchase)
        {
            using var resp = await client.PostAsJsonAsync(
                "https://api.example.com/purchase",
                purchase,
                ActivityExecutionContext.Current.CancellationToken);

            // Make sure we succeeded
            try
            {
                resp.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e) when (resp.StatusCode < HttpStatusCode.InternalServerError)
            {
                // We don't want to retry 4xx status codes, only 5xx status codes
                throw new ApplicationFailureException("API returned error", e, nonRetryable: true);
            }
        }
    }
}
