using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Restful
{
     [ServiceContract(Name = "FenceDataImp")]
    public  interface IChatData
    {
        [OperationContract]
        [WebGet(UriTemplate = "SendMessage/{json}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void SendMeesage(string json);

        [OperationContract]
        [WebGet(UriTemplate = "GetMessage/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetMessage(string userId);

    }
}
