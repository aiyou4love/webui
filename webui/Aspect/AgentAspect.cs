using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webui
{
    public class AgentAspect
    {
        public static AgentInfo getIdleAgent(string nOperatorName, int nVersionNo, int nClassify)
        {
            initAgent(false);

            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return null;

            return mAgentStates[gameName_].getIdleAgent(nClassify);
        }

        static string mInitAgent = "SELECT gameName,agentId,classify,agentIp,agentPort,playerMax,playerCount,serialNo FROM t_agentList";
        public static void initAgent(bool nReinit)
        {
            if (!nReinit)
            {
                if (mInitAgented) return;
            }

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = mInitAgent;
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            while (sqlDataReader_.Read())
            {
                AgentInfo agentInfo_ = new AgentInfo();
                string gameName_ = sqlDataReader_.GetString(0).Trim();
                int agentId_ = sqlDataReader_.GetInt32(1);
                int classify_ = sqlDataReader_.GetInt16(2);
                agentInfo_.mAgentIp = sqlDataReader_.GetString(3).Trim();
                agentInfo_.mAgentPort = sqlDataReader_.GetString(4).Trim();
                agentInfo_.mPlayerMax = sqlDataReader_.GetInt32(5);
                agentInfo_.mPlayerCount = sqlDataReader_.GetInt32(6);
                agentInfo_.mSerialNo = sqlDataReader_.GetString(7).Trim();
                if (!mAgentStates.ContainsKey(gameName_))
                {
                    AgentClassify agentState_ = new AgentClassify();
                    mAgentStates[gameName_] = agentState_;
                }
                mAgentStates[gameName_].pushAgentInfo(classify_, agentId_, agentInfo_);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            mInitAgented = true;
        }

        static Dictionary<string, AgentClassify> mAgentStates = new Dictionary<string, AgentClassify>();

        static bool mInitAgented = false;
    }
}
