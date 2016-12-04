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
        public HttpResponseMessage getServerList(ServerRequest nServerRequest)
        {
            ServerResult serverResult_ = new ServerResult();
            serverResult_.mServerList = ServerAspect.getServerList(nServerRequest.mOperatorName, nServerRequest.mVersionNo);
            serverResult_.mUpInts = UpIntAspect.getUpints(nServerRequest.mOperatorName, nServerRequest.mVersionNo);
            return toJson(serverResult_);
        }

        HttpResponseMessage toJson(Object nObject)
        {
            String value_;
            if (nObject is String || nObject is Char)
            {
                value_ = nObject.ToString();
            }
            else
            {
                value_ = JsonConvert.SerializeObject(nObject);
            }
            HttpResponseMessage result_ = new HttpResponseMessage();
            result_.Content = new StringContent(value_, Encoding.GetEncoding("UTF-8"), "application/json");
            return result_;
        }
    }
}
