using HotelReservations.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotelReservations.Data.Initialization;

public class DataInitializer
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public DataInitializer(RoleManager<Role> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    
    public async Task Seed()
    {
        var adminRole = await _roleManager.FindByNameAsync("Admin");
        var managerRole = await _roleManager.FindByNameAsync("Manager");
        var employeeRole = await _roleManager.FindByNameAsync("Employee");
        var userRole = await _roleManager.FindByNameAsync("User");
        
        var admin = await _userManager.FindByNameAsync("admin");
        var manager = await _userManager.FindByNameAsync("manager");
        var employee = await _userManager.FindByNameAsync("employee");
        var user = await _userManager.FindByNameAsync("user");
        
        if (adminRole is null)
        {
            await _roleManager.CreateAsync(new Role() {Name = "Admin"});
            adminRole = await _roleManager.FindByNameAsync("Admin");
        }
        
        if (managerRole is null)
        {
            await _roleManager.CreateAsync(new Role() {Name = "Manager"});
            managerRole = await _roleManager.FindByNameAsync("Manager");
        }

        if (employeeRole is null)
        {
            await _roleManager.CreateAsync(new Role() {Name = "Employee"});
            employeeRole = await _roleManager.FindByNameAsync("Employee");
        }
        
        if (userRole is null)
        {
            await _roleManager.CreateAsync(new Role() {Name = "User"});
            userRole = await _roleManager.FindByNameAsync("User");
        }

        
        if (admin is null)
        {
            await _userManager.CreateAsync(new User() {UserName = "admin"}, "admin");
            admin = await _userManager.FindByNameAsync("admin");
            await _userManager.AddToRoleAsync(admin, "Admin");
        }
        
        if (manager is null)
        {
            await _userManager.CreateAsync(new User() {UserName = "manager"}, "manager");
            manager = await _userManager.FindByNameAsync("manager");
            await _userManager.AddToRoleAsync(manager, "Manager");
        }
        
        if (employee is null)
        {
            await _userManager.CreateAsync(new User() {UserName = "employee"}, "employee");
            employee = await _userManager.FindByNameAsync("employee");
            await _userManager.AddToRoleAsync(employee, "Employee");
        }
        
        if (user is null)
        {
            await _userManager.CreateAsync(new User() {UserName = "user"}, "user");
            user = await _userManager.FindByNameAsync("user");
            await _userManager.AddToRoleAsync(user, "User");
        }

        for (int i = 1; i <= 100; i++)
        {
            var name = "test" + i;
            var nameArray = name.ToCharArray();
            Array.Reverse(nameArray);
            var nameReversed = new string(nameArray);
            var password = "test" + i;
            var testUser = await _userManager.FindByNameAsync(name);
            if (testUser is null)
            {
                await _userManager.CreateAsync(new User()
                {
                    Email = name + "@" + name + "." + name, 
                    FirstName = name,
                    LastName = nameReversed,
                    IsAdult = i % 2 == 0,
                    PhoneNumber = (i * 10230102).ToString(),
                    UserName = name
                }, password);
            }
        }
    }
}