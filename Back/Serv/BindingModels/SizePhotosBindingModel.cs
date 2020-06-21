using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Vk_server
{
    public class SizePhotosBindingModel
    {

        /// <summary>
        /// Фронтальное фото
        /// </summary>
        public Image PhotoFront { get; set; }

        /// <summary>
        /// Боковое фото
        /// </summary>

        public Image PhotoSide { get; set; }

        public long UserId { get; set; }
        public long Height { get; set;}


    }
}
