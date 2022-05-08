using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Formatters;
using StudentMultiTool.Backend.Services.Authorization;
using StudentMultiTool.Backend.Services.Authorization.Helpers;
using StudentMultiTool.Backend.Services.Authorization.Service;

static class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // CORS to allow cross-origin requests to port 8080
        // Should be before AddControllers()
        builder.Services.AddDbContext<DataContext>();
        var CORSapi = "allowApiThruCORS";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: CORSapi,
                policy =>
                {
                    policy.WithOrigins("http://localhost:5003", "https://localhost:5001", "http://localhost:8080", "https://localhost:5002")
                    .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
        });

        // Add controllers to the server to handle requests
        // Should be after AddCors()
        builder.Services.AddControllers(options =>
        {
            options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            var jsonInputFormatter = options.InputFormatters.OfType<SystemTextJsonInputFormatter>().Single();
            jsonInputFormatter.SupportedMediaTypes.Add("application/json");
        });
        var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();
        builder.Services.Configure<AppSettings>(config);
        builder.Services.AddScoped<IJwtUtils, JwtUtils>();
        builder.Services.AddScoped<IUserService, UserService>();
        //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //.AddCookie(options =>
        //{
        //    options.ExpireTimeSpan = TimeSpan.FromHours(2);
        //    options.SlidingExpiration = true;
        //    options.AccessDeniedPath = "/Forbidden/";
        //});

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (!app.Environment.IsDevelopment())
        //{
        //    app.UseExceptionHandler("/Error");
        //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //    //app.UseHsts();
        //}

        //app.UseHttpsRedirection();
        //app.UseStaticFiles();

        //app.UseRouting();

        // Use CORS policy as defined earlier
        // Should be before MapControllers()
        app.UseCors(CORSapi);

        // Map controllers for URL routing. Since we aren't using Razor Pages, we don't need
        // to call app.MapRazorPages(). Should be after UseCors()
        app.MapControllers();

        app.Run();
    }
}