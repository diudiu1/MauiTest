using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Configs
{
    public static class Appsettings
    {
        public static string ApiUrl = "http://192.168.1.238:5251";
        public static string LoginUrl = ApiUrl+"/auth/login";
        public static string BlogListUrl = ApiUrl + "/blog";
        public static string BlogInfoUrl = ApiUrl + "/blog/";
    }
}
