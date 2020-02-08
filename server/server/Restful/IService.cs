﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Restful
{
     [ServiceContract(Name = "FenceDataImp")]
    public  interface IService
    {
        [OperationContract]
        [WebGet(UriTemplate = "SaveFence/{json}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int Save(string json);

        [OperationContract]
        [WebGet(UriTemplate = "UpdateGrounpState/{grounpId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string UpdateState(string grounpId);

        [OperationContract]
        [WebGet(UriTemplate = "SearchGrounpState/{grounpId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SearchState(string grounpId);


        [OperationContract]
        [WebGet(UriTemplate = "SendMessage/{json}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SendMeesage(string json);

        [OperationContract]
        [WebGet(UriTemplate = "GetMessage/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetMessage(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "Deubg/{info}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string ShowDebug(string info);

        [OperationContract]
        [WebGet(UriTemplate = "SubtractLife/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SubTractLife(string userId);
    }
}