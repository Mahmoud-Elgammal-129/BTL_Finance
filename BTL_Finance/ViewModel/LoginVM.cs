using System.ComponentModel.DataAnnotations;

namespace BTL_Finance.ViewModel
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
