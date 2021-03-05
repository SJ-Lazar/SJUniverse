using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SJU_WebApi.ViewModels
{
    public class ConfirmEmailModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
