using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SJU_WebApi.ViewModels
{
    public class RegisterModel
    {
		
		[Required]
		public string Username { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string JobTitle { get; set; }

    }
}
