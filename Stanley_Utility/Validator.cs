using System;
using System.Text.RegularExpressions;

namespace Stanley_Utility
{
    public class Validator
    {
        private static bool IsContainSpeicalChar(string str, bool allowSpace)
        {
            string pattern = allowSpace ? "[^A-Za-z0-9 _-]" : "[^A-Za-z0-9_-]";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(str);
        }

        public static bool IsValid(string str, bool allowSpace, int minLen, int maxLen)
        {
            bool result;
            if (str == null || str == "")
            {
                result = false;
            }
            else if (str.Length < minLen)
            {
                result = false;
            }
            else if (str.Length > maxLen)
            {
                result = false;
            }
            else if (!allowSpace && str.Contains(" "))
            {
                result = false;
            }
            else if (Validator.IsContainSpeicalChar(str, allowSpace))
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] != ' ')
                    {
                        break;
                    }
                    if (i == str.Length - 1)
                    {
                        return false;
                    }
                }
                result = true;
            }
            return result;
        }

        public static bool IsValid(string str, bool AllowSpace, int MaxLen)
        {
            bool result;
            if (str == null || str == "")
            {
                result = false;
            }
            else if (str.Length > MaxLen)
            {
                result = false;
            }
            else if (!AllowSpace && str.Contains(" "))
            {
                result = false;
            }
            else if (Validator.IsContainSpeicalChar(str, AllowSpace))
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] != ' ')
                    {
                        break;
                    }
                    if (i == str.Length - 1)
                    {
                        return false;
                    }
                }
                result = true;
            }
            return result;
        }

        public static bool IsValid(string val, int MinVal, int MaxVal)
        {
            bool result;
            if (val == null || val == "")
            {
                result = false;
            }
            else
            {
                int num = 0;
                result = (int.TryParse(val, out num) && (num >= MinVal && num <= MaxVal));
            }
            return result;
        }

        public static bool IsValid(string val, long MinVal, long MaxVal)
        {
            bool result;
            if (val == null || val == "")
            {
                result = false;
            }
            else
            {
                long num = 0L;
                result = (long.TryParse(val, out num) && (num >= MinVal && num <= MaxVal));
            }
            return result;
        }

        public static bool IsValid(string val, float MinVal, float MaxVal)
        {
            bool result;
            if (val == null || val == "")
            {
                result = false;
            }
            else
            {
                float num = 0f;
                result = (float.TryParse(val, out num) && (num >= MinVal && num <= MaxVal));
            }
            return result;
        }

        public static bool IsValid(string val, double MinVal, double MaxVal)
        {
            bool result;
            if (val == null || val == "")
            {
                result = false;
            }
            else
            {
                double num = 0.0;
                result = (double.TryParse(val, out num) && (num >= MinVal && num <= MaxVal));
            }
            return result;
        }

        public static string TimeSpanToReadableString(TimeSpan span)
        {
            string text = string.Format("{0}{1}{2}{3}", new object[]
            {
                (span.Duration().Days > 0) ? string.Format("{0:0}d: ", span.Days) : string.Empty,
                (span.Duration().Hours > 0) ? string.Format("{0:0}h: ", span.Hours) : string.Empty,
                (span.Duration().Minutes > 0) ? string.Format("{0:0}m: ", span.Minutes) : string.Empty,
                (span.Duration().Seconds > 0) ? string.Format("{0:0}s", span.Seconds) : string.Empty
            });
            if (text.EndsWith(", "))
            {
                text = text.Substring(0, text.Length - 2);
            }
            if (string.IsNullOrEmpty(text))
            {
                text = "0s";
            }
            return text;
        }
    }
}
