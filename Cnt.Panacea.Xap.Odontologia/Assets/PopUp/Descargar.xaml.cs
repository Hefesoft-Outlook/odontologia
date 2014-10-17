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
using System.IO;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Recursos;

namespace Cnt.Panacea.Xap.Odontologia.PopUp
{
    public partial class Descargar : ChildWindow
    {
        #region Propiedades
        /// <summary>
        /// Gets or sets the imagen.
        /// </summary>
        /// <value>The imagen.</value>
        public TratamientoImagenEntity Imagen { get; set; }
        #endregion 

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Descargar"/> class.
        /// </summary>
        public Descargar()
        {            
                InitializeComponent();
                Loaded += new RoutedEventHandler(Descargar_Loaded);
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Handles the Loaded event of the Descargar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void Descargar_Loaded(object sender, RoutedEventArgs e)
        {
            if (Imagen != null)
            {
                System.Windows.Media.Imaging.BitmapImage Imagen1 = new System.Windows.Media.Imaging.BitmapImage();
                Imagen1.SetSource(new MemoryStream((Byte[])Imagen.Imagen));
                ImagenCargar.Source = Imagen1;
            }
            else
            {
                MessageBox.Show("");
            }

        }

        /// <summary>
        /// Handles the Click event of the OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();

            sd.DefaultExt = ".jpg";

            sd.Filter = "Image files (png, jpeg)|*.jpeg;*.jpg;*.png";


            bool? dialogResult = sd.ShowDialog();

            if (dialogResult == true)
            {

                byte[] contents = (byte[])Imagen.Imagen;



                using (Stream fs = (Stream)sd.OpenFile())
                {

                    fs.Write(contents, 0, contents.Length);

                    fs.Close();

                    MessageBox.Show(Mensajes.Archivo_Salvado);

                }

            }

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
        #endregion
        
    }
}

