using System.ComponentModel.DataAnnotations;

namespace StudyApp.Models
{
    public class NoteModel
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        [Required]
        public string Note { get; set; } = string.Empty;
    }
}
