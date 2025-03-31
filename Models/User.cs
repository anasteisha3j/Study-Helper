using Microsoft.AspNetCore.Identity;

namespace StudyApp.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        
        // Navigation property for grades
        public virtual ICollection<GradeModel> Grades { get; set; } = new List<GradeModel>();
    }
}