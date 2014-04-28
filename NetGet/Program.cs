using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace NetGetApp
{
    class NetGet
    {
        public static ResourceManager lang;
        static void Main(string[] args)
        {
            lang = new ResourceManager("NetGetApp.lang.lang", Assembly.GetExecutingAssembly());            
            
            NetGet ng = new NetGet(args);
        }
        
        public NetGet(String[] args)
        {
            Hashtable argTabRaw = ShadowLib.argCutter(args);
            Hashtable argTab = argTabSort(argTabRaw);

            //Console.WriteLine(argTab.ContainsKey("h".ToString()));

            if (argTab.ContainsKey("h"))
            {                
                Console.WriteLine(lang.GetString("help"), Environment.NewLine);
                Console.WriteLine(lang.GetString("mainAnyKey"));
                Console.ReadKey();   
                return;
            }

            if (!argTab.ContainsKey("url"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(lang.GetString("mainNoUrl"));
                Console.ResetColor();
                return;
            }

            getUrl(argTab["url"].ToString(), argTab["o"].ToString());

            int timer;

            if (argTab.ContainsKey("r") && int.TryParse(argTab["r"].ToString(), out timer))
            {
                Console.WriteLine(lang.GetString("mainSleep") + Environment.NewLine, timer);

                Thread.Sleep(timer * 1000);
                Main(args);
            }
            else
            {
                Console.WriteLine(lang.GetString("mainAnyKey"));
                Console.ReadKey();   
            }            
        }

        private Hashtable argTabSort(Hashtable argTabRaw)
        {            
            String[] allowedArgs = {"url", "o", "rn", "r", "h"};
            Hashtable argTab = new Hashtable();

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

        private bool getUrl(String url, String fileName)
        {
            using (WebClient client = new WebClient())
            {      
                Console.WriteLine(lang.GetString("urlConnecting"), url);

                try
                {
                    if (fileName == "")
                    {
                        client.DownloadString(url);

                        Console.WriteLine(lang.GetString("urlDoneNoFile"));
                    }
                    else
                    {
                        client.DownloadFile(url, fileName);

                        Console.Write(lang.GetString("urlDone"), Environment.NewLine);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(fileName);
                        Console.ResetColor();
                    }
                }
                catch(WebException e)
                {                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(lang.GetString("urlMalformed"), url);
                    Console.ResetColor();
                    return false;
                }
            }

            return true;
        }
    }
}
