using Microsoft.AspNetCore.Identity;
namespace IdentityDemo2026.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public int Age { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}
