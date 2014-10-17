using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia.Behaviors
{
    public class Numeric_Behavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += AssociatedObject_KeyDown;
        }       

        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
        }

        void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            else if (
                e.Key == Key.D1
                || e.Key == Key.D2
                || e.Key == Key.D3
                || e.Key == Key.D4
                || e.Key == Key.D5
                || e.Key == Key.D6
                || e.Key == Key.D7
                || e.Key == Key.D8
                || e.Key == Key.D9
                || e.Key == Key.D0
                || e.Key == Key.NumPad0
                || e.Key == Key.NumPad1
                || e.Key == Key.NumPad2
                || e.Key == Key.NumPad3
                || e.Key == Key.NumPad4
                || e.Key == Key.NumPad5
                || e.Key == Key.NumPad6
                || e.Key == Key.NumPad7
                || e.Key == Key.NumPad8
                || e.Key == Key.NumPad9
                || e.Key == Key.Decimal
             )
            {

            }
            else
            {
                e.Handled = true;
            }

        }
    }
}
