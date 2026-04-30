using Arrivo.Domain.Entities;
using Arrivo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Arrivo.Infrastructure.Persistence.Seeds;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<AppDbContext>>();

        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied.");

            await SeedRolesAsync(roleManager, logger);
            await SeedUsersAsync(userManager, logger);
            await SeedRestaurantAsync(context, userManager, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger)
    {
        foreach (var role in AppRoles.All)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                logger.LogInformation("Created role: {Role}", role);
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, ILogger logger)
    {
        // Platform Admin
        if (await userManager.FindByEmailAsync("admin@arrivo.io") is null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@arrivo.io",
                Email = "admin@arrivo.io",
                EmailConfirmed = true,
                FirstName = "Platform",
                LastName = "Admin",
                PhoneNumber = "+905000000000"
            };
            var result = await userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, AppRoles.PlatformAdmin);
                logger.LogInformation("Seeded platform admin user.");
            }
        }

        // Restaurant Owner (Fabrika Kitchen)
        if (await userManager.FindByEmailAsync("fabrika@arrivo.io") is null)
        {
            var owner = new ApplicationUser
            {
                UserName = "fabrika@arrivo.io",
                Email = "fabrika@arrivo.io",
                EmailConfirmed = true,
                FirstName = "Fabrika Kitchen",
                LastName = "Sahibi",
                PhoneNumber = "+905001234567"
            };
            var result = await userManager.CreateAsync(owner, "Owner123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(owner, AppRoles.RestaurantOwner);
                logger.LogInformation("Seeded restaurant owner user.");
            }
        }

        // Demo Cashier
        if (await userManager.FindByEmailAsync("kasiyer@arrivo.io") is null)
        {
            var cashier = new ApplicationUser
            {
                UserName = "kasiyer@arrivo.io",
                Email = "kasiyer@arrivo.io",
                EmailConfirmed = true,
                FirstName = "Fabrika Kitchen",
                LastName = "Kasiyer",
                PhoneNumber = "+905009876543"
            };
            var result = await userManager.CreateAsync(cashier, "Cashier123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(cashier, AppRoles.Cashier);
                logger.LogInformation("Seeded cashier user.");
            }
        }
    }

    private static async Task SeedRestaurantAsync(
        AppDbContext context,
        UserManager<ApplicationUser> userManager,
        ILogger logger)
    {
        if (await context.Restaurants.AnyAsync())
            return;

        var owner = await userManager.FindByEmailAsync("fabrika@arrivo.io");
        if (owner is null)
        {
            logger.LogWarning("Owner user not found; skipping restaurant seed.");
            return;
        }

        var restaurant = Restaurant.Create(
            name: "Fabrika Kitchen",
            description: "İYTE Kampüsü Gülbahçe'de, taze ve lezzetli ev yemekleri sunan kafeterya.",
            phoneNumber: "+902324986000",
            address: "İYTE Kampüsü, Gülbahçe Mah., Urla, İzmir",
            openingTime: new TimeOnly(8, 0),
            closingTime: new TimeOnly(22, 0),
            minimumOrderAmount: 50m,
            ownerId: owner.Id
        );
        restaurant.Approve();

        context.Restaurants.Add(restaurant);
        await context.SaveChangesAsync();
        logger.LogInformation("Seeded restaurant: Fabrika Kitchen (Id={Id})", restaurant.Id);

        var menuItems = new[]
        {
            MenuItem.Create(
                name: "Izgara Tavuk Dürüm",
                description: "Taze sebzeler ve özel sos ile servis edilen ızgara tavuk dürüm",
                price: 85m,
                category: "Dürüm",
                restaurantId: restaurant.Id),

            MenuItem.Create(
                name: "Et Döner Dürüm",
                description: "Özel baharatlı et döner, ince lavaşta",
                price: 95m,
                category: "Dürüm",
                restaurantId: restaurant.Id),

            MenuItem.Create(
                name: "Karışık Pide",
                description: "Kıymalı ve kaşarlı karışık pide",
                price: 110m,
                category: "Pide",
                restaurantId: restaurant.Id),

            MenuItem.Create(
                name: "Mercimek Çorbası",
                description: "Günlük taze hazırlanan mercimek çorbası, limonlu",
                price: 35m,
                category: "Çorba",
                restaurantId: restaurant.Id),

            MenuItem.Create(
                name: "Patates Kızartması",
                description: "Çıtır çıtır patates kızartması",
                price: 40m,
                category: "Yan Ürün",
                restaurantId: restaurant.Id),

            MenuItem.Create(
                name: "Ayran",
                description: "Soğuk ev yapımı ayran 300ml",
                price: 15m,
                category: "İçecek",
                restaurantId: restaurant.Id),

            MenuItem.Create(
                name: "Kola",
                description: "Kutu kola 330ml",
                price: 20m,
                category: "İçecek",
                restaurantId: restaurant.Id),

            MenuItem.Create(
                name: "Su",
                description: "Şişe su 500ml",
                price: 10m,
                category: "İçecek",
                restaurantId: restaurant.Id),
        };

        context.MenuItems.AddRange(menuItems);
        await context.SaveChangesAsync();
        logger.LogInformation("Seeded {Count} menu items for Fabrika Kitchen.", menuItems.Length);
    }
}
