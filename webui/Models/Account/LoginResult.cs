using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class LoginResult
    {
        public ServerItem mServerItem { get; set; }
        public RoleItem mRoleItem { get; set; }
        public long mAccountId { get; set; }
    }
}
