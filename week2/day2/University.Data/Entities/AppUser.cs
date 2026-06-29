using Microsoft.AspNetCore.Identity;

namespace University.Data.Entities
    {
    public class AppUser : IdentityUser<int>
        {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        }

    public class AppRole : IdentityRole<int>
        {
        }

    public class AppRoleClaim : IdentityRoleClaim<int>
        {
        }

    public class AppUserClaim : IdentityUserClaim<int>
        {
        }

    public class AppUserLogin : IdentityUserLogin<int>
        {
        }

    public class AppUserRole : IdentityUserRole<int>
        {
        }

    public class AppUserToken : IdentityUserToken<int>
        {
        }
    }