using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webui
{
    public class ServerResult
    {
        public List<ServerItem> mServerItems { get; set; }
        public List<ServerInfo> mServerInfos { get; set; }
        public List<NetIp> mNetIps { get; set; }
    }
}
