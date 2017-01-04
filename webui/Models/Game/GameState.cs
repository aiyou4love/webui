using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class GameState
    {
        public void pushNetIp(List<NetIp> nNetIps)
        {
            foreach (var i in mGameInfos)
            {
                NetIp netIp_ = new NetIp();
                netIp_.mAppNo = i.Key;
                netIp_.mAppType = 2;
                netIp_.mIp = i.Value.mGameIp;
                netIp_.mPort = i.Value.mGamePort;
                nNetIps.Add(netIp_);
            }
        }

        public void pushGameInfo(int nSocialId, GameInfo nGameInfo)
        {
            mGameInfos[nSocialId] = nGameInfo;
        }

        Dictionary<int, GameInfo> mGameInfos = new Dictionary<int, GameInfo>();
    }
}
