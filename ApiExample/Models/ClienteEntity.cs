using System.ComponentModel.DataAnnotations;

namespace ApiExample.Models
{
    public class ClienteEntity
    {
        [Key]
        public int? Idcliente { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Cliente ToModel()
        {
            return new Cliente()
            {
                IdCliente = Idcliente ?? throw new Exception("El id no puede ser nulo"),
                FirstName = First_Name,
                LastName = Last_Name,
                Email = Email,
                Phone = Phone,
                Address = Address,
            };
        }
    }
}
