using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_ATM_Project.Encryption;

using OOP_ATM_Project.Interfaces;

namespace OOP_ATM_Project.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Toolbox
    {
        #region Classes

        #endregion

        #region Fields 

        #endregion

        #region Properties

        #endregion

        #region Constructors
        //public Toolbox()
        //{

        //}
        #endregion

        #region Methods
            #region Get Methods
        public static double GetDouble(string text)
        {
            while (true)
            {
                try
                {
                    Console.Write(text);

                    return double.Parse(Console.ReadLine()!);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static int GetInt(string text,
            int min = int.MinValue,
            int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(text);

                if (int.TryParse(Console.ReadLine(), out int val)
                    && val >= min
                    && val <= max)
                {
                    return val;
                }
                else
                {
                    Console.WriteLine("Podano nieprawidlowa wartosc!");
                }
            }
        }

        public static string GetString(string text)
        {
            while (true)
            {
                Console.Write(text);

                string val = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(val))
                {
                    return val;
                }
                else
                {
                    Console.WriteLine("Podano nieprawidlowa wartosc!");
                }
            }
        }
        #endregion

        #region Set Methods

        #endregion

        #region Console Text Coloring
        public static void ColorWrite(ConsoleColor color,
            string text,
            params object[] parameters)
        {
            ConsoleColor actualFg = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text, parameters);
            Console.ForegroundColor = actualFg;
        }

        public static void ColorWriteLine(ConsoleColor col,
            string text,
            params object[] parameters)
        {
            ColorWrite(col, text + "\n", parameters);
        }
        #endregion

        public static byte[]? PadRight(this byte[] input, int padValue)
        {
            byte[]? outputArray = null;

            if (padValue > 0 && input != null && input.Length > 0)
            {
                outputArray = new byte[padValue];
                Array.Copy(input, outputArray, ( padValue > input.Length ? input.Length : padValue ));
            }

            return outputArray;
        }

        #region Hex-ByteArray Conversion
        public static byte[]? HexToBytesArray(this string input)
        {
            if (string.IsNullOrEmpty(input) || ( input.Length % 2 != 0 ))
            {
                return null;
            }

            byte[] output = new byte[input.Length / 2];

            try
            {
                for (int i = 0; i < input.Length; i += 2)
                    output[i / 2] = byte.Parse(input.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return null;
            }

            return output;
        }

        public static string BytesArrayToHex(this byte[] input)
        {
            string output = string.Empty;

            foreach (var b in input)
                output += b.ToString("X").PadLeft(2, '0');

            return output;
        }
        #endregion

        #endregion
    }
}
