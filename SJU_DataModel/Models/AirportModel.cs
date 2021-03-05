using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SJU_DataModel.Models
{
    public class AirportModel
    {
        [Key]
        public int AirportId { get; set; }

        [Required]
        public string AirportName { get; set; }
    }
}
