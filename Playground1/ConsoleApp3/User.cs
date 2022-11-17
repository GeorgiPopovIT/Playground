using Microsoft.AspNetCore.Identity;

namespace ConsoleApp3
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<Animal> Animals { get; set; } = new HashSet<Animal>();
    }
}
