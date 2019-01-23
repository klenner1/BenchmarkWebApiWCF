using Entities;
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
        public LinkedList<EProduto> ListaProdutos { get; set; }

        public EProduto Buscar(int codigo)
        {
            return ListaProdutos.FirstOrDefault(x=>x.Codigo==codigo);
        }

        public void Criar(EProduto produto)
        {
            ListaProdutos.AddLast(produto);
        }
    }
}
