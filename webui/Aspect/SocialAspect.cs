using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webui
{
    public class SocialAspect
    {
        public static void pushNetIp(List<NetIp> nNetIps, string nOperatorName, int nVersionNo, int nClassify)
        {
            initSocial(false);

            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return;

            mGameStates[gameName_].pushNetIp(nNetIps, nClassify);
        }

        static string mInitSocial = "SELECT gameName,socialId,classify,socialIp,socialPort FROM t_socialList";
        public static void initSocial(bool nReinit)
        {
            if (!nReinit)
            {
                if (mInitSocialed) return;
            }

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = mInitSocial;
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            while (sqlDataReader_.Read())
            {
                SocialInfo gameInfo_ = new SocialInfo();
                string gameName_ = sqlDataReader_.GetString(0).Trim();
                int gameId_ = sqlDataReader_.GetInt32(1);
                int classify_ = sqlDataReader_.GetInt16(2);
                gameInfo_.mSocialIp = sqlDataReader_.GetString(3).Trim();
                gameInfo_.mSocialIp = sqlDataReader_.GetString(4).Trim();
                if (!mGameStates.ContainsKey(gameName_))
                {
                    SocialClassify gameState_ = new SocialClassify();
                    mGameStates[gameName_] = gameState_;
                }
                mGameStates[gameName_].pushSocialInfo(classify_, gameId_, gameInfo_);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            mInitSocialed = true;
        }

        static Dictionary<string, SocialClassify> mGameStates = new Dictionary<string, SocialClassify>();

        static bool mInitSocialed = false;
    }
}
