using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technical_Test.Services.Setting
{
    public class AppSettings
    {
        public static ConnectionStrings ConnectionStrings { get; set; }
        public static Logging Logging { get; set; }
        public static string AllowedHosts { get; set; }
    }
}
