using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_trial.DAL.Models
{
    public class Account
    {
        public Account() 
        { 
        }
        public string Username { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        [Key]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
