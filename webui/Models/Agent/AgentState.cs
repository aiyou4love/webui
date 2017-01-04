using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class AgentState
    {
        public void pushNetIp(List<NetIp> nNetIps)
        {
            foreach (var i in mAgentInfos)
            {
                NetIp netIp_ = new NetIp();
                netIp_.mAppNo = i.Key;
                netIp_.mAppType = 2;
                netIp_.mIp = i.Value.mAgentIp;
                netIp_.mPort = i.Value.mAgentPort;
                nNetIps.Add(netIp_);
            }
        }

        public void pushAgentInfo(int nAgentId, AgentInfo nAgentInfo)
        {
            mAgentInfos[nAgentId] = nAgentInfo;
        }

        public AgentInfo getIdleAgent()
        {
            foreach (var i in mAgentInfos)
            {
                if (i.Value.mPlayerCount < i.Value.mPlayerMax)
                {
                    return i.Value;
                }
            }
            return null;
        }

        public int getPlayerCount(int nAgentId)
        {
            return mAgentInfos[nAgentId].mPlayerCount;
        }

        public void roleEnter(int nAgentId)
        {
            mAgentInfos[nAgentId].mPlayerCount++;
        }

        Dictionary<int, AgentInfo> mAgentInfos = new Dictionary<int, AgentInfo>();
    }
}
