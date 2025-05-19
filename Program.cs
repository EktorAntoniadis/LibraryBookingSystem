using LibraryBookingSystem.Models;
using LibraryBookingSystem.Repositories;
using LibraryBookingSystem.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<LibraryManagementDbContext>(options =>
                options.UseMySql("server=127.0.0.1;uid=root;database=librarymanagementdb",
                ServerVersion.AutoDetect("server=127.0.0.1;uid=root;database=librarymanagementdb")));

            //Στο ASP.NET Core(συμπεριλαμβανομένου του .NET 8), τα tokens κατά της παραχάραξης (anti-forgery) είναι ήδη ενεργοποιημένα για τις σελίδες Razor και MVC. Το μόνο που χρειάζεται είναι να ενεργοποιηθεί το στοιχείο 
            // AutoValidateAntiforgeryToken και να μπει το @Html.AntiForgeryToken() σε όλες τις φόρμες.
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.ConfigureFilter(new AutoValidateAntiforgeryTokenAttribute());
            });

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

            builder.Services.AddAuthentication("LibraryBookingSystemScheme")
                .AddCookie("LibraryBookingSystemScheme", options =>
                {
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath = "/Error";
                });

            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseSession();
            app.MapRazorPages();

            app.Use(async (context, next) =>
            {
                // Πολιτική αποστολής του "Referrer" header από τον browser.
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");

                // Αποφυγή παγίδευσης κλικ
                context.Response.Headers.Add("X-Frame-Options", "DENY");                            

                // Χρήση Content Security Policy (CSP) Καθαρισμός εισόδου (input sanitization) στον κώδικα της εφαρμογής.
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' data:; font-src 'self';");

                // Ενεργοποίηση προστασίας από XSS
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

                // Εξαναγκασμός του browser να χρησιμοποιεί μόνο HTTPS
                context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");

                // Αποτροπή αναγνώρισης MIME τύπου από τον browser
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

                await next();
            });

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<LibraryManagementDbContext>();

                var seeder = new DatabaseSeed(context);
                seeder.Seed();
            }

            app.Run();            
        }
    }
}