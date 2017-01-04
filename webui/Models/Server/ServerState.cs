﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class ServerState
    {
        public int getServerId()
        {
            return mServerItems.Keys.Max();
        }

        public List<ServerItem> getServerItems()
        {
            List<ServerItem> serverItems_ = new List<ServerItem>();
            foreach (KeyValuePair<int, ServerItem> i in mServerItems)
            {
                serverItems_.Add(i.Value);
            }
            return serverItems_;
        }

        public List<ServerInfo> getServerInfos()
        {
            List<ServerInfo> serverInfos_ = new List<ServerInfo>();
            foreach (KeyValuePair<int, ServerInfo> i in mServerInfos)
            {
                serverInfos_.Add(i.Value);
            }
            return serverInfos_;
        }

        public void pushServerInfo(int nServerNo, ServerInfo nServerInfo)
        {
            mServerInfos[nServerNo] = nServerInfo;
        }

        public void pushServerItem(ServerItem nServerItem)
        {
            mServerItems[nServerItem.mServerId] = nServerItem;
        }

        public ServerInfo getServerInfo(int nServerId)
        {
            if (!mServerItems.ContainsKey(nServerId))
            {
                return null;
            }
            int serverNo_ = mServerItems[nServerId].mServerNo;
            if (!mServerInfos.ContainsKey(serverNo_))
            {
                return null;
            }
            return mServerInfos[serverNo_];
        }

        public ServerItem getServerItem(int nServerId)
        {
            if (!mServerItems.ContainsKey(nServerId))
            {
                return null;
            }
            return mServerItems[nServerId];
        }

        static Dictionary<int, ServerInfo> mServerInfos = new Dictionary<int, ServerInfo>();
        static Dictionary<int, ServerItem> mServerItems = new Dictionary<int, ServerItem>();
    }
}
