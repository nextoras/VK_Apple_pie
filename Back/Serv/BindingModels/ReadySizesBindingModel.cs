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
        public long userId { get; set; }
        public double chest { get; set; }
        public double waist { get; set; }
        public double hips { get; set; }
        public double leg { get; set; }
        public string parts_coordinates { get; set; }
    }
}
