using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia.Mef.Dlls
{
    [Export(typeof(UserControl))]    
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            this.Tag = "Cnt.Panacea.Xap.Odontologia.Mef.Dlls.xap";
            InitializeComponent();
        }
    }
}
