using Temporalio.Workflows;

namespace TemporalSampleApp
{
    public enum PurchaseStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    [Workflow]
    public class OneClickBuyWorkflow
    {
        private PurchaseStatus currentStatus = PurchaseStatus.Pending;
        private Purchase? currentPurchase;

        [WorkflowRun]
        public async Task<PurchaseStatus> RunAsync(Purchase purchase)
        {
            currentPurchase = purchase;

            // Give user 10 seconds to cancel or update before we send it through
            try
            {
                await Workflow.DelayAsync(TimeSpan.FromSeconds(10));
            }
            catch (TaskCanceledException)
            {
                currentStatus = PurchaseStatus.Cancelled;
                return currentStatus;
            }

            // Update the status, perform the purchase, update the status again
            currentStatus = PurchaseStatus.Confirmed;
            await Workflow.ExecuteActivityAsync(
                (PurchaseActivities act) => act.DoPurchaseAsync(currentPurchase!),
                new() { ScheduleToCloseTimeout = TimeSpan.FromMinutes(2) });
            currentStatus = PurchaseStatus.Completed;
            return currentStatus;
        }

        [WorkflowSignal]
        public async Task UpdatePurchaseAsync(Purchase purchase) => currentPurchase = purchase;

        [WorkflowQuery]
        public PurchaseStatus CurrentStatus() => currentStatus;
    }
}
