using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MorrowSolutions.Web.EFCore.Example
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();

#warning Tutorial: Registering Entity Framework
         services.AddEntityFrameworkSqlServer()
                 .AddDbContext<DatabaseContext>(options =>
                 {
                    options.UseSqlServer(Configuration["ConnectionString"]);
                 },
                 ServiceLifetime.Scoped
               );
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });

#warning Tutorial: Running Database Migrations when the application starts
#error Tutorial: Add your SQL Connection String within the appsettings.json file first!
         using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
         {
            var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();
            var pendingMigrations = dbContext.Database.GetPendingMigrations().Any();
            if (pendingMigrations)
            {
               dbContext.Database.Migrate();
            }
         }
      }
   }
}
