namespace ApiQuala.Entities.Class.Models
{
    public class ESucursales
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Identificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string Moneda { get; set; }
        public bool Estado { get; set; }
    }
}
