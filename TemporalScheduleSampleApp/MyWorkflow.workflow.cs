using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalio.Workflows;

namespace TemporalScheduleSampleApp
{

    [Workflow]
    public class MyWorkflow
    {
        [WorkflowRun]
        public async Task RunAsync(string text)
        {
            await Workflow.ExecuteActivityAsync(
                () => MyActivities.AddReminderToDatabase(text),
                new()
                {
                    StartToCloseTimeout = TimeSpan.FromMinutes(5),
                });

            await Workflow.ExecuteActivityAsync(
                () => MyActivities.NotifyUserAsync(text),
                new()
                {
                    StartToCloseTimeout = TimeSpan.FromMinutes(5),
                });
        }
    }
}
