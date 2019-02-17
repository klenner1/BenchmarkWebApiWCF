namespace Entidades
{
    public class EProduto : ECategoria
    {
        public int CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public double PrecoProduto { get; set; }
    }
}
