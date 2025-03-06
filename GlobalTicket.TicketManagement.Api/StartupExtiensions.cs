using GlobalTicket.TicketManagement.Application;
using GlobalTicket.TicketManagement.Infrastructure;
using GlobalTicket.TicketManagement.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GlobalTicket.TicketManagement.Api
{
    public static class StartupExtiensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAplicationService();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin
                ().AllowAnyHeader().AllowAnyMethod());
            });
            return builder.Build();


        }

        public static WebApplication ConfigurePipeline(this WebApplication app) 
        { 
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("Open");

            app.MapControllers();

            return app;
        }

        public static async Task ResetDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<GloboTicketDbContext>();
                if (context == null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                //add logging here later on
            }

    }
}
