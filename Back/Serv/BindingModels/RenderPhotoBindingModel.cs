using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Vk_server
{
    public class RenderPhotoBindingModel
    {

        /// <summary>
        /// Фронтальное фото
        /// </summary>
        public IFormFile Photo { get; set; }


        public long ClothingId { get; set; }

        public long UserId { get; set; }


    }
}
