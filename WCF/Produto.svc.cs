﻿using Entidades;
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


        public EProduto Adicionar()
        {
            int codigo = ListaProdutos.Count + 1;
            EProduto produto = new EProduto()
            {
                CodigoProduto = codigo,
                NomeProduto = "Produto N" + codigo,
                DescricaoProduto = "",
                PrecoProduto = codigo*Math.PI,

                CodigoCategoria = codigo%9,
                NomeCategoria = "Categoria N" + (codigo % 9),
                DescricaoCategoria = "Categoria N"+ (codigo % 9) + " Descrição",

                CodigoDepartamento = codigo % 10,
                NomeDepartamento = "Departamento N" + (codigo % 9),
                DescricaoDepartamento = "Departamento N" + (codigo % 9) + " Descrição",

                ImpostoUniao=0.34*codigo,
                ImpostoEstado=0.09*codigo,
                ImpostoMuniciopio=0.009*codigo
            };
            ListaProdutos.Add(produto);
            return produto;
        }

        public EProduto Criar(EProduto produto)
        {
            ListaProdutos.Add(produto);
            return produto;
        }

        public EProduto Buscar(int codigo)
        {
            return ListaProdutos.FirstOrDefault(x => x.CodigoProduto == codigo);
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
    }
}
