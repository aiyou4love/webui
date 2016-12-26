using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class AgentClassify
    {
        public void pushAgentInfo(int nClassify, int nAgentId, AgentInfo nAgentInfo)
        {
            if (!mAgentStates.ContainsKey(nClassify))
            {
                AgentState agentState_ = new AgentState();
                mAgentStates[nClassify] = agentState_;
            }
            mAgentStates[nClassify].pushAgentInfo(nAgentId, nAgentInfo);
        }

        public AgentInfo getIdleAgent(int nClassify)
        {
            return mAgentStates[nClassify].getIdleAgent();
        }

        public int getPlayerCount(int nClassify, int nAgentId)
        {
            return mAgentStates[nClassify].getPlayerCount(nAgentId);
        }

        public void roleEnter(int nClassify, int nAgentId)
        {
            mAgentStates[nClassify].roleEnter(nAgentId);
        }

        Dictionary<int, AgentState> mAgentStates = new Dictionary<int, AgentState>();
    }
}
