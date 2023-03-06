using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class HotelDataRequest
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public string TargetDate { get; set; }
    }
}
