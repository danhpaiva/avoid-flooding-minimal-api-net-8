namespace AvoidFloodingApi.Models
{
    public class Localizacao
    {
        public int LocalizacaoId { get; set; }
        public string? Nome { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public decimal ProbabilidadeAlagamento { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
