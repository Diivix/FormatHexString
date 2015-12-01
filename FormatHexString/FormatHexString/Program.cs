using System;
using System.Collections.Generic;
using System.Text;

namespace FormatHexString
{
    class Program
    {
        static void Main(string[] args)
        {
            string unFormattedString = "\\xc2\\xa7 kk";
            string formattedString = FormatHex2String(unFormattedString);

            Console.WriteLine(formattedString);
            Console.ReadLine();
        }

        private static string FormatHex2String(string inputString)
        {
            try
            {
                while (inputString.Contains("\\x"))
                {
                    int startIndex = inputString.IndexOf("\\x");
                    string hexString = string.Empty;

                    int cursor = startIndex;
                    bool isHexString = false;
                    while(!isHexString)
                    {
                        hexString = hexString + inputString.Substring(cursor, 4);
                        cursor += 4;

                        //Exit, if the following values are not hex delimeted.
                        if(!inputString.Substring(cursor, 2).Equals("\\x"))
                        {
                            isHexString = true;
                        }
                    }

                    string hexCompressed = hexString.Replace("\\x", "");

                    byte[] bytes = ASCIIEncoding.ASCII.GetBytes(hexCompressed);
                    List<byte> byteList = new List<byte>();
                    for (int i = 0; i <= bytes.Length - 2; i += 2)
                    {
                        int hex = Convert.ToInt32(ASCIIEncoding.ASCII.GetString(bytes, i, 2), 16);
                        byteList.Add((byte)hex);
                    }

                    hexCompressed = ASCIIEncoding.UTF32.GetString(byteList.ToArray());

                    //replaces all of the same hex value occurances
                    inputString = inputString.Replace(hexString, hexCompressed); 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No go... {0}", e.Message);
            }
            return inputString;
        }
    }
}
