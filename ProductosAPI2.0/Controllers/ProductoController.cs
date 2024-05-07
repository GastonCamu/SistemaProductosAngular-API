using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI2._0.Data;
using ProductosAPI2._0.Dto;
using ProductosAPI2._0.Models;

namespace ProductosAPI2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private ResponseDto _response;

        public ProductoController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Lista()
        {
            try
            {
                IEnumerable<Producto> productos = _context.Productos.ToList();
                _response.Data = productos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpGet("{id}")]
        public ResponseDto Obtener(int id)
        {
            try
            {
                var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == id);
                _response.Data = producto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost]
        public ResponseDto Crear([FromBody] Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPut]
        public ResponseDto Editar([FromBody] Producto producto)
        {
            try
            {
                _context.Productos.Update(producto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpDelete("{id}")]
        public ResponseDto Eliminar(int id)
        {
            try
            {
                var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == id);
                _context.Remove(producto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
