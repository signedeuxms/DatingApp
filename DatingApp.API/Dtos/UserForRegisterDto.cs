using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(8, MinimumLength=5, ErrorMessage="\n you must specify passWord between 5 and 8 characters.")]
        public string PassWord { get; set; }
    }
}