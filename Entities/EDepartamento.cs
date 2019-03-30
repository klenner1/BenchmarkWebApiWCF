using MessagePack;

namespace Entidades
{
    [MessagePackObject]
    public class EDepartamento
    {
        [Key(10)]
        public int CodigoDepartamento { get; set; }
        [Key(11)]
        public string NomeDepartamento { get; set; }
        [Key(12)]
        public string DescricaoDepartamento { get; set; }
    }
}
