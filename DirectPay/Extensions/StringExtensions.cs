using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DirectPay.Extensions
{
    public static class StringExtensions
    {
        internal static string ToMd5Hash(this string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }


        internal static string AddResultExplanationCdata(this string input, bool bypass)
        {
            if (bypass)
            {
                return input;
            }

            try
            {
                //Because the directpay API was written by inexperienced developers who know nothing about what CDATA tags are used for.
                //This isn't a great solution though but it works
                //TODO: Get irritated by DPO devs not fixing their shitty xml response

                var startText = "<ResultExplanation>";
                var endText = "</ResultExplanation>";

                var startIndex = input.IndexOf(startText, StringComparison.OrdinalIgnoreCase);
                var endIndex = input.IndexOf(endText, StringComparison.OrdinalIgnoreCase);
                if (startIndex <= 0 || endIndex <= 0)
                {
                    return input;
                }

                var result = input.Insert(startIndex + startText.Length, "<![CDATA[");
                endIndex = result.IndexOf(endText, StringComparison.OrdinalIgnoreCase);
                result = result.Insert(endIndex, "]]>");
                return result;
            }
            catch (Exception)
            {
                //Fallback to original input
                return input;
            }
        }
        internal static string AddInstructionsCdata(this string input, bool bypass)
        {
            if (bypass)
            {
                return input;
            }

            try
            {
                //Because the directpay API was written by developers who know nothing about how CDATA tags are used.
                //This isn't a great solution though but it works
                //TODO: Get irritated by DPO devs not fixing their shitty xml response

                var startText = "<instructions>";
                var endText = "</instructions>";

                var startIndex = input.IndexOf(startText, StringComparison.OrdinalIgnoreCase);
                var endIndex = input.IndexOf(endText, StringComparison.OrdinalIgnoreCase);
                if (startIndex <= 0 || endIndex <= 0)
                {
                    return input;
                }

                var result = input.Insert(startIndex + startText.Length, "<![CDATA[");
                endIndex = result.IndexOf(endText, StringComparison.OrdinalIgnoreCase);
                result = result.Insert(endIndex, "]]>");
                return result;
            }
            catch (Exception)
            {
                //Fallback to original input
                return input;
            }
        }
    }
}