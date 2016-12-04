using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webui
{
    public class UpIntAspect
    {
        public static List<UpInt> getUpints(string nOperatorName, int nVersionNo)
        {
            initUpint(false);

            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return null;

            if (!mUpInts.ContainsKey(gameName_))
            {
                return null;
            }
            return mUpInts[gameName_];
        }

        static string mInitUpint = @"SELECT gameName,updateName,updateNo FROM t_upint;";
        static void initUpint(bool nReinit)
        {
            if (!nReinit)
            {
                if (mInitUpInted) return;
            }
            else
            {
                mUpInts.Clear();
            }

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = mInitUpint;
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            while (sqlDataReader_.Read())
            {
                UpInt upint_ = new UpInt();
                string gameName_ = sqlDataReader_.GetString(0).Trim();
                upint_.mUpdateName = sqlDataReader_.GetString(1).Trim();
                upint_.mUpdateNo = sqlDataReader_.GetInt32(2);
                if (!mUpInts.ContainsKey(gameName_))
                {
                    mUpInts[gameName_] = new List<UpInt>();
                }
                mUpInts[gameName_].Add(upint_);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            mInitUpInted = true;
        }

        static Dictionary<string, List<UpInt>> mUpInts = new Dictionary<string, List<UpInt>>();

        static bool mInitUpInted = false;
    }
}