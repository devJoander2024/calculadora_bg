using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Entities;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculadoraController : ControllerBase
    {
        private readonly SoapClient _soapClient;
        private readonly AppDbContext _dbContext;


        public CalculadoraController(SoapClient soapClient, AppDbContext dbContext)
        {
            _soapClient = soapClient;
            _dbContext = dbContext;
        }

        [HttpPost("operar")]
        public async Task<IActionResult> Operar([FromBody] OperacionRequest request)
        {
            int resultado = request.Operacion.ToLower() switch
            {
                "sumar" => await _soapClient.Sumar(request.Valor1, request.Valor2),
                "restar" => await _soapClient.Restar(request.Valor1, request.Valor2),
                "multiplicar" => await _soapClient.Multiplicar(request.Valor1, request.Valor2),
                "dividir" => await _soapClient.Dividir(request.Valor1, request.Valor2),
                _ => throw new ArgumentException("Operación no válida")
            };

            var respuesta = new OperacionResponse
            {
                Operacion = request.Operacion,
                Valor1 = request.Valor1,
                Valor2 = request.Valor2,
                Resultado = resultado
            };

            var operacion = new Operacion
            {
                OperacionTipo = request.Operacion,
                Valor1 = request.Valor1,
                Valor2 = request.Valor2,
                Resultado = resultado,
                Fecha = DateTime.UtcNow // Fecha actual
            };

            _dbContext.Operaciones.Add(operacion);
            await _dbContext.SaveChangesAsync();


            return Ok(respuesta);
        }

        [HttpGet("historial")]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var historial = await _dbContext.Operaciones
                .OrderByDescending(o => o.Fecha) // Ordenar por fecha descendente
                .ToListAsync();

            var historialResponse = historial.Select(o => new
            {
                o.OperacionTipo,
                o.Valor1,
                o.Valor2,
                o.Resultado,
                Fecha = o.Fecha.ToString("yyyy-MM-ddTHH:mm:ss") // Formatear la fecha
            }).ToList();

            return Ok(historialResponse);
        }

    }
}