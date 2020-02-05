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
    public  interface IFenceData
    {
        [OperationContract]
        [WebGet(UriTemplate = "SaveFence/{json}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int Save(string json);

        [OperationContract]
        [WebGet(UriTemplate = "UpdateGrounpState/{grounpId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string UpdateState(string grounpId);
    }
}
