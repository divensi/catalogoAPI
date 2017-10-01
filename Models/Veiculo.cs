namespace CatalogoAPI.Models
{
    public class Veiculo
    {
        public long VeiculoId { get; set; }
        public string Versao { get; set; }
        public int AnoFabricacao { get; set; }
        public int AnoModelo { get; set; }
        public long IdModelo { get; set; }
        public virtual Modelo Modelo { get; set; }
    }
}