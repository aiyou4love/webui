using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webui
{
    public class GameAspect
    {
        public static void pushNetIp(List<NetIp> nNetIps, string nOperatorName, int nVersionNo, int nClassify)
        {
            initGame(false);

            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return;

            mGameStates[gameName_].pushNetIp(nNetIps, nClassify);
        }

        static string mInitGame = "SELECT gameName,gameId,classify,gameIp,gamePort FROM t_gameList";
        public static void initGame(bool nReinit)
        {
            if (!nReinit)
            {
                if (mInitGamed) return;
            }

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = mInitGame;
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            while (sqlDataReader_.Read())
            {
                GameInfo gameInfo_ = new GameInfo();
                string gameName_ = sqlDataReader_.GetString(0).Trim();
                int gameId_ = sqlDataReader_.GetInt32(1);
                int classify_ = sqlDataReader_.GetInt16(2);
                gameInfo_.mGameIp = sqlDataReader_.GetString(3).Trim();
                gameInfo_.mGamePort = sqlDataReader_.GetString(4).Trim();
                if (!mGameStates.ContainsKey(gameName_))
                {
                    GameClassify gameState_ = new GameClassify();
                    mGameStates[gameName_] = gameState_;
                }
                mGameStates[gameName_].pushGameInfo(classify_, gameId_, gameInfo_);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            mInitGamed = true;
        }

        static Dictionary<string, GameClassify> mGameStates = new Dictionary<string, GameClassify>();

        static bool mInitGamed = false;
    }
}
