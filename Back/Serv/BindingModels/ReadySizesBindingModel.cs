using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vk_server
{
    public class ReadySizesBindingModel
    {
        [Required]
        public long UserId { get; set; }
        public double Chest { get; set; }
        public double Waist { get; set; }
        public double Hips { get; set; }
        public double Shulders { get; set; }
        public double Pelvic { get; set; }
        public double Legs { get; set; }
        public double Foots { get; set; }
    }
}
