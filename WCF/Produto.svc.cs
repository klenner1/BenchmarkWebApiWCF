using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Produto" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Produto.svc ou Produto.svc.cs no Gerenciador de Soluções e inicie a depuração.

    public class Produto : IProduto
    {
        static List<EProduto> ListaProdutos { get; set; } = new List<EProduto>();


        public bool Adicionar()
        {
            int codigo = ListaProdutos.Count + 1;
            EProduto produto = CriarObjProduto(codigo);
            ListaProdutos.Add(produto);
            return true;
        }


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

        public bool Criar(EProduto produto)
        {
            ListaProdutos.Add(produto);
            return true;
        }

        public bool CriarVarios(List<EProduto> produtos)
        {
            ListaProdutos.AddRange(produtos);
            return true;
        }


        public bool Atualizar(EProduto produto)
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

        public bool AtualizarTodos(EProduto produto)
        {
            int count = 0;
            for (int i = 0; i < ListaProdutos.Count; i++)
            {
                EProduto produtoAntigo = ListaProdutos.ElementAt(i);
                AtualizarProduto(produto, produtoAntigo);
                count++;
            }
            return true;
        }


        public bool CriarVazio()
        {
            EProduto produto = new EProduto();
            ListaProdutos.Add(produto);
            return true;
        }

        public EProduto Buscar(int codigo)
        {
            return ListaProdutos.FirstOrDefault(x => x.CodigoProduto == codigo);
        }
        public List<EProduto> BuscarTodos()
        {
            return ListaProdutos;
        }
        public double CalcularImposto(int codigo)
        {
            EProduto produto = ListaProdutos.FirstOrDefault(x => x.CodigoProduto == codigo);
            Double imposto = produto.PrecoProduto * (produto.ImpostoUniao + produto.ImpostoEstado + produto.ImpostoMuniciopio);
            return imposto;
        }

        public bool Remover(int codigo)
        {
            EProduto produto = ListaProdutos.FirstOrDefault(x => x.CodigoProduto == codigo);
            return ListaProdutos.Remove(produto);
        }

        public bool RemoverTodos()
        {
            ListaProdutos.Clear();
            return true;
        }

        private static EProduto CriarObjProduto(int codigo)
        {
            return new EProduto()
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
        }

        public int Count()
        {
            return ListaProdutos.Count();
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
