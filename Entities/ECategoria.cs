namespace Entidades
{
    public class ECategoria : EDepartamento
    {
        public int CodigoCategoria { get; set; }
        public string NomeCategoria { get; set; }
        public string DescricaoCategoria { get; set; }

        public double ImpostoUniao { get; set; }
        public double ImpostoEstado { get; set; }
        public double ImpostoMuniciopio { get; set; }
    }
}
