using Cnt.Panacea.Entities.Odontologia;
using ImageUploader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace Cnt.Panacea.Xap.Odontologia.Util.Adjuntar_Archivos
{
    public class Adjuntar
    {
        /// <summary>
        /// Metodo para adjuntar imagenes a los odontogramas
        /// Esta funcionalidad se mueve a una clase ya que es una funcionalidad excepcional
        /// </summary>
        public void AdjuntarImagen(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm)
        {
            try
            {   
                vm.TratamientoImagenEntity = new ObservableCollection<TratamientoImagenEntity>();
                vm.LstImagenes = new List<TratamientoImagenEntity>();
                OpenFileDialog dlgImage = new OpenFileDialog()
                {
                    Filter = "Image files (png, jpeg)|*.jpeg;*.jpg;*.png"
                };
                if (dlgImage.ShowDialog().Value && dlgImage.File != null)

                    using (var fileStreamImage = dlgImage.File.OpenRead())
                    {
                        try
                        {
                            vm.LstImagenes = vm.TratamientoImagenEntity.ToList();
                            BinaryReader binaryReaderImage = new BinaryReader(fileStreamImage);
                            byte[] buffer;
                            WriteableBitmap bitmap;

                            using (FileStream stream = dlgImage.File.OpenRead())
                            {
                                bitmap = ImageHelper.GetImageSource(stream, 352, 240);
                            }

                            binaryReaderImage.ReadBytes((int)fileStreamImage.Length);
                            using (System.IO.Stream Source = JpgEncoder.Encode(bitmap, 70))
                            {
                                int bufferSize = Convert.ToInt32(Source.Length);
                                buffer = new byte[bufferSize];
                                Source.Read(buffer, 0, bufferSize);
                                Source.Close();
                            }

                            vm.LstImagenes.Add(new TratamientoImagenEntity()
                            {
                                Imagen = buffer,
                                Nombre = dlgImage.File.Name,
                                TipoImagen = Entities.Parametrizacion.TiposImagenes.Foto,
                                EstadoRegistro = Std.EstadosEntidad.Creado,
                                TipoMime = dlgImage.File.GetType().ToString()
                            }
                            );

                            vm.TratamientoImagenEntity.Clear();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
