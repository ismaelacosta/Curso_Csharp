﻿using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }


        //TODO: Poner un enlace correcto
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7066/images/noimage.png"
            : $"https://shoppingzulu.blob.core.windows.net/products/{ImageId}";
    }
}
