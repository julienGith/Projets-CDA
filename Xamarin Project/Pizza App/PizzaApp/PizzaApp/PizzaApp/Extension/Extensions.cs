using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaApp.Extension
{
   public static class StringExtensions
    {
        public static string PremiereLettreMajuscule(this string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }
            String ret = str.ToLower();
            ret = ret.Substring(0, 1).ToUpper() + ret.Substring(1, ret.Length - 1);
            return ret;
        }
    }
}
