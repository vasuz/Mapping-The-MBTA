using FluentScheduler;
using MappingTheMBTA.Models;
using MappingTheMBTA.Data;

namespace MappingTheMBTA
{
    public class Scheduler : Registry
    {
        public Scheduler()
        {
            // Populate the routes model, wait for it
            Route.Populate().Wait();

            // Populate the current dataset, wait for it
            Sources.Populate().Wait();

            // Run the schedule updater every day at 3AM Eastern Time
            Schedule(async () => await Sources.Populate()).ToRunEvery(1).Days().At(3, 00);

            // Run the prediction updater now & every 30 seconds
            Schedule(async () => await Predicted.Update()).ToRunNow().AndEvery(30).Seconds();

            // Run the data updater now & every 3 seconds
            Schedule(async () => await Sources.Actual.Update()).ToRunNow().AndEvery(3).Seconds();

            // Save data to db every 30 seconds
            Schedule(() => Database.Capture()).ToRunEvery(30).Seconds();
        }
    }
}