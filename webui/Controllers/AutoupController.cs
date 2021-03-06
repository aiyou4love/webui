﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web.Http;

namespace webui.Controllers
{
    public class AutoupController : ApiController
    {
        //http://localhost/api/autoup/lastupdate/?nOperatorName=iosfigus&nVersionNo=1
        readonly string mLastUpdate = "SELECT updateName,updateNo,packetName,downloadUrl FROM t_autoUpdate WHERE operatorName='{0}'";
        [HttpGet]
        public HttpResponseMessage lastUpdate(string nOperatorName, int nVersionNo)
        {
            UpdateResult updateResult_ = new UpdateResult();
            updateResult_.mErrorCode = ConstAspect.mFail;
            updateResult_.mUpdateItems = null;

            string operatorName_ = OperatorAspect.getOperator(nOperatorName, nVersionNo);
            if ("" == operatorName_)
            {
                updateResult_.mErrorCode = ConstAspect.mOperator;
                return ConstAspect.toJson(updateResult_);
            }

            SqlConnection sqlConnection_ = new SqlConnection();

            sqlConnection_.ConnectionString = ConstAspect.mConnectionString;
            sqlConnection_.Open();

            SqlCommand sqlCommand_ = new SqlCommand();
            sqlCommand_.Connection = sqlConnection_;
            sqlCommand_.CommandType = CommandType.Text;
            sqlCommand_.CommandText = string.Format(mLastUpdate, operatorName_);
            SqlDataReader sqlDataReader_ = sqlCommand_.ExecuteReader();
            while (sqlDataReader_.Read())
            {
                updateResult_.mErrorCode = ConstAspect.mSucess;

                string updateName_ = sqlDataReader_.GetString(0).Trim();
                if ("versionNo" != updateName_)
                {
                    if (null == updateResult_.mUpdateItems)
                    {
                        updateResult_.mUpdateItems = new List<UpdateItem>();
                    }
                    UpdateItem updateItem_ = new UpdateItem();
                    updateItem_.mUpdateName = updateName_;
                    updateItem_.mUpdateNo = sqlDataReader_.GetInt32(1);
                    updateItem_.mPacketName = sqlDataReader_.GetString(2).Trim();
                    updateItem_.mDownloadUrl = sqlDataReader_.GetString(3).Trim();
                    updateResult_.mUpdateItems.Add(updateItem_);
                }
                else
                {
                    updateResult_.mVersionNo = sqlDataReader_.GetInt32(1);
                }
            }
            sqlDataReader_.Close();
            sqlConnection_.Close();
            return ConstAspect.toJson(updateResult_);
        }
    }
}
