using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimaCartoonizer
{
    static class CConvert
    {
        public static long FromHex(string Hex)
        {
            return long.Parse(Hex, System.Globalization.NumberStyles.HexNumber);
        }

        public static string ToHex(long Deci)
        {
            return Deci.ToString("X");
        }

        public static long FromBase36(string IBase36)
        {
            IBase36 = IBase36.ToUpper();
            string[] Base36 = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            int i = 0;
            long v = 0;
            // for (i = IBase36.Length - 1; i > 0; i = i--)
            for (i = IBase36.Length - 1; i >= 0; i = i - 1)
            {
                long bc = Convert.ToInt64(Math.Pow(36, ((IBase36.Length - 1) - i)));
                var target = IBase36[i].ToString();
                var result = Array.FindAll(Base36, s => s.Equals(target));
                if (result != null)
                //if (Convert.ToString(Base36).Contains(IBase36[i].ToString()))
                {
                    v += Array.LastIndexOf(Base36, IBase36[i].ToString()) * bc;
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            return v;
        }

        public static string ToBase36(double IBase36)
        {
            string[] Base36 = {
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9",
			"A",
			"B",
			"C",
			"D",
			"E",
			"F",
			"G",
			"H",
			"I",
			"J",
			"K",
			"L",
			"M",
			"N",
			"O",
			"P",
			"Q",
			"R",
			"S",
			"T",
			"U",
			"V",
			"W",
			"X",
			"Y",
			"Z"
		};
            string v = null;
            long _temp;
            decimal i = default(decimal);
            while (!(IBase36 < 1))
            {
                i = Convert.ToDecimal(IBase36 % 36);
                v = Base36[Convert.ToInt16(i)] + v;
                //IBase36 =Math.DivRem(long.Parse(IBase36), 36, out null);
                IBase36 = Math.DivRem(Convert.ToInt64(IBase36), 36, out _temp);
            }
            return v;

        }
    }

}
