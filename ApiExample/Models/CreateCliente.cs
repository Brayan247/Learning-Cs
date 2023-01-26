using System.ComponentModel.DataAnnotations;

namespace ApiExample.Models
{
    public class CreateCliente
    {
        [Required (ErrorMessage = "El cliente debe tener un nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El cliente debe tener un apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El cliente debe tener un correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El cliente debe tener un telefono")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El cliente debe tener una direccion")]
        public string Address { get; set; }

        public Cliente ToModel()
        {
            return new Cliente()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Address = Address,
            };
        }
    }
}
