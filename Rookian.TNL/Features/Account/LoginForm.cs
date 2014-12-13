using System.ComponentModel.DataAnnotations;

namespace Rookian.TNL.Features.Account
{
    public class LoginForm
    {
        public LoginForm()
        {
            DefaultSubsidiary = new Subsidiary
            {
                Id = 2, IsPublic = true, Name = "Sub2"
            };
        }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public Subsidiary DefaultSubsidiary { get; set; }
    }
}