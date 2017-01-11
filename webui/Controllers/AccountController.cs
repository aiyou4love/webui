using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace webui.Controllers
{
    public class AccountController : ApiController
    {
        //http://localhost:8313/api/values/accountEnter
        //content-type: application/json;charset=utf-8
        //{"mAccountName": "zyh", "mAccountPassword": "123456", "mAgentName": "iosfigus", "mVersionNo": "1","mAccountId": "1", "mServerId": "1", "mRoleId": "1", "mStart": "true"}
        [HttpPost]
        public HttpResponseMessage accountEnter([FromBody]EnterRequest nEnterRequest)
        {
            AccountInfo accountInfo_ = AccountAspect.getAccountId(nEnterRequest.mAccountName, nEnterRequest.mPassword, nEnterRequest.mAccountType);
            EnterResult enterResult_ = new EnterResult();
            enterResult_.mErrorCode = ConstAspect.mFail;
            enterResult_.mAuthority = 0;
            enterResult_.mRoleItem = null;
            if ((null == accountInfo_) || (accountInfo_.mAccountId <= 0) || (accountInfo_.mAccountId != nEnterRequest.mAccountId))
            {
                enterResult_.mErrorCode = ConstAspect.mAccount;
                return ConstAspect.toJson(enterResult_);
            }
            enterResult_.mAuthority = accountInfo_.mAuthority;
            RoleItem roleItem_ = RoleAspect.getRoleInfo(nEnterRequest.mOperatorName, nEnterRequest.mVersionNo, nEnterRequest.mAccountId, nEnterRequest.mRoleId, nEnterRequest.mServerId);
            if (null == roleItem_)
            {
                enterResult_.mErrorCode = ConstAspect.mRole;
                return ConstAspect.toJson(enterResult_);
            }
            enterResult_.mErrorCode = ConstAspect.mSucess;
            enterResult_.mRoleItem = roleItem_;
            if (nEnterRequest.mStart)
            {
                RoleAspect.updateRoleStart(nEnterRequest.mOperatorName, nEnterRequest.mVersionNo, nEnterRequest.mAccountId, nEnterRequest.mServerId, nEnterRequest.mRoleId);
            }
            return ConstAspect.toJson(enterResult_);
        }

        //http://localhost/api/account/accountLogin
        //content-type: application/json;charset=utf-8
        //{"mAccountName": "zyh", "mPassword": "123456", "mAgentName": "iosfigus", "mVersionNo": "1"}
        [HttpPost]
        public HttpResponseMessage accountLogin([FromBody]LoginRequest nLoginRequest)
        {
            AccountInfo accountInfo_ = AccountAspect.getAccountId(nLoginRequest.mAccountName, nLoginRequest.mPassword, nLoginRequest.mAccountType);
            LoginResult loginResult_ = new LoginResult();
            loginResult_.mAccountId = 0;
            loginResult_.mRoleItem = null;
            loginResult_.mServerItem = null;
            if ((null != accountInfo_) && (accountInfo_.mAccountId > 0))
            {
                loginResult_.mAccountId = accountInfo_.mAccountId;
                int serverId_ = 0;
                RoleStart roleStart_ = RoleAspect.getRoleStart(nLoginRequest.mOperatorName, nLoginRequest.mVersionNo, accountInfo_.mAccountId);
                if (null != roleStart_)
                {
                    loginResult_.mRoleItem = RoleAspect.getRoleInfo(nLoginRequest.mOperatorName, nLoginRequest.mVersionNo, accountInfo_.mAccountId, roleStart_.mRoleId, roleStart_.mServerId);
                    serverId_ = roleStart_.mServerId;
                }
                else
                {
                    serverId_ = ServerAspect.getServerId(nLoginRequest.mOperatorName, nLoginRequest.mVersionNo);
                }
                loginResult_.mServerItem = ServerAspect.getServerItem(nLoginRequest.mOperatorName, nLoginRequest.mVersionNo, serverId_);
            }
            return ConstAspect.toJson(loginResult_);
        }

        //http://localhost/api/account/accountRegister
        //content-type: application/json;charset=utf-8
        //{"mAccountName": "zyh", "mAccountPassword": "123456"}
        [HttpPost]
        public HttpResponseMessage accountRegister([FromBody]RegisterRequest nRegisterRequest)
        {
            if (AccountAspect.accountCheck(nRegisterRequest.mAccountName)) return ConstAspect.toJson(false);

            return ConstAspect.toJson(AccountAspect.accountRegister(nRegisterRequest.mAccountName, nRegisterRequest.mAccountPassword, 1));
        }

        //http://localhost/api/account/accountCheck
        [HttpPost]
        public HttpResponseMessage accountCheck([FromBody]string nAccountName)
        {
            return ConstAspect.toJson(AccountAspect.accountCheck(nAccountName));
        }
    }
}
