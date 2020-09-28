
using System.Text.RegularExpressions;


namespace Blog.Helpers
{
    public static class Helper
    {

        public static string UrlClean(string url)
        {

            //url.Replace(" ", "-").
            //    Replace("!", " ").
            //    Replace("ğ", "g").
            //    Replace("ş", "s").
            //    Replace("ç", "c").
            //    Replace("ö", "o").
            //    Replace("ü", "u").
            //    Replace("ı", "i");

            url = Regex.Replace(url,
                "([0-9]*)(([-/.]|[ \\w])([n][c][ıIiİuUüÜ]))|([0-9]*)(([-/.]|[\\w])([ıIiİuUüÜ][n][c][ıIiİuUüÜ]))",
                "");
            url = Regex.Replace(url, " +", "_");

            return url;
        }
    }
}