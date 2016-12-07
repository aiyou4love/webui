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
            serverResult_.mRoleList = RoleAspect.getRoleList(nServerRequest.mOperatorName, nServerRequest.mVersionNo, nServerRequest.mAccountId);
            serverResult_.mServerList = ServerAspect.getServerList(nServerRequest.mOperatorName, nServerRequest.mVersionNo);
            return ConstAspect.toJson(serverResult_);
        }

        //http://localhost/api/server/getServerItems
        //content-type: application/json;charset=utf-8
        //{"mOperatorName": "iosfigus", "mVersionNo": "1"}
        [HttpPost]
        public HttpResponseMessage getServerItems([FromBody]ServerRequest nServerRequest)
        {
            ServerResult serverResult_ = new ServerResult();
            serverResult_.mRoleList = RoleAspect.getRoleList(nServerRequest.mOperatorName, nServerRequest.mVersionNo, nServerRequest.mAccountId);
            serverResult_.mServerList = ServerAspect.getServerList(nServerRequest.mOperatorName, nServerRequest.mVersionNo);
            return ConstAspect.toJson(serverResult_);
        }
    }
}
