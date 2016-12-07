using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webui.Controllers
{
    public class RoleController : ApiController
    {
        //http://localhost/api/role/createRole
        //content-type: application/json;charset=utf-8
        //{"mAccountName": "zyh", "mAccountPassword": "123456", "mAgentName": "iosfigus", "mVersionNo": "1","mAccountId": "1", "mServerId": "1", "mRoleName": "赵由华", "nRoleRace": "1", "nUpdate": "true"}
        [HttpPost]
        public HttpResponseMessage createRole([FromBody]RoleRequest nRoleRequest)
        {
            AccountInfo accountInfo_ = AccountAspect.getAccountId(nRoleRequest.mAccountName, nRoleRequest.mPassword, nRoleRequest.mAccountType);
            RoleResult roleResult_ = new RoleResult();
            roleResult_.mErrorCode = ConstAspect.mFail;
            roleResult_.mAccountId = 0;
            roleResult_.mRoleItem = null;
            if ((null == accountInfo_) || (0 == accountInfo_.mAccountId) || (nRoleRequest.mAccountId != accountInfo_.mAccountId))
            {
                roleResult_.mErrorCode = ConstAspect.mAccount;
                return ConstAspect.toJson(roleResult_);
            }
            roleResult_.mAccountId = accountInfo_.mAccountId;
            int roleCount_ = RoleAspect.getRoleCount(nRoleRequest.mOperatorName, nRoleRequest.mVersionNo, accountInfo_.mAccountId, nRoleRequest.mServerId);
            if (roleCount_ > 0)
            {
                roleResult_.mErrorCode = ConstAspect.mRole;
                return ConstAspect.toJson(roleResult_);
            }
            if (RoleAspect.createRole(nRoleRequest.mOperatorName, nRoleRequest.mVersionNo, accountInfo_.mAccountId, nRoleRequest.mServerId, nRoleRequest.mRoleName, nRoleRequest.mRoleRace))
            {
                roleResult_.mErrorCode = ConstAspect.mSucess;
                roleResult_.mAccountId = accountInfo_.mAccountId;
                roleResult_.mRoleItem = new RoleItem();
                roleResult_.mRoleItem.mRoleId = nRoleRequest.mServerId;
                roleResult_.mRoleItem.mServerId = nRoleRequest.mServerId;
                roleResult_.mRoleItem.mRoleName = nRoleRequest.mRoleName;
                roleResult_.mRoleItem.mRoleRace = nRoleRequest.mRoleRace;
                roleResult_.mRoleItem.mRoleType = accountInfo_.mAuthority;
                roleResult_.mRoleItem.mRoleStep = 1;
                roleResult_.mRoleItem.mRoleLevel = 1;
            }
            else
            {
                roleResult_.mErrorCode = ConstAspect.mCreate;
            }
            if (nRoleRequest.mUpdate)
            {
                RoleAspect.updateRoleStart(nRoleRequest.mOperatorName, nRoleRequest.mVersionNo, accountInfo_.mAccountId, nRoleRequest.mServerId, nRoleRequest.mServerId);
            }
            else
            {
                RoleAspect.insertRoleStart(nRoleRequest.mOperatorName, nRoleRequest.mVersionNo, accountInfo_.mAccountId, nRoleRequest.mServerId, nRoleRequest.mServerId);
            }
            return ConstAspect.toJson(roleResult_);
        }
    }
}
