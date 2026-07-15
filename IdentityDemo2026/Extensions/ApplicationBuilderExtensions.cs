using IdentityDemo2026.Data;
using System.Runtime.InteropServices;

namespace IdentityDemo2026.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();

                try
                {
                    await SeedData.Init(context, services);
                }
                catch (Exception)
                {

                    throw;
                }
                return app; 
            }
        }
    }
}
