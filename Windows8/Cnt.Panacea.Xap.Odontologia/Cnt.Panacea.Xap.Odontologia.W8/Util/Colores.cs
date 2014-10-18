using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;


internal static class Colores 
{
    internal static Color ColorEntero(int color)
    {
        string str = color.ToString("X", CultureInfo.InvariantCulture);
        if (str.Length < 6)
        {
            str = str.PadLeft(6, '0');
        }
        byte num = Convert.ToByte(str.Substring(0, 2), 16);
        byte num1 = Convert.ToByte(str.Substring(2, 2), 16);
        byte num2 = Convert.ToByte(str.Substring(4, 2), 16);
        return Color.FromArgb(255, num, num1, num2);
    }
}

