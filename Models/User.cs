// // In Models/User.cs
// namespace StudyApp.Models  
// {
//     public class User
//     {
//         public int Id { get; set; }
        
//         [Required]
//         public string Username { get; set; }
        
//         [Required]
//         public string Password { get; set; }
        
//         public string Role { get; set; } = "User";
//     }
// }

using Microsoft.AspNetCore.Identity;

namespace StudyApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}