using System;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NetGetApp
{
    static class ShadowLib
    {
        public static Hashtable argCutter(String[] args)
        {
            String argsLine = String.Join(" ", args);

            MatchCollection mc = Regex.Matches(argsLine, "-([a-z0-9]{1,9})(?: (?!-)([^ ]{1,900}))?");

            Hashtable argTab = new Hashtable();

            foreach (Match m in mc)
            {
                argTab.Add(m.Groups[1].ToString(), m.Groups[2].ToString());                
                Debug.WriteLine(m.Groups[1] + ": " + m.Groups[2]);
            }            

            return argTab;
        }
    }
}
