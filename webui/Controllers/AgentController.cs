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
    public class AgentController : ApiController
    {
        //http://localhost/api/agent/getIdleAgent
        //content-type: application/json;charset=utf-8
        //{"mOperatorName": "iosfigus", "mVersionNo": "1", "mServerId": "1"}
        [HttpPost]
        public HttpResponseMessage getIdleAgent([FromBody]AgentRequest nAgentRequest)
        {
            AgentResult agentResult_ = new AgentResult();
            agentResult_.mErrorCode = ConstAspect.mFail;
            agentResult_.mServerInfo = ServerAspect.getServerInfo(nAgentRequest.mOperatorName, nAgentRequest.mVersionNo, nAgentRequest.mServerId);
            agentResult_.mAgentIp = "";
            agentResult_.mAgentPort = "";

            int classify_ = 0;
            if (null != agentResult_.mServerInfo)
            {
                classify_ = agentResult_.mServerInfo.mClassify;
            }
            if (0 == classify_)
            {
                agentResult_.mErrorCode = ConstAspect.mServerId;
                return toJson(agentResult_);
            }

            AgentInfo agentInfo_ = AgentAspect.getIdleAgent(nAgentRequest.mOperatorName, nAgentRequest.mVersionNo, classify_);
            if (null == agentInfo_)
            {
                agentResult_.mErrorCode = ConstAspect.mAgent;
                return toJson(agentResult_);
            }
            agentResult_.mErrorCode = ConstAspect.mSucess;
            agentResult_.mAgentIp = agentInfo_.mAgentIp;
            agentResult_.mAgentPort = agentInfo_.mAgentPort;
            return toJson(agentResult_);
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
