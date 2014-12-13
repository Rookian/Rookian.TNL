using System.ComponentModel.DataAnnotations;

namespace Rookian.TNL.Features.Account
{
    public class LoginForm
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}