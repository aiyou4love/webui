using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace webui.Controllers
{
    public class ServerController : ApiController
    {
        //http://localhost/api/server/getServerList
        //content-type: application/json;charset=utf-8
        //{"mOperatorName": "iosfigus", "mVersionNo": "1"}
        [HttpPost]
        public HttpResponseMessage getServerList([FromBody]ServerRequest nServerRequest)
        {
            ServerResult serverResult_ = new ServerResult();
            serverResult_.mServerItems = ServerAspect.getServerItems(nServerRequest.mOperatorName, nServerRequest.mVersionNo);
            serverResult_.mServerInfos = ServerAspect.getServerInfos(nServerRequest.mOperatorName, nServerRequest.mVersionNo);
            serverResult_.mNetIps = new List<NetIp>();
            AgentAspect.pushNetIp(serverResult_.mNetIps, nServerRequest.mOperatorName, nServerRequest.mVersionNo, nServerRequest.mClassify);
            SocialAspect.pushNetIp(serverResult_.mNetIps, nServerRequest.mOperatorName, nServerRequest.mVersionNo, nServerRequest.mClassify);
            GameAspect.pushNetIp(serverResult_.mNetIps, nServerRequest.mOperatorName, nServerRequest.mVersionNo, nServerRequest.mClassify);
            return ConstAspect.toJson(serverResult_);
        }

        //http://localhost/api/server/getServerItems
        //content-type: application/json;charset=utf-8
        //{"mOperatorName": "iosfigus", "mVersionNo": "1"}
        [HttpPost]
        public HttpResponseMessage getServerItems([FromBody]ServerItemReq nServerItemReq)
        {
            ServerItemRes serverItemRes_ = new ServerItemRes();
            serverItemRes_.mRoleList = RoleAspect.getRoleList(nServerItemReq.mOperatorName, nServerItemReq.mVersionNo, nServerItemReq.mAccountId);
            serverItemRes_.mServerList = ServerAspect.getServerItems(nServerItemReq.mOperatorName, nServerItemReq.mVersionNo);
            return ConstAspect.toJson(serverItemRes_);
        }
    }
}
