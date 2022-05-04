using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EvaluacionTecnica.Models;
using Microsoft.AspNetCore.Cors;

namespace EvaluacionTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly string cadenaSQL;

        public CompraController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("cn");
        }
        

        [HttpGet]
        [Route("Consultar/{idUsuario:int}")]
        public IActionResult Consultar([FromBody] Compra objeto)
        {
            List<Compra> lista = new List<Compra>();
            Compra ocompra = new Compra();
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_ComprasXUsuario", conexion);
                    cmd.Parameters.AddWithValue("idUsuario", objeto.idUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    using (var rd = cmd.ExecuteReader())
                    {

                        while (rd.Read())
                        {

                            lista.Add(new Compra
                            {
                                idUsuario = Convert.ToInt32(rd["idUsuario"]),
                                NombreUsuario = rd["NombreUsuario"].ToString(),
                                NombreCompra = rd["CompraProducto"].ToString(),
                                Precio = Convert.ToInt32(rd["Precio"])
                            });
                        }

                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }
        [HttpPost]
        [Route("Agregar")]
        public IActionResult Agregar([FromBody] Compra objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_agregar_Compra", conexion);
                    cmd.Parameters.AddWithValue("idusuario", objeto.idUsuario);
                    cmd.Parameters.AddWithValue("idproducto", objeto.idProducto);
                    cmd.Parameters.AddWithValue("nombreusuario", objeto.NombreUsuario);
                    cmd.Parameters.AddWithValue("nombreproducto", objeto.NombreCompra);
                    cmd.Parameters.AddWithValue("precio", objeto.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Compra objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_compra", conexion);
                    cmd.Parameters.AddWithValue("nombreProducto", objeto.NombreUsuario);
                    cmd.Parameters.AddWithValue("nombreUsuario", objeto.NombreCompra);
                    cmd.Parameters.AddWithValue("precio", objeto.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(int idProducto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_eliminar_compra", conexion);
                    cmd.Parameters.AddWithValue("idCompra", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
            }
            catch (Exception error)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }
    }
}
