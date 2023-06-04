using Microsoft.EntityFrameworkCore;
using testApp.Context;

namespace testApp
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add EF Core services.
            services.AddDbContext<TestContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("testApp"));
            });

            // Add other services here.
        }

        public void Configure(IApplicationBuilder app)
        {
            // Use MVC to handle requests.
            app.UseMvc();
        }
    }
}
