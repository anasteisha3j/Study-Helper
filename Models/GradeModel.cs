using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp.Models
{
    public class GradeModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Предмет є обов'язковим.")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Оцінка є обов'язковою.")]
        [Range(1, 10, ErrorMessage = "Оцінка повинна бути в діапазоні від 1 до 10.")]
        public double Grade { get; set; }

        [Required(ErrorMessage = "Дата є обов'язковою.")]
        public DateTime Date { get; set; }
    }
}
