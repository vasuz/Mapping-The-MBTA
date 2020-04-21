using FluentScheduler;
using MappingTheMBTA.Models;
using MappingTheMBTA.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MappingTheMBTA
{
    public class Scheduler : Registry
    {
        public Scheduler()
        {
            // Populate the routes model, wait for it
            Route.Populate().Wait();
            // Populate the schedule for the first time, wait for it
            Sources.Scheduled.Update().Wait();

            // Run the prediction updater now & every 30 seconds
            Schedule(async () => await Predicted.Update()).ToRunNow().AndEvery(30).Seconds();

            // Run the data updater now & every 3 seconds
            Schedule(async () => await Sources.Actual.Update()).ToRunNow().AndEvery(3).Seconds();

            // Run the schedule updater every day at 4AM
            Schedule<FillJob>().ToRunEvery(1).Days().At(4, 00);
        }

        public class FillJob : IJob
        {
            // fill the schedule and save it to the database
            public async void Execute()
            {
                await Sources.Scheduled.Update();
                Database.Capture();
            }
        }
    }
}
