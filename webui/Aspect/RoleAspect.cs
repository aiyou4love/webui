using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webui
{
    public class RoleAspect
    {
        static string mCreateRole = "INSERT INTO t_roleList(gameName,accountId,serverId,roleId,roleName,roleRace,roleType,roleStep,roleLevel)VALUES('{0}','{1}','{2}','{3}','{4}','{5}','1','1','1');";
        public static bool createRole(string nOperatorName, int nVersionNo, long nAccountId, int nServerId, string nRoleName, short nRoleRace)
        {
            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return false;

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mCreateRole, gameName_, nAccountId, nServerId, nServerId, nRoleName, nRoleRace);
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

        static string mRoleCount = "SELECT COUNT(*) FROM t_roleList WHERE gameName='{0}' AND accountId='{1}' AND serverId='{2}';";
        public static int getRoleCount(string nOperatorName, int nVersionNo, long nAccountId, int nServerId)
        {
            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return 0;

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mRoleCount, gameName_, nAccountId, nServerId);
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            int roleCount_ = 0;
            if (sqlDataReader_.Read())
            {
                roleCount_ = sqlDataReader_.GetInt32(0);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            return roleCount_;
        }

        static string mUpdateRoleStart = "UPDATE t_roleStart SET serverId='{0}',roleId='{1}' WHERE gameName='{2}' AND accountId='{3}';";
        public static int updateRoleStart(string nOperatorName, int nVersionNo, long nAccountId, int nServerId, int nRoleId)
        {
            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return ConstAspect.mOperator;

            int result_ = ConstAspect.mFail;

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mUpdateRoleStart, nServerId, nRoleId, gameName_, nAccountId);
            try
            {
                sqlCommand_.ExecuteNonQuery();
                result_ = ConstAspect.mSucess;
            }
            catch (SqlException)
            {
                result_ = ConstAspect.mSql;
            }
            sqlConnection_.Close();
            return result_;
        }

        static string mInsertRoleStart = "INSERT INTO t_roleStart(gameName,accountId,serverId,roleId) VALUES ('{0}','{1}','{2}','{3}');";
        public static int insertRoleStart(string nOperatorName, int nVersionNo, long nAccountId, int nServerId, int nRoleId)
        {
            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return ConstAspect.mOperator;

            int result_ = ConstAspect.mFail;

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mInsertRoleStart, gameName_, nAccountId, nServerId, nRoleId);
            try
            {
                sqlCommand_.ExecuteNonQuery();
                result_ = ConstAspect.mSucess;
            }
            catch (SqlException)
            {
                result_ = ConstAspect.mSql;
            }
            sqlConnection_.Close();
            return result_;
        }

        static string mRoleInfo = "SELECT roleType,roleName,roleRace,roleStep,roleLevel FROM t_roleList WHERE gameName='{0}' AND accountId='{1}' AND roleId='{2}' AND serverId='{3}';";
        public static RoleItem getRoleInfo(string nOperatorName, int nVersionNo, long nAccountId, int nRoleId, int nServerId)
        {
            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return null;

            RoleItem roleItem_ = null;

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mRoleInfo, gameName_, nAccountId, nRoleId, nServerId);
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            if (sqlDataReader_.Read())
            {
                roleItem_ = new RoleItem();
                roleItem_.mServerId = nServerId;
                roleItem_.mRoleId = nRoleId;
                roleItem_.mRoleType = sqlDataReader_.GetInt16(0);
                roleItem_.mRoleName = sqlDataReader_.GetString(1).Trim();
                roleItem_.mRoleRace = sqlDataReader_.GetInt16(2);
                roleItem_.mRoleStep = sqlDataReader_.GetInt16(3);
                roleItem_.mRoleLevel = sqlDataReader_.GetInt32(4);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            return roleItem_;
        }

        static string mRoleStart = "SELECT serverId,roleId FROM t_roleStart WHERE gameName='{0}' AND accountId='{1}';";
        public static RoleStart getRoleStart(string nOperatorName, int nVersionNo, long nAccountId)
        {
            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return null;

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mRoleStart, gameName_, nAccountId);
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            RoleStart roleStartInfo_ = null;
            if (sqlDataReader_.Read())
            {
                roleStartInfo_ = new RoleStart();
                roleStartInfo_.mServerId = sqlDataReader_.GetInt32(0);
                roleStartInfo_.mRoleId = sqlDataReader_.GetInt32(1);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            return roleStartInfo_;
        }

        static string mRoleList = "SELECT serverId,roleId,roleType,roleName,roleRace,roleStep,roleLevel FROM t_roleList WHERE gameName='{0}' AND accountId='{1}';";
        public static List<RoleItem> getRoleList(string nOperatorName, int nVersionNo, long nAccountId)
        {
            string gameName_ = OperatorAspect.getGameName(nOperatorName, nVersionNo);
            if ("" == gameName_) return null;

            List<RoleItem> roleItems_ = null;

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mRoleList, gameName_, nAccountId);
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            while (sqlDataReader_.Read())
            {
                if (null == roleItems_)
                {
                    roleItems_ = new List<RoleItem>();
                }
                RoleItem roleItem_ = new RoleItem();
                roleItem_.mServerId = sqlDataReader_.GetInt32(0);
                roleItem_.mRoleId = sqlDataReader_.GetInt32(1);
                roleItem_.mRoleType = sqlDataReader_.GetInt16(2);
                roleItem_.mRoleName = sqlDataReader_.GetString(3).Trim();
                roleItem_.mRoleRace = sqlDataReader_.GetInt16(4);
                roleItem_.mRoleStep = sqlDataReader_.GetInt16(5);
                roleItem_.mRoleLevel = sqlDataReader_.GetInt32(6);
                roleItems_.Add(roleItem_);
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            return roleItems_;
        }
    }
}
