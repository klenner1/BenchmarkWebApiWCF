using MessagePack;

namespace Entidades
{
    [MessagePackObject]
    public class EProduto : ECategoria
    {
        [Key(0)]
        public int CodigoProduto { get; set; }
        [Key(1)]
        public string NomeProduto { get; set; }
        [Key(2)]
        public string DescricaoProduto { get; set; }
        [Key(3)]
        public double PrecoProduto { get; set; }
    }
}
