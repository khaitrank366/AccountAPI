using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LoginApi.Models
{
    public class Account
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = "1";
    }
}