﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

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
    }
}
