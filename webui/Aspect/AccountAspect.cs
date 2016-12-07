﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webui
{
    public class AccountAspect
    {
        static string mGameAccount = "SELECT accountId,accountPassword,accountAuthority FROM t_accountTb WHERE accountName='{0}' AND accountType='{1}';";
        public static AccountInfo getGameAccount(string nAccountName, string nPassword, short nAccountType)
        {
            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mGameAccount, nAccountName, nAccountType);
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            AccountInfo accountInfo_ = new AccountInfo();
            string password_ = "";
            if (sqlDataReader_.Read())
            {
                accountInfo_.mAccountId = sqlDataReader_.GetInt64(0);
                password_ = sqlDataReader_.GetString(1).Trim();
                accountInfo_.mAuthority = sqlDataReader_.GetInt16(2);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();

            if (1 == nAccountType)
            {
                if (("" == password_) || (nPassword != password_))
                {
                    accountInfo_.mAccountId = 0;
                    accountInfo_.mAuthority = 0;
                }
            }
            return accountInfo_;
        }

        public static AccountInfo getAccountId(string nAccountName, string nPassword, short nAccountType)
        {
            if (1 == nAccountType)
            {
                return getGameAccount(nAccountName, nPassword, nAccountType);
            }
            if (!validAccount(nAccountName, nPassword, nAccountType))
            {
                return null;
            }
            AccountInfo accountInfo_ = getGameAccount(nAccountName, nPassword, nAccountType);

            if (accountInfo_.mAccountId > 0) return accountInfo_;

            if (accountRegister(nAccountName, "3SVkxs8b0Bj4kgqo", nAccountType))
            {
                return getGameAccount(nAccountName, nPassword, nAccountType);
            }
            return null;
        }

        public static bool validAccount(string nAccountName, string nPassword, short nAccountType)
        {
            return true;
        }

        static string mAccountRegister = @"INSERT INTO t_accountTb(accountName,accountPassword,accountType,accountAuthority)VALUES('{0}','{1}','{2}','1');";
        public static bool accountRegister(string nAccountName, string nAccountPassword, short nAccountType)
        {
            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mAccountRegister, nAccountName, nAccountPassword, nAccountType);
            bool result_ = false;
            try
            {
                sqlCommand_.ExecuteNonQuery();
                result_ = true;
            }
            catch (SqlException)
            {
                result_ = false;
            }
            sqlConnection_.Close();
            return result_;
        }

        static string mAccountCheck = "SELECT COUNT(*) FROM t_accountTb WHERE accountName='{0}' AND accountType='1';";
        public static bool accountCheck(string nAccountName)
        {
            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mAccountCheck, nAccountName);
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            int value_ = 0;
            if (sqlDataReader_.Read())
            {
                value_ = sqlDataReader_.GetInt32(0);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            return (1 == value_);
        }
    }
}
