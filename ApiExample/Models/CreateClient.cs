﻿using System.ComponentModel.DataAnnotations;

namespace ApiExample.Models
{
    public class CreateClient
    {
        [Required (ErrorMessage = "El cliente debe tener un nombre")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "El cliente debe tener un apellido")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "El cliente debe tener un correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El cliente debe tener un telefono")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El cliente debe tener una direccion")]
        public string Address { get; set; }
    }
}
