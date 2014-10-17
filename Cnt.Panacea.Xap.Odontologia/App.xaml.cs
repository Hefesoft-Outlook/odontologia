using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cnt.Std.Xap;
using Cnt.Std;
using Cnt.Std.Xap.Controles;



namespace Cnt.Panacea.Xap.Odontologia
{
    public partial class App : CntApplication
    {

        /// <summary>
        /// Obtiene o establece la propiedad session id.
        /// </summary>
        /// <value>Session id.</value>
        public new string SessionId { get; private set; }

        /// <summary>
        /// Obtiene o establece la propiedad confirmacion.
        /// </summary>
        /// <value>confirmacion.</value>
        public bool Confirmacion { get; private set; }

        public App()
        {
            this.TokenCompleto += new EventHandler<StartupEventArgs>(App_TokenCompleto);
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        void App_TokenCompleto(object sender, StartupEventArgs e)
        {
            EvaluarParametros(e.InitParams);
            this.RootVisual = new MainPage();

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.RootVisual = new MainPage();
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.

            if ((e.ExceptionObject).Message.ToString() == "El xml con la información del token viene mal formateado o no se pudo leer")
            {
                e.Handled = true;
            }

            else if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }

          
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Función para evaluar los valores contenidos dentro de los parametros de inicio
        /// </summary>
        private void EvaluarParametros(IDictionary<string, string> initParams)
        {
            if (initParams.ContainsKey("SessionId"))
                if (!String.IsNullOrEmpty(initParams["SessionId"].ToString()))
                    this.SessionId = initParams["SessionId"].ToString();
            this.Confirmacion = initParams.ContainsKey("conf");
        }

    }
}
