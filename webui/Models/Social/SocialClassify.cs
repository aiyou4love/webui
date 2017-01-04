using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class SocialClassify
    {
        public void pushNetIp(List<NetIp> nNetIps, int nClassify)
        {
            if (!mSocialStates.ContainsKey(nClassify))
            {
                return;
            }
            mSocialStates[nClassify].pushNetIp(nNetIps);
        }

        public void pushSocialInfo(int nClassify, int nSocialId, SocialInfo nSocialInfo)
        {
            if (!mSocialStates.ContainsKey(nClassify))
            {
                SocialState socialState_ = new SocialState();
                mSocialStates[nClassify] = socialState_;
            }
            mSocialStates[nClassify].pushSocialInfo(nSocialId, nSocialInfo);
        }

        Dictionary<int, SocialState> mSocialStates = new Dictionary<int, SocialState>();
    }
}
