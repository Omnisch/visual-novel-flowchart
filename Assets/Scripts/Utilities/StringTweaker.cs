// author: Omnistudio
// version: 2025.02.16

using UnityEngine;

namespace Omnis
{
    public class StringTweaker
    {
        public static readonly string[] hanNumbers = { "��", "һ", "��", "��", "��", "��", "��", "��", "��", "��" };
        public static readonly string[] hanNumbersFormal = { "��", "Ҽ", "��", "��", "��", "��", "½", "��", "��", "��" };
        private static readonly string[] unit = { "", "ʮ", "��", "ǧ", "��", "ʮ", "��", "ǧ", "��" };
        private static readonly string[] unitFormal = { "", "ʰ", "��", "Ǫ", "��", "ʰ", "��", "Ǫ", "��" };

        public static string NumberToHanCardinal(string numStr, bool useFormal = false)
        {
            // Not a number.
            if (!float.TryParse(numStr, out float num)) return "NAN";

            var splitted = num.ToString("F99").TrimEnd('0').Split('.');
            string integerPart = splitted[0];

            var numbers = useFormal ? hanNumbersFormal : hanNumbers;
            var units = useFormal ? unitFormal : unit;
            string integerStr = "";

            if (integerPart == "0")
            {
                integerStr = numbers[0];
            }
            else
            {
                int length = integerPart.Length;
                int midZero = 0;

                for (int i = 0; i < length; i++)
                {
                    int digit = integerPart[i] - '0';
                    int unitIndex = (length - 2 - i) % 8 + 1;

                    if (digit == 0)
                    {
                        midZero++;
                        if (unitIndex % 8 == 0)
                            integerStr += units[unitIndex];
                        else if (midZero < 4 && unitIndex % 4 == 0)
                            integerStr += units[unitIndex];
                    }
                    else
                    {
                        if (midZero > 0)
                        {
                            integerStr += numbers[0];
                            midZero = 0;
                        }
                        integerStr += numbers[digit] + units[unitIndex];
                    }
                }
            }

            if (!useFormal)
            {
                if (integerStr.StartsWith("һʮ"))
                    integerStr = integerStr[1..];
            }

            string decimalStr = NumeralToHanIndividual(splitted[1], useFormal);

            return decimalStr == "" ? integerStr : integerStr + "��" + decimalStr;
        }

        public static string NumeralToHanIndividual(string no, bool useFormal = false)
        {
            string result = "";
            var numbers = useFormal ? hanNumbersFormal : hanNumbers;

            foreach (char digit in no)
            {
                if (digit >= '0' && digit <= '9')
                    result += numbers[digit - '0'];
                else
                    result += digit;
            }

            return result;
        }
    }
}
