using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCF
{
    [ServiceContract]
    public interface IService
    {
        [WebInvoke()]
        [OperationContract]
        void Get();

    }
}
