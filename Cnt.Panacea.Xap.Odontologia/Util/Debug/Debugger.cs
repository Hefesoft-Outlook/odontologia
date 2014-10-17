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

namespace Cnt.Panacea.Xap.Odontologia.Util.Debug
{
    public static class Debugger
    {
        public static void breakPoint()
        {          
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Launch();
                System.Diagnostics.Debugger.Break();
            }
        }
    }
}
