using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class SocialState
    {
        public void pushNetIp(List<NetIp> nNetIps)
        {
            foreach (var i in mSocialInfos)
            {
                NetIp netIp_ = new NetIp();
                netIp_.mAppNo = i.Key;
                netIp_.mAppType = 4;
                netIp_.mIp = i.Value.mSocialIp;
                netIp_.mPort = i.Value.mSocialPort;
                nNetIps.Add(netIp_);
            }
        }

        public void pushSocialInfo(int nSocialId, SocialInfo nSocialInfo)
        {
            mSocialInfos[nSocialId] = nSocialInfo;
        }

        Dictionary<int, SocialInfo> mSocialInfos = new Dictionary<int, SocialInfo>();
    }
}
