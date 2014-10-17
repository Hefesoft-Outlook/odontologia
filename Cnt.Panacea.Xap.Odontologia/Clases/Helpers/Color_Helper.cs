using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia.Clases.Helpers
{
    public class Color_Helper
    {
        public SolidColorBrush convertirExaSolid(string hex)
        {
            //strip out any # if they exist
            hex = hex.Replace("#", string.Empty);

            byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));

            return new SolidColorBrush(Color.FromArgb(255, r, g, b));
        }

        public SolidColorBrush CombineAlphaAndColorInSolidColorBrush(string myColor)
        {
            if (myColor == "Transparent")
            {
                return new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                return new SolidColorBrush(
                  Color.FromArgb(Convert.ToByte(myColor.Substring(1, 2), 16), Convert.ToByte(myColor.Substring(3, 2), 16), Convert.ToByte(myColor.Substring(5, 2), 16), Convert.ToByte(myColor.Substring(7, 2), 16)));
            }
        }

    }
}
