using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Printing;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;
using System.Windows.Media;
using System.Collections.Generic;
using Cnt.Panacea.Xap.Odontologia.Clases;
using ImageUploader;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;

namespace Cnt.Panacea.Xap.Odontologia.PopUp
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ImprimirOdontograma : ChildWindow
    {
        #region Variables
        private const string SAVEDIMG = "Odontograma.png";
        //public List<Odontologia.Clases.SuperficiesGrillaPlanTratamiento> GrillaPlanTratamiento { get; set; }
        //public List<ProcedimientosGrillaPlanTratamiento> ProcedimientosGrillaPlanTratamiento { get; set; }
        public List<GrillaTratamiento> grillaTratamiento { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ImprimirOdontograma"/> class.
        /// </summary>
        public ImprimirOdontograma()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(ImprimirOdontograma_Loaded);
            InitializeReport();
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Initializes the report.
        /// </summary>
        private void InitializeReport()
        {
            if (Variables_Globales.Plantratamiento)
            {
                PlanTratamiento();
            }
            else if (Variables_Globales.Evolucion)
            {
                PlanEvolucion();
            }
            else if (Variables_Globales.OdontogramaInicial)
            {
                Inicial();
            }
        }
        /// <summary>
        /// Handles the Loaded event of the ImprimirOdontograma control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void ImprimirOdontograma_Loaded(object sender, RoutedEventArgs e)
        {
            if (Variables_Globales.OdontogramaInicial)
            {
                LoadInicial();
            }
            else if (Variables_Globales.Plantratamiento)
            {
                LoadPlanTratamiento();
            }
            if (Variables_Globales.Evolucion)
            {
                LoadEvolucion();
            }
        }
        /// <summary>
        /// Handles the Click event of the OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        /// <summary>
        /// Handles the Click event of the HyprlnkBttnIMprimir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void HyprlnkBttnIMprimir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Variables_Globales.OdontogramaInicial)
            {
                ImprimirInicial();
            }
            else if (Variables_Globales.Plantratamiento)
            {
                ImprimirPlanTratamiento();
            }
            else if (Variables_Globales.Evolucion)
            {
                ImprimirEvolucion();
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Plans the tratamiento.
        /// </summary>
        private void PlanTratamiento()
        {
            Report.EndPrint += (s, e) =>
            {
                this.DialogResult = true;
            };

            Report.BeginPrint += (s, e) =>
            {
                
            };

            Report.BeginBuildReport += (s, e) =>
            {
                
            };

            Report.BeginBuildReportItem += (s, e) =>
            {
                
            };

            Report.BeginBuildReportFooter += (s, e) =>
            {
                
            };
        }

        /// <summary>
        /// Plans the evolucion.
        /// </summary>
        private void PlanEvolucion()
        {
            ReportEvolucion.EndPrint += (s, e) =>
            {
                this.DialogResult = true;
            };

            ReportEvolucion.BeginPrint += (s, e) =>
            {
              
            };

            ReportEvolucion.BeginBuildReport += (s, e) =>
            {
              
            };

            ReportEvolucion.BeginBuildReportItem += (s, e) =>
            {
            };

            ReportEvolucion.BeginBuildReportFooter += (s, e) =>
            {
                
            };
        }

        /// <summary>
        /// Inicials this instance.
        /// </summary>
        private void Inicial()
        {
            ReportInicial.EndPrint += (s, e) =>
            {
                this.DialogResult = true;
            };

            ReportInicial.BeginPrint += (s, e) =>
            {
                
            };

            ReportInicial.BeginBuildReport += (s, e) =>
            {
                
            };

            ReportInicial.BeginBuildReportItem += (s, e) =>
            {
                
            };

            ReportInicial.BeginBuildReportFooter += (s, e) =>
            {
                
            };
        }

        /// <summary>
        /// Loads the plan tratamiento.
        /// </summary>
        private void LoadPlanTratamiento()
        {
            byte[] buffer = _LoadIfExists(SAVEDIMG);
            if (buffer.Length > 0)
            {
                BitmapImage Imagen = new BitmapImage();
                Imagen.SetSource(new MemoryStream((Byte[])buffer));
                ImgOdontograma.Source = Imagen;
                Report.ImagenOdontograma = new Image();
                Report.ImagenOdontograma.Source = Imagen;
            }
            else
            {

            }
        }

        /// <summary>
        /// Loads the evolucion.
        /// </summary>
        private void LoadEvolucion()
        {
            byte[] buffer = _LoadIfExists(SAVEDIMG);
            if (buffer.Length > 0)
            {
                BitmapImage Imagen = new BitmapImage();
                Imagen.SetSource(new MemoryStream((Byte[])buffer));
                ImgOdontograma.Source = Imagen;
                ReportEvolucion.ImagenOdontograma = new Image();
                ReportEvolucion.ImagenOdontograma.Source = Imagen;
            }
            else
            {

            }
        }

        /// <summary>
        /// Loads the inicial.
        /// </summary>
        private void LoadInicial()
        {
            byte[] buffer = _LoadIfExists(SAVEDIMG);
            if (buffer.Length > 0)
            {
                BitmapImage Imagen = new BitmapImage();
                Imagen.SetSource(new MemoryStream((Byte[])buffer));
                ImgOdontograma.Source = Imagen;
                ReportInicial.ImagenOdontograma = new Image();
                ReportInicial.ImagenOdontograma.Source = Imagen;
            }
            else
            {

            }
        }

        /// <summary>
        /// _s the load if exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private static byte[] _LoadIfExists(string fileName)
        {
            byte[] retVal;

            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (iso.FileExists(fileName))
                {
                    using (IsolatedStorageFileStream stream =
                    iso.OpenFile(fileName, FileMode.Open))
                    {
                        retVal = new byte[stream.Length];
                        stream.Read(retVal, 0, retVal.Length);
                    }
                }
                else
                {
                    retVal = new byte[0];
                }
            }
            return retVal;
        }

        /// <summary>
        /// Imprimirs the plan tratamiento.
        /// </summary>
        private void ImprimirPlanTratamiento()
        {
            RowDefinition Titulo = new RowDefinition();
            Titulo.Height = GridLength.Auto;
            TextBlock Texto = new TextBlock() { Text = Odontologia.Recursos.Mensajes.Plan_Tratamiento_Sin_Previo_Aviso };
            Grid Grid = new Grid() { Height = 50 };
            Grid.RowDefinitions.Add(Titulo);
            Grid.SetRow(Texto, 0);
            Grid.Children.Add(Texto);
            Report.Grid = Grid;
            Report.Imagen = Convert.ToBoolean(ChckBxImagen.IsChecked);
            Report.Texto = Convert.ToBoolean(ChckBxTexto.IsChecked);
            ProcesarImagen();
            //Report.ItemsSource = GrillaPlanTratamiento;

            

            if (Report.Imagen == false && Report.Texto == false)
            {
                //Mensaje seleccione una opcion. 
            }
            else
            {
                Report.Print();
            }
        }

        /// <summary>
        /// Imprimirs the evolucion.
        /// </summary>
        private void ImprimirEvolucion()
        {
            RowDefinition Titulo = new RowDefinition();
            Titulo.Height = GridLength.Auto;
            TextBlock Texto = new TextBlock() { Text = Odontologia.Recursos.Mensajes.Plan_Tratamiento_Sin_Previo_Aviso };
            Grid Grid = new Grid() { Height = 50 };
            Grid.RowDefinitions.Add(Titulo);
            Grid.SetRow(Texto, 0);
            Grid.Children.Add(Texto);
            ReportEvolucion.Grid = Grid;
            ReportEvolucion.Imagen = Convert.ToBoolean(ChckBxImagen.IsChecked);
            ReportEvolucion.Texto = Convert.ToBoolean(ChckBxTexto.IsChecked);
            //ReportEvolucion.ItemsSource = ProcedimientosGrillaPlanTratamiento;
            ProcesarImagen();
            

            if (ReportEvolucion.Imagen == false && Report.Texto == false)
            {
                //Mensaje seleccione una opcion. 
            }
            else
            {
                ReportEvolucion.Print();
            }
        }

        /// <summary>
        /// Imprimirs the inicial.
        /// </summary>
        private void ImprimirInicial()
        {
            RowDefinition Titulo = new RowDefinition();
            Titulo.Height = GridLength.Auto;
            TextBlock Texto = new TextBlock() { Text = Odontologia.Recursos.Mensajes.Plan_Tratamiento_Sin_Previo_Aviso };
            Grid Grid = new Grid() { Height = 50 };
            Grid.RowDefinitions.Add(Titulo);
            Grid.SetRow(Texto, 0);
            Grid.Children.Add(Texto);
            ProcesarImagen();
            ReportInicial.Grid = Grid;
            ReportInicial.Imagen = Convert.ToBoolean(ChckBxImagen.IsChecked);
            ReportInicial.Texto = Convert.ToBoolean(ChckBxTexto.IsChecked);
            ReportInicial.ItemsSource = grillaTratamiento;
            

            if (ReportInicial.Imagen == false && ReportInicial.Texto == false)
            {
                //Mensaje seleccione una opcion. 
            }
            else
            {
                ReportInicial.Print();
            }
        }

        /// <summary>
        /// Procesars the imagen.
        /// </summary>
        private void ProcesarImagen()
        {
            SalvarImagen();
            LoadInicial();
        }

        /// <summary>
        /// Salvars the imagen.
        /// </summary>
        private void SalvarImagen()
        {
            WriteableBitmap bitmap = new WriteableBitmap(this.VwbxOdontograma, new TranslateTransform());

            byte[] buffer;

            using (System.IO.Stream Source = JpgEncoder.Encode(bitmap, 70))
            {
                int bufferSize = Convert.ToInt32(Source.Length);
                buffer = new byte[bufferSize];
                Source.Read(buffer, 0, bufferSize);
                Source.Close();
            }

            _SaveToDisk(buffer, SAVEDIMG);
        }

        /// <summary>
        /// _s the save to disk.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="fileName">Name of the file.</param>
        private static void _SaveToDisk(byte[] buffer, string fileName)
        {
            using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream(fileName, System.IO.FileMode.Create, iso))
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        #endregion       
    }
}

