namespace CatalogoAPI.Models
{
    public class Modelo
    {
        public long ModeloId { get; set; }
        public string Nome { get; set; }
        public long MarcaId { get; set; }
        public virtual Marca Marca { get; set; }
    }
}