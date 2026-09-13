using Microsoft.EntityFrameworkCore;
using Mostra.Application.Interfaces;
using Mostra.Infrastructure.Persistence;

public class HardDeleteExpiredEntitiesJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval = TimeSpan.FromDays(1);

    public HardDeleteExpiredEntitiesJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MostraContext>();
                var imageStorage = scope.ServiceProvider.GetRequiredService<IImageStorageService>();

                var cutoff = DateTime.UtcNow.AddDays(-30);

                var expiredProducts = await context.Products
                    .IgnoreQueryFilters()
                    .Where(p => p.IsDeleted && p.DeletedAt < cutoff)
                    .ToListAsync(stoppingToken);

                foreach (var product in expiredProducts)
                {
                    if (!string.IsNullOrEmpty(product.ImagePublicId))
                    {
                        await imageStorage.DeleteAsync(product.ImagePublicId, stoppingToken);
                    }
                    context.Products.Remove(product);
                }

                await context.SaveChangesAsync(stoppingToken);
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}