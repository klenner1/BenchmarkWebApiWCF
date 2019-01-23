using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IProduto" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IProduto
    {
        [OperationContract]
        void Criar(EProduto produto);
        EProduto Buscar(int codigo);
    }
}
