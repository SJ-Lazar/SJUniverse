using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SJU_DataModel.Models
{
    public class AirplaneModel
    {
        [Key]
        public int AirplaneId { get; set; }
        [Required]
        public string AirplaneName { get; set; }
        [Required]
        public int Seating { get; set; }
    }
}
