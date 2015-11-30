using System;

namespace FormatHexString
{
    class Program
    {
        static void Main(string[] args)
        {
            string unFormattedString = "\\xE9 blah \\xfa balh \\xff";
            string formattedString = FormatHex2String(unFormattedString);

            Console.WriteLine(formattedString);
            Console.ReadLine();
        }

        private static string FormatHex2String(string hexString)
        {
            try
            {
                while (hexString.Contains("\\x"))
                {
                    int startIndex = hexString.IndexOf("\\x") + 2;
                    string subString = hexString.Substring(startIndex, 2);

                    int intValue = Convert.ToInt32(subString, 16);
                    string stringValue = char.ConvertFromUtf32(intValue);

                    //replaces all of the same hex value occurances
                    hexString = hexString.Replace("\\x" + subString, stringValue); 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No go... {0}", e.Message);
            }
            return hexString;
        }
    }
}
