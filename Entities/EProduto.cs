using MessagePack;

namespace Entidades
{
    [MessagePackObject]
    public class EProduto
    {
        [Key(0)]
        public int CodigoProduto { get; set; }
        [Key(1)]
        public string NomeProduto { get; set; }
        [Key(2)]
        public string DescricaoProduto { get; set; }
        [Key(3)]
        public double PrecoProduto { get; set; }
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
        [Key(10)]
        public int CodigoDepartamento { get; set; }
        [Key(11)]
        public string NomeDepartamento { get; set; }
        [Key(12)]
        public string DescricaoDepartamento { get; set; }
    }
}
