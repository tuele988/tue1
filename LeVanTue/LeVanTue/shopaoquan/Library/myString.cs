using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace shopaoquan.Library
{
  
    public static class myString
    {
        public static String ToBase64(this String s)
        {
            if (s != null)
            {
                var bytes = Encoding.UTF8.GetBytes(s);
                return Convert.ToBase64String(bytes);
            }
            return s;
        }
        public static String FromBase64(this String s)
        {
            if (s != null)
            {
                var bytes = Convert.FromBase64String(s);
                return Encoding.UTF8.GetString(bytes);
            }
            return s;
        }
        public static String ToMD5(this String s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            var hash = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static string str_slug(string s)
        {
            String[][] symbols =
            {
                new string[] {"[áà]"}
            };
            s = s.ToLower();
            foreach(var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        public static string GenerateSeoFriendlyURL(string Title)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9-\s]", (RegexOptions)0);
            Title = regex.Replace(Title, "");
            Title = Title.ToLower().Replace(" - ", "-");
            Title = Title.ToLower().Replace("- ", "-");
            Title = Title.ToLower().Replace(" -", "-");
            Title = Title.ToLower().Replace("   ", " ");
            Title = Title.ToLower().Replace("  ", " ");
            Title = Title.ToLower().Replace("  ", " ");
            Title = Title.ToLower().Replace(" ", " ");
            Title = Title.ToLower().Replace(" ", "-");
            return Title;
        }



    }
}