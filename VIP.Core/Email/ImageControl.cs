using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIP.Core.Email
{
    public class ImageControl
    {
        private const string postUrl = "https://geekpics.net/upload.php";
        private const string View = "https://geekpics.net/view/";
        //7ff51814bfc0651d6a73ffc25111551a
        public static string UploadImage(string img)
        {
            Easy.Net.WebPage webPage = new Easy.Net.WebPage(postUrl);
            webPage.Referer = "https://geekpics.net/";
            string resultHtml = webPage.UploadFile("ImageUp", img, new Dictionary<string, string> 
            { 
                { "Filename", System.IO.Path.GetFileName(img) },
                {"doShort","false"},
                {"sID",DateTime.Now.ToFileTime().ToString()},
                {"Upload","Submit Query"}
            });
            resultHtml = resultHtml.Split(':')[1].Replace("}", "").Replace("\"", "");
            System.Net.WebClient client = new System.Net.WebClient();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(client.DownloadString(View + resultHtml));
            return doc.DocumentNode.SelectSingleNode("//div[@class='view-full-image']/a/img").GetAttributeValue("src", "");
        }

    }
}
