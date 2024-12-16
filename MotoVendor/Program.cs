using BusinessLogic.Repositories;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotoVendor.Authorizations.Handlers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        InjectRepositories(builder);
        InjectServices(builder);
        InjectDbContext(builder);
        builder.Services.AddScoped<IAuthorizationHandler, IsVehicleOfferOwnerHandler>();
        builder.Services.AddScoped<IAuthorizationHandler, IsTopicResponseOwnerHandler>();
        builder.Services.AddScoped<IAuthorizationHandler, CanDeleteVehicleOfferHandler>();

        builder.Services.AddControllersWithViews();
        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<MVAppContext>()
        .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/User/Login";
            options.AccessDeniedPath = "/Home/Error";
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            SeedRolesAndAdminUser(services).GetAwaiter().GetResult();
        }

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=IntroductionPage}/{id?}");

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
        builder.Services.AddScoped<IEventIntrestRepository, EventInterestRepository>();
        builder.Services.AddScoped<IBanRepository, BanRepository>();
        builder.Services.AddScoped<IReportRepository, ReportRepository>();
        builder.Services.AddScoped<IPostReactionRepository, PostReactionRepository>();
        builder.Services.AddScoped<IPostCommentRepository, PostCommentRepository>();
        builder.Services.AddScoped<IPostCommentReactionRepository, PostCommentReactionRepository>();
        builder.Services.AddScoped<IVehicleOfferObservationRepository, VehicleOfferObservationRepository>();
        builder.Services.AddScoped<ITopicResponseReactionRepository, TopicResponseReactionRepository>();
        builder.Services.AddScoped<IReportBugService, ReportBugService>();
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
        builder.Services.AddScoped<IBanService, BanService>();
        builder.Services.AddScoped<IReportService, ReportService>();
        builder.Services.AddScoped<IPostCommentService, PostCommentService>();
        builder.Services.AddScoped<IPostCommentReactionService, PostCommentReactionService>();
        builder.Services.AddScoped<IVehicleOfferObservationService, VehicleOfferObservationService>();
        builder.Services.AddScoped<ITopicResponseReactionService, TopicResponseReactionService>();
        builder.Services.AddScoped<IBugReportRepository, BugReportRepository>();
    }

    private static void InjectDbContext(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<MVAppContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
        );
    }

    private static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var adminUser = await userManager.FindByEmailAsync("admin@motovendor.com");
        if (adminUser == null)
        {
            var newAdmin = new User("admin", "admin@motovendor.com", null, null, DateTime.Now, "")
            {
                EmailConfirmed = true
            };
            var adminPassword = "a1d2m3i4n5";
            var createAdminResult = await userManager.CreateAsync(newAdmin, adminPassword);

            if (createAdminResult.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}
