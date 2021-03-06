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
        [WebGet(UriTemplate = "Debug/{info}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string ShowDebug(string info);

        [OperationContract]
        [WebGet(UriTemplate = "SubtractLife/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SubTractLife(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "SetLife/{json}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SetLife(string json);

        [OperationContract]
        [WebGet(UriTemplate = "SetPlayerState/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string  SetPlayerState(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "CheckEnterButton/{json}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int CheckEnterButton(string json);


        [OperationContract]
        [WebGet(UriTemplate = "SearchScore/{userId}/{userType}/{currrentUser}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string SearchScore(string userId,string userType,string currrentUser);

        [OperationContract]
        [WebGet(UriTemplate = "GetRoomList/{adminUserId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetRoomList(string adminUserId);

        //[OperationContract]
        //[WebGet(UriTemplate = "SearchScoreByRoom/{roomId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //string SearchScoreByRoom(string roomId);


        [OperationContract]
        [WebGet(UriTemplate = "EditRoom/{roomId}/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int  IsEditRoom(string roomId,string userId);

        [OperationContract]
        [WebGet(UriTemplate = "GetRoomUserTreeData/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetRoomUserTreeData(string userId);

        //通过玩家获取当前房间的所有玩家的生命值
        [OperationContract]
        [WebGet(UriTemplate = "GetRoomLifeInfoByUser/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetRoomLifeInfoByUser(string userId);

     
        [OperationContract]
        [WebGet(UriTemplate = "GetPlayerLifeInfoByUser/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetPlayerLifeInfoByUser(string userId);


        [OperationContract]
        [WebGet(UriTemplate = "GetRemainTime/{userId}/{userType}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetRemainTime(string userId,string userType);

        [OperationContract]
        [WebGet(UriTemplate = "GetLeaderAuthority/{userId}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetLeaderAuthority(string userId);


        [OperationContract]
        [WebGet(UriTemplate = "AddPlayerLife/{userId}/{addLifeValue}/{currentUser}", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string AddPlayerLife(string userId,string addLifeValue,string currentUser);


        


    }
}
