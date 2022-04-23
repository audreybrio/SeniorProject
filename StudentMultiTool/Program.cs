using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Formatters;

static class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers(options =>
        {
            options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            var jsonInputFormatter = options.InputFormatters.OfType<SystemTextJsonInputFormatter>().Single();
            jsonInputFormatter.SupportedMediaTypes.Add("application/json");
        });

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

        app.MapControllers();

        app.Run();
    }
}