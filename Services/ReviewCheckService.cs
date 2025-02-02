using Api.Data;
using Microsoft.EntityFrameworkCore;

public class ReviewCheckService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ReviewCheckService> _logger;

    public ReviewCheckService(IServiceScopeFactory serviceScopeFactory, ILogger<ReviewCheckService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var currentTime = DateTime.Now;
            var nextMidnight = currentTime.Date.AddDays(1); // Calculate the next midnight (00:00)
            var timeToMidnight = nextMidnight - currentTime;

            // Wait until midnight
            await Task.Delay(timeToMidnight, stoppingToken);

            // Run the check after midnight
            await CheckAndUpdateReviewAsync(stoppingToken);
        }
    }

    private async Task CheckAndUpdateReviewAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            //var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //// Get the current date
            //var currentDate = DateTime.Now;

            //// Query records where DataOperation date is older than 1 day (you can adjust the condition)
            //var recordsToUpdate = dbContext.YourEntities
            //    .Where(r => EF.Functions.DateDiffDay(r.DataOperation, currentDate) == 1) // Check difference of 1 day
            //    .ToList();
                        

            //foreach (var record in recordsToUpdate)
            //{
            //    record.Status = "Reviewed"; // Update the status or perform any update

            //    // Optionally: update the DataOperation to the current date or any other update logic
            //    record.DataOperation = currentDate;

            //    // Save changes
            //    dbContext.Update(record);
            //}

            //await dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}
