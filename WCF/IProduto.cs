using Entidades;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IProduto" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IProduto
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        EProduto Adicionar();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        EProduto Criar(EProduto produto);

        [OperationContract]
        [WebInvoke(Method = "GET",  ResponseFormat = WebMessageFormat.Json)]
        EProduto Buscar(int codigo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        double CalcularImposto(int codigo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        bool Remover(int codigo);
    }
}
