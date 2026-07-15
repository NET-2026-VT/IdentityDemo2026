using Microsoft.AspNetCore.Identity;

namespace IdentityDemo2026.Data
{
    public class SeedData
    {
        private static ApplicationDbContext _context = default!;
        private static RoleManager<IdentityRole> _roleManager = default!;
        private static UserManager<ApplicationUser> _userManager = default!; 

        public static async Task Init(ApplicationDbContext context, IServiceProvider services)
        {

            _context = context;

            if (_context.Roles.Any()) return;

            _roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var roleNames = new[] { "User", "Admin" };

            string adminEmail = "admin@admin.com";
            string userEmail = "user@user.com";

            await AddRolesAsync(roleNames);

            var admin = await AddAccountAsync(adminEmail, "Admin", "Adminsson", 9001, "P@55w.rd!");
            var user = await AddAccountAsync(userEmail, "User", "Usersson", 25, "Pa55w.rd!");

            await AddUserToRoleAsync(admin, "Admin");
            await AddUserToRoleAsync(user, "User");

        }

        private static async Task AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            if(!await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task<ApplicationUser> AddAccountAsync(string accountEmail, string fName, string lName, int age, string pw)
        {
            var found = await _userManager.FindByEmailAsync(accountEmail);

            if (found != null) return null!;

            ApplicationUser user = new ApplicationUser
            {
                UserName = accountEmail,
                Email = accountEmail,
                FirstName = fName,
                LastName = lName,
                Age = age,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, pw);

            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return user; 
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (string roleName in roleNames)
            {
                if (await _roleManager.RoleExistsAsync(roleName)) continue;

                var role = new IdentityRole { Name = roleName };

                var result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors)); 
            }
        }
    }
}
