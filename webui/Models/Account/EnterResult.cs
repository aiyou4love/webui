using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class EnterResult
    {
        public int mErrorCode { get; set; }
        public short mAuthority { get; set; }
        public RoleItem mRoleItem { get; set; }
    }
}
