using System.ComponentModel.DataAnnotations;

namespace juan.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        public bool RememberMe { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
