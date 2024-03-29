﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Configs
{
    public static class Appsettings
    {
        public static string BaseAddress =    DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.1.238:5251" : "http://192.168.1.238:5251";
        //public static string ApiUrl = "http://localhost:5251";
        public static string LoginUrl = BaseAddress + "/auth/login";
        public static string TestUrl = BaseAddress + "/auth/test";
        public static string MyInfoUrl = BaseAddress + "/account/myinfo";
        public static string UpdateAvatar = BaseAddress + "/account/updateAvatar";
        public static string BlogListUrl = BaseAddress + "/blog";
        public static string BlogInfoUrl = BaseAddress + "/blog/";
    }
}
