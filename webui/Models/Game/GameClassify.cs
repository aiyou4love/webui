using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class GameClassify
    {
        public void pushNetIp(List<NetIp> nNetIps, int nClassify)
        {
            if (!mGameStates.ContainsKey(nClassify))
            {
                return;
            }
            mGameStates[nClassify].pushNetIp(nNetIps);
        }

        public void pushGameInfo(int nClassify, int nGameId, GameInfo nGameInfo)
        {
            if (!mGameStates.ContainsKey(nClassify))
            {
                GameState gameState_ = new GameState();
                mGameStates[nClassify] = gameState_;
            }
            mGameStates[nClassify].pushGameInfo(nGameId, nGameInfo);
        }

        Dictionary<int, GameState> mGameStates = new Dictionary<int, GameState>();
    }
}
