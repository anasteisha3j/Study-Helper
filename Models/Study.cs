using System.ComponentModel.DataAnnotations;

namespace StudyApp.Models
{
    public class Study
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Заголовок конспекту є обов'язковим.")]
        [StringLength(100, ErrorMessage = "Заголовок не може перевищувати 100 символів.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Вміст конспекту є обов'язковим.")]
        public string Content { get; set; } = string.Empty;

        public string? FilePath { get; set; }  // Шлях до завантаженого файлу
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; }
    }
}