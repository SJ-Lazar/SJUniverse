using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SJU_DataModel.Models
{
    public class AirplaneSeatingModel
    {
        [Key]
        public int AirplaneSeatingId { get; set; }
        [Required]
        public int AirplaneId { get; set; }
        [Required]
        public int FlightClassId { get; set; }
        [Required]
        public int NumberOfSeats { get; set; }
    }
}
