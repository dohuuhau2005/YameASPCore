
using Microsoft.AspNetCore.Identity;
namespace yame.Models
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Fullname { get; set; }
        public DateTime Birthday { get; set; }
    }


}
