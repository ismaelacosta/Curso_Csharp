using Microsoft.AspNetCore.Identity;
using Shopping.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Document { get; set; }


        [Display(Name = "Nombres")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string LastName { get; set; }

        [Display(Name = "Ciudad")]
        public City City { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Adrress { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO : Pendind to put the corerct paths
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7066/images/noimage.png"
            : $"https://shoppingprep.blob.core.windows.net/users/{ImageId}";


        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

      

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Usuario")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";




    }
}
