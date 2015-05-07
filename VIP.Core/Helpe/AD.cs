using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VIP.Core.Helpe
{
    public class ADHelper
    {
        public List<AD> ADs { get; set; }

        public static ADHelper GetAD()
        {
            try
            {
                Easy.Net.WebClient client = new Easy.Net.WebClient();
                string ads = client.DownloadString("http://code.taobao.org/svn/DataCore/AD.json");
                File.WriteAllText("AD.json", ads, Encoding.UTF8);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ADHelper>(ads);
            }
            catch
            {
                if (File.Exists("AD.json"))
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ADHelper>(File.ReadAllText("AD.json", Encoding.UTF8));
                }
            }
            return null;
        }
    }
    public class AD
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
