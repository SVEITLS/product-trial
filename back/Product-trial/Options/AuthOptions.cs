using System.ComponentModel.DataAnnotations;
using Product_trial.DAL.Utils;

namespace Product_trial.Options
{
    public class AuthOptions
    {
        public const string Auth = "Auth";

        [Required]
        [MinLength(1)]
        public string JwtSecret { get; set; } = string.Empty;
        [Required]
        [MinLength(1)]
        public string JwtIssuer { get; set; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "JwtExpiration must be a positive number.")]
        public int JwtExpiration {  get; set; }

        [Required]
        [EmailAddress(ErrorMessage="Admin email adress is invalid")]
        public string AdminEmail { get; set; } = string.Empty;
    }
}
