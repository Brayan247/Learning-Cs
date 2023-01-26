using ApiExample.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace ApiExample.Database
{
    public class ApiExampleContext : DbContext
    {
        public ApiExampleContext(DbContextOptions<ApiExampleContext> options)
            : base(options)
        {

        }

        public DbSet<ClienteEntity> Cliente { get; set; }

        public List<ClienteEntity> GetAll()
        {
            List<ClienteEntity> result = Cliente.FromSqlRaw(sql:"sp_getAllClientes").ToList();
            return result;
        }

        public async Task<ClienteEntity?> Get(int id)
        {
            var result = await Cliente.FirstOrDefaultAsync(x => x.Idcliente == id);
            return result;
        }

        public async Task<bool> AddClient(CreateCliente client)
        {
            bool result = false;
            using(SqlConnection connection = new SqlConnection("Server=localhost;DataBase=csApi;Integrated Security=true;Encrypt=false"))
            {
                // Creamos el comando y le pasamos sus propiedades
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "sp_addCliente";
                command.CommandType = CommandType.StoredProcedure;

                // Creamos el parametro que va con el sp
                SqlParameter fn = new SqlParameter();
                fn.ParameterName = "@fn";
                fn.SqlDbType = SqlDbType.NVarChar;
                fn.Direction = ParameterDirection.Input;
                fn.Value = client.FirstName;

                SqlParameter ln = new SqlParameter();
                ln.ParameterName = "@ln";
                ln.SqlDbType = SqlDbType.NVarChar;
                ln.Direction = ParameterDirection.Input;
                ln.Value = client.LastName;

                SqlParameter e = new SqlParameter();
                e.ParameterName = "@e";
                e.SqlDbType = SqlDbType.NVarChar;
                e.Direction = ParameterDirection.Input;
                e.Value = client.Email;

                SqlParameter phone = new SqlParameter();
                phone.ParameterName = "@phone";
                phone.SqlDbType = SqlDbType.NVarChar;
                phone.Direction = ParameterDirection.Input;
                phone.Value = client.Phone;

                SqlParameter address = new SqlParameter();
                address.ParameterName = "@address";
                address.SqlDbType = SqlDbType.NVarChar;
                address.Direction = ParameterDirection.Input;
                address.Value = client.Address;

                // Añadimos el parametro al sp 
                command.Parameters.Add(fn);
                command.Parameters.Add(ln);
                command.Parameters.Add(e);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    Console.WriteLine($"El cliente ah sido agregado exitosamente");
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Huvo algun error al momento de Guardar");
                }
            }
            return result;
        }

        public async Task<bool> UpdateClient(ClienteEntity client)
        {
            bool result = false;
            try
            {
                Cliente.FromSqlRaw(sql: "sp_updateCliente(" + client + ")");
                await SaveChangesAsync();
                Console.WriteLine("Cliente editado con exito" + client.ToModel());
                result = true;
            }
            catch
            {
                Console.WriteLine("Hubo un error al momento de editar el cliente");
            }
            return result;
        } 

        public async Task<bool> DeleteClient(int id)
        {
            bool result = false;

            using(SqlConnection connection = new SqlConnection("Server=localhost;DataBase=csApi;Integrated Security=true;Encrypt=false"))
            {
                // Creamos el comando y le pasamos sus propiedades
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "sp_deleteCliente";
                command.CommandType = CommandType.StoredProcedure;

                // Creamos el parametro que va con el sp
                SqlParameter pk = new SqlParameter();
                pk.ParameterName = "@pk";
                pk.SqlDbType = SqlDbType.Int;
                pk.Direction = ParameterDirection.Input;
                pk.Value = id;

                // Añadimos el parametro al sp 
                command.Parameters.Add(pk);

                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    Console.WriteLine($"El cliente con el id {id} fue eliminado del sistema");
                    connection.Close();
                }
                catch (Exception ex)
                {
                Console.WriteLine("Huvo algun error al momento de Eliminar");
                }
            }
            return result;
        }
    }

}
