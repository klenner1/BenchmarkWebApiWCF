using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProdutoController : ControllerBase
    {
        static List<EProduto> ListaProdutos { get; set; } = new List<EProduto>();

        [HttpPost]
        public EProduto Adicionar()
        {
            int codigo = ListaProdutos.Count + 1;
            EProduto produto = new EProduto()
            {
                CodigoProduto = codigo,
                NomeProduto = "Produto N" + codigo,
                DescricaoProduto = "",
                PrecoProduto = codigo * Math.PI,

                CodigoCategoria = codigo % 9,
                NomeCategoria = "Categoria N" + (codigo % 9),
                DescricaoCategoria = "Categoria N" + (codigo % 9) + " Descrição",

                CodigoDepartamento = codigo % 10,
                NomeDepartamento = "Departamento N" + (codigo % 9),
                DescricaoDepartamento = "Departamento N" + (codigo % 9) + " Descrição",
                
                ImpostoUniao = 0.34 * codigo,
                ImpostoEstado = 0.09 * codigo,
                ImpostoMuniciopio = 0.009 * codigo
            };
            ListaProdutos.Add(produto);
            return produto;
        }


        [HttpPost]
        public EProduto Criar([FromBody] EProduto produto)
        {
            ListaProdutos.Add(produto);
            return produto;
        }

        [HttpGet("{codigo}")]
        public EProduto Buscar(int codigo)
        {
            return ListaProdutos.FirstOrDefault(x => x.CodigoProduto == codigo);
        }

        [HttpGet]
        public double CalcularImposto(int codigo)
        {
            EProduto produto = ListaProdutos.FirstOrDefault(x => x.CodigoProduto == codigo);
            Double imposto = produto.PrecoProduto * (produto.ImpostoUniao + produto.ImpostoEstado + produto.ImpostoMuniciopio);
            return imposto;
        }

        [HttpDelete]
        public bool Remover(int codigo)
        {
            EProduto produto = ListaProdutos.FirstOrDefault(x => x.CodigoProduto == codigo);
            return ListaProdutos.Remove(produto);
        }
    }
}