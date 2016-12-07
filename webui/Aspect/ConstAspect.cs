using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace webui
{
    public class ConstAspect
    {
        public static string mConnectionString = ConfigurationManager.ConnectionStrings["dbstr"].ToString();

        public static int mSucess = 1;

        public static int mFail = 2;

        public static int mOperator = 3;

        public static int mSql = 4;

        public static int mRole = 5;

        public static int mAccount = 6;

        public static int mAgent = 7;

        public static int mCreate = 8;

        public static HttpResponseMessage toJson(Object nObject)
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
