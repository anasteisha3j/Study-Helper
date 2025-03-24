using System.ComponentModel.DataAnnotations;

namespace StudyApp.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва завдання є обов'язковою.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Опис завдання є обов'язковим.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Термін виконання є обов'язковим.")]
        public DateTime Deadline { get; set; }

        public bool IsCompleted { get; set; } = false; 
    }
}
