using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webui
{
    public class ServerAspect
    {
        public static int getServerId(string nOperatorName, int nVersionNo)
        {
            initServerState(false);

            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return 0;

            return mServerStates[gameName_].getServerId();
        }

        public static ServerItem getServerItem(string nOperatorName, int nVersionNo, int nServerId)
        {
            initServerState(false);

            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return null;

            return mServerStates[gameName_].getServerItem(nServerId);
        }

        public static List<ServerItem> getServerItems(string nOperatorName, int nVersionNo)
        {
            initServerState(false);

            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return null;

            return mServerStates[gameName_].getServerItems();
        }

        public static List<ServerInfo> getServerInfos(string nOperatorName, int nVersionNo)
        {
            initServerState(false);

            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return null;

            return mServerStates[gameName_].getServerInfos();
        }

        public static ServerInfo getServerInfo(string nOperatorName, int nVersionNo, int nServerId)
        {
            initServerState(false);

            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return null;

            return mServerStates[gameName_].getServerInfo(nServerId);
        }

        static string mInitServerItems = "SELECT gameName,serverId,serverNo,serverName,serverState FROM t_serverList";
        static void initServerItems()
        {
            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = mInitServerItems;
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            while (sqlDataReader_.Read())
            {
                string gameName_ = sqlDataReader_.GetString(0).Trim();
                ServerItem serverItem_ = new ServerItem();
                serverItem_.mServerId = sqlDataReader_.GetInt32(1);
                serverItem_.mServerNo = sqlDataReader_.GetInt32(2);
                serverItem_.mServerName = sqlDataReader_.GetString(3).Trim();
                serverItem_.mServerState = sqlDataReader_.GetInt16(4);
                if (!mServerStates.ContainsKey(gameName_))
                {
                    ServerState serverState_ = new ServerState();
                    mServerStates[gameName_] = serverState_;
                }
                mServerStates[gameName_].pushServerItem(serverItem_);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
        }

        static string mServerInfo = "SELECT gameName,serverNo,serverStart,classify FROM t_serverInfo";
        static void initServerInfo()
        {
            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = mServerInfo;
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            while (sqlDataReader_.Read())
            {
                ServerInfo serverInfo_ = new ServerInfo();
                string gameName_ = sqlDataReader_.GetString(0).Trim();
                int serverNo_ = sqlDataReader_.GetInt32(1);
                serverInfo_.mServerNo = serverNo_;
                DateTime dateTime_ = sqlDataReader_.GetDateTime(2);
                serverInfo_.mServerStart = ToTimestamp(dateTime_);
                serverInfo_.mClassify = sqlDataReader_.GetInt16(3);
                if (!mServerStates.ContainsKey(gameName_))
                {
                    ServerState serverState_ = new ServerState();
                    mServerStates[gameName_] = serverState_;
                }
                mServerStates[gameName_].pushServerInfo(serverNo_, serverInfo_);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
        }

        static long ToTimestamp(DateTime value)
        {
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            return (long)span.TotalSeconds;
        }

        static DateTime ConvertTimestamp(long timestamp)
        {
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime newDateTime = converted.AddSeconds(timestamp);
            return newDateTime.ToLocalTime();
        }

        static void initServerState(bool nReinit)
        {
            if (!nReinit)
            {
                if (mInitServered) return;
            }
            else
            {
                mServerStates.Clear();
            }

            initServerItems();
            initServerInfo();

            mInitServered = true;
        }

        static Dictionary<string, ServerState> mServerStates = new Dictionary<string, ServerState>();

        static bool mInitServered = false;
    }
}
