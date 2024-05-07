using ProductosAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProductosAPI.Data
{
    public class ProductoData
    {
        private readonly string conexion;

        public ProductoData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Producto>> Lista()
        {
            List<Producto> lista = new List<Producto>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listaProductos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) {
                        lista.Add(new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Stock = Convert.ToInt32(reader["Stock"])
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<Producto> Obtener(int Id)
        {
            Producto objeto = new Producto();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerProducto", con);
                cmd.Parameters.AddWithValue("@IdProducto", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Producto
                        {
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Stock = Convert.ToInt32(reader["Stock"])
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Producto objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_crearProducto", con);
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@Stock", objeto.Stock);
                cmd.CommandType = CommandType.StoredProcedure;
                try 
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true: false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(Producto objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_editarProducto", con);
                cmd.Parameters.AddWithValue("@IdProducto", objeto.IdProducto);
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@Stock", objeto.Stock);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminarProducto", con);
                cmd.Parameters.AddWithValue("@IdProducto", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
