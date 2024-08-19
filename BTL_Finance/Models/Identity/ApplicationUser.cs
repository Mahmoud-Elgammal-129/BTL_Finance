using Microsoft.AspNetCore.Identity;

namespace BTL_Finance.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public bool Is_Deleted {  get; set; }=false;
    }
}
