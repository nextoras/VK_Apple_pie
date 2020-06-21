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
        public UserParametersDTO UserParameters { get; set; }
    }

    public class UserParametersDTO
    {
        public long Id { get; set; }
        public double Chest { get; set; }
        public double Waist { get; set; }
        public double Hips { get; set; }
        public double Legs { get; set; }
        public long? SizeUpId { get; set; }
        public long? SizeMiddleId { get; set; }
        public long? SizeDownId { get; set; }
        public string PartsCoordinates { get; set; }
        public long UserId { get; set; }
    }
}