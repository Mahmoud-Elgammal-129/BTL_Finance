using System.ComponentModel.DataAnnotations;

namespace BTL_Finance.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }
}
