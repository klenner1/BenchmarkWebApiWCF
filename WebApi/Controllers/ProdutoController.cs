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
            EProduto produto = CriarObjProduto(codigo);
            ListaProdutos.Add(produto);
            return produto;
        }

        [HttpPost]
        public bool AdicionarVarios(int count)
        {
            int codigo = ListaProdutos.Count + 1;
            for (int i = 0; i < count; i++)
            {
                EProduto produto = CriarObjProduto(codigo);
                ListaProdutos.Add(produto);
                codigo++;
            }
            return true;
        }

        [HttpPost]
        public EProduto Criar([FromBody] EProduto produto)
        {
            ListaProdutos.Add(produto);
            return produto;
        }

        [HttpPost]
        public int CriarVarios([FromBody] List<EProduto> produtos)
        {
            ListaProdutos.AddRange(produtos);
            return produtos.Count;
        }

        [HttpPost]
        public bool Atualizar([FromBody] EProduto produto)
        {
            EProduto produtoAntigo = ListaProdutos.FirstOrDefault(x => x.CodigoProduto == produto.CodigoProduto);
            if (produtoAntigo == null)
            {
                return false;
            }
            else
            {
                AtualizarProduto(produto, produtoAntigo);
                return true;
            }
        }

        [HttpPost]
        public int AtualizarTodos([FromBody] EProduto produto)
        {
            int count = 0;
            for (int i = 0; i < ListaProdutos.Count; i++)
            {
                EProduto produtoAntigo = ListaProdutos.ElementAt(i);
                AtualizarProduto(produto, produtoAntigo);
                count++;
            }
            return count;
        }


        [HttpPost]
        public EProduto CriarVazio()
        {
            EProduto produto = new EProduto();
            ListaProdutos.Add(produto);
            return produto;
        }

        [HttpGet("{codigo}")]
        public EProduto Buscar(int codigo)
        {
            return ListaProdutos.FirstOrDefault(x => x.CodigoProduto == codigo);
        }

        [HttpGet]
        public List<EProduto> BuscarTodos()
        {
            return ListaProdutos;
        }
        [HttpGet]
        public int Count()
        {
            return ListaProdutos.Count();
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

        private static EProduto CriarObjProduto(int codigo)
        {
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
            return produto;
        }
        private static void AtualizarProduto(EProduto produto, EProduto produtoAntigo)
        {
            if (produtoAntigo.NomeProduto != produto.NomeProduto)
            {
                produtoAntigo.DescricaoProduto = produto.DescricaoProduto;
            }
            if (produtoAntigo.DescricaoProduto != produto.DescricaoProduto)
            {
                produtoAntigo.DescricaoProduto = produto.DescricaoProduto;
            }
            if (produtoAntigo.PrecoProduto != produto.PrecoProduto)
            {
                produtoAntigo.PrecoProduto = produto.PrecoProduto;
            }

            if (produtoAntigo.NomeDepartamento != produto.NomeDepartamento)
            {
                produtoAntigo.NomeDepartamento = produto.NomeDepartamento;
            }
            if (produtoAntigo.DescricaoDepartamento != produto.DescricaoDepartamento)
            {
                produtoAntigo.DescricaoDepartamento = produto.DescricaoDepartamento;
            }

            if (produtoAntigo.NomeCategoria != produto.NomeCategoria)
            {
                produtoAntigo.NomeCategoria = produto.NomeCategoria;
            }
            if (produtoAntigo.DescricaoCategoria != produto.DescricaoCategoria)
            {
                produtoAntigo.DescricaoCategoria = produto.DescricaoCategoria;
            }

            if (produtoAntigo.ImpostoMuniciopio != produto.ImpostoMuniciopio)
            {
                produtoAntigo.ImpostoMuniciopio = produto.ImpostoMuniciopio;
            }
            if (produtoAntigo.ImpostoEstado != produto.ImpostoEstado)
            {
                produtoAntigo.ImpostoEstado = produto.ImpostoEstado;
            }
            if (produtoAntigo.ImpostoUniao != produto.ImpostoUniao)
            {
                produtoAntigo.ImpostoUniao = produto.ImpostoUniao;
            }
        }
    }
}