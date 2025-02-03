namespace WebApplication1.Entities
{
    public class OperacionRequest
    {
        public string Operacion { get; set; }
        public int Valor1 { get; set; }
        public int Valor2 { get; set; }
    }

    public class OperacionResponse
    {
        public string Operacion { get; set; }
        public int Valor1 { get; set; }
        public int Valor2 { get; set; }
        public int Resultado { get; set; }
    }

    public class Operacion
    {
        public int Id { get; set; }
        public string OperacionTipo { get; set; }  // "sumar", "restar", etc.
        public int Valor1 { get; set; }
        public int Valor2 { get; set; }
        public int Resultado { get; set; }
        public DateTime Fecha { get; set; }
    }

}
