namespace ApiExample.Models
{
    public class ClientEntity
    {
        public int? Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Client ToModel()
        {
            return new Client()
            {
                Id = Id ?? throw new Exception("El id no puede ser nulo"),
                Firs_tName = First_Name,
                Last_Name = Last_Name,
                Email = Email,
                Phone = Phone,
                Address = Address,
            };
        }
    }
}
