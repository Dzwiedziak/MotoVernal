using System.ComponentModel.Design;
using System;
using BusinessLogic.Repositories;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        InjectRepositories(builder);
        InjectServices(builder);
        InjectDbContext(builder);
        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Forum}/{action=SectionsAndTopicsList}/{id?}");

        app.Run();
    }
    private static void InjectRepositories(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventRepository, EventRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<ISectionRepository, SectionRepository>();
        builder.Services.AddScoped<ITopicResponseRepository, TopicResponseRepository>();
        builder.Services.AddScoped<ITopicRepository, TopicRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IVehicleOfferRepository, VehicleOfferRepository>();
        builder.Services.AddScoped<IUserObservationRepository, UserObservationRepository>();
    }

    private static void InjectServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventService, EventService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<ISectionService, SectionService>();
        builder.Services.AddScoped<ITopicResponseService, TopicResponseService>();
        builder.Services.AddScoped<ITopicService, TopicService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IVehicleOfferService, VehicleOfferService>();
    }

    private static void InjectDbContext(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<MVAppContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
        );
    }
}
