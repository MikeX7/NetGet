using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace NetGetApp
{
    class NetGet
    {
        public static ResourceManager Lang;
        static int _fileNum = 1;
        static void Main(string[] args)
        {
            Lang = new ResourceManager("NetGetApp.lang.lang", Assembly.GetExecutingAssembly());            
            
            // ReSharper disable once ObjectCreationAsStatement
            new NetGet(args);
        }
        
        public NetGet(String[] args)
        {
            var argTabRaw = ShadowLib.ArgCutter(args);
            var argTab = ArgTabSort(argTabRaw);

            if (argTab.ContainsKey("h"))
            {                
                Console.WriteLine(Lang.GetString("help"), Environment.NewLine);
                Console.WriteLine(Lang.GetString("mainAnyKey"));
                Console.ReadKey();   
                return;
            }

            if (!argTab.ContainsKey("url"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Lang.GetString("mainNoUrl"));
                Console.ResetColor();
                return;
            }

            var fileName = argTab["o"].ToString();

            if (argTab.ContainsKey("rn"))
            {
                fileName = _fileNum++ + "_" + argTab["o"];
            }

            GetUrl(argTab["url"].ToString(), fileName);

            int timer;

            if (argTab.ContainsKey("r") && int.TryParse(argTab["r"].ToString(), out timer))
            {
                Console.WriteLine(Lang.GetString("mainSleep") + Environment.NewLine, timer);

                Thread.Sleep(timer * 1000);
                Main(args);
            }
            else
            {
                Console.WriteLine(Lang.GetString("mainAnyKey"));
                Console.ReadKey();   
            }            
        }        

        private static Hashtable ArgTabSort(ICollection argTabRaw)
        {            
            String[] allowedArgs = {"url", "o", "rn", "r", "h"};
            var argTab = new Hashtable();

            if (argTabRaw.Count == 0)
            {
                return argTab;
            }

            foreach (DictionaryEntry entry in argTabRaw)
            {
                if (allowedArgs.Contains(entry.Key))
                {
                    argTab.Add(entry.Key, entry.Value);
                }
            }

            return argTab;
        }

        private static void GetUrl(String url, String fileName)
        {
            using (var client = new WebClient())
            {      
                Console.WriteLine(Lang.GetString("urlConnecting"), url);

                try
                {
                    if (fileName == "" || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                    {
                        client.DownloadString(url);

                        Console.WriteLine(Lang.GetString("urlDoneNoFile"));
                    }
                    else
                    {
                        client.DownloadFile(url, fileName);

                        Console.Write(Lang.GetString("urlDone"), Environment.NewLine);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(fileName);
                        Console.ResetColor();
                    }
                }
                catch(WebException)
                {                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Lang.GetString("urlMalformed"), url);
                    Console.ResetColor();                    
                }
            }
        }
    }
}
