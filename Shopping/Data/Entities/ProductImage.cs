using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://shoppingcursocsharp.azurewebsites.net/images/noimage.png"
            : $"https://shoppingcsharpcourse.blob.core.windows.net/products/{ImageId}";
    }
}
