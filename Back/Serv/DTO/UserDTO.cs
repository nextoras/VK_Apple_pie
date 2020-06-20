using System;
using System.Collections.Generic;
using System.Text;

namespace Vk_server
{
    public class UserDTO
    {
        public long Id { get; set; }
        public long? VkId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long SexId { get; set; }
        public SizeDTO Sizes { get; set; }
    }

    public class SizeDTO
    {
        public double Chest { get; set; }
        public double Waist { get; set; }
        public double Hips { get; set; }
        public double Shulders { get; set; }
        public double Pelvic { get; set; }
        public double Legs { get; set; }
        public double Foots { get; set; }
    }
}