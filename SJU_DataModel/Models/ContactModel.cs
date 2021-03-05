using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SJU_DataModel.Models
{
    public class ContactModel
    {
        [Key]
        public int ContactId { get; set; }
        
        public int ContactNumber { get; set; }
    }
}
