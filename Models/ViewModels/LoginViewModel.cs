using System.ComponentModel.DataAnnotations;
using StudyApp.Models;

public class LoginViewModel
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", 
    ErrorMessage = "Пароль повинен містити мінімум 8 символів, 1 велику 1 малу літери, 1 цифру, і 1 спеціальний символ.")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
