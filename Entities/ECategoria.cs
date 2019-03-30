using MessagePack;

namespace Entidades
{
    [MessagePackObject]
    public class ECategoria : EDepartamento
    {
        [Key(4)]
        public int CodigoCategoria { get; set; }
        [Key(5)]
        public string NomeCategoria { get; set; }
        [Key(6)]
        public string DescricaoCategoria { get; set; }
        [Key(7)]
        public double ImpostoUniao { get; set; }
        [Key(8)]
        public double ImpostoEstado { get; set; }
        [Key(9)]
        public double ImpostoMuniciopio { get; set; }
    }
}
