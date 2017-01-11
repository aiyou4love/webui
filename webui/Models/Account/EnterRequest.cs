﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class EnterRequest
    {
        public string mAccountName { get; set; }
        public string mPassword { get; set; }
        public short mAccountType { get; set; }
        public string mOperatorName { get; set; }
        public int mVersionNo { get; set; }
        public long mAccountId { get; set; }
        public int mRoleId { get; set; }
        public int mServerId { get; set; }
        public bool mStart { get; set; }
    }
}
