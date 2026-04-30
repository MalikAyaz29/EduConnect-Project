using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using EduConnect.Interfaces;
using EduConnect.Models;
using EduConnect.Services;

namespace EduConnect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddSingleton<IStudentService, StudentService>();
            builder.Services.AddSingleton<ICourseService, CourseService>();
            builder.Services.AddSingleton<IGradeService, GradeService>();
            builder.Services.AddSingleton<INotificationService, NotificationService>();
            
            // Register AuthStateService and map IAuthService to it as Scoped (per user session)
            builder.Services.AddScoped<AuthStateService>();
            builder.Services.AddScoped<IAuthService>(sp => sp.GetRequiredService<AuthStateService>());

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var facultyRepo = scope.ServiceProvider.GetRequiredService<IRepository<Faculty>>();
                var courseRepo = scope.ServiceProvider.GetRequiredService<IRepository<Course>>();
                var adminRepo = scope.ServiceProvider.GetRequiredService<IRepository<Admin>>();
                DataSeeder.SeedData(facultyRepo, courseRepo, adminRepo);
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
