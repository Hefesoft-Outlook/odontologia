using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Hefesoft.MenuOdontologia.Elastic.ViewModel
{
    public class Menu : ViewModelBase
    {
        public Menu()
        {
            inicializarDatos();

            if(IsInDesignMode)
            {
                
            }
            else
            {
                
            }
        }


        private void inicializarDatos()
        {
            Listado = new ObservableCollection<Entidades.Menu>();
            Listado.Add(new Entidades.Menu() 
            { 
                Codigo = 1, 
                Nombre = "Odontograma",
                Pagina =  Enumeradores.Paginas.Odontograma,
                UrlImagen = "http://docdia.com.pe/uploads/user/original/doctor_gallery_image1365359927.jpg",
                UrlMini = "http://docdia.com.pe/uploads/user/original/doctor_gallery_image1365359927.jpg",
                Descripcion = @"Un odontograma es un esquema utilizado por los odontólogos que permite registrar información sobre la boca de una persona. En dicho gráfico, el profesional detalla qué cantidad de piezas dentales permanentes tiene el paciente, cuáles han sido restauradas y otros datos de importancia.                              
                              El odontograma, de este modo, supone un registro de la historia clínica del individuo. Se trata, por lo tanto, de una herramienta de identificación. El odontólogo, al analizar el odontograma de un paciente, puede saber qué trabajos se realizaron en la boca de la persona en cuestión y realizar comparaciones entre el estado bucal actual y el registrado en la visita anterior."
            });

            Listado.Add(new Entidades.Menu()
            {
                Codigo = 2,
                Nombre = "Periodontograma",
                Pagina =  Enumeradores.Paginas.Periodontograma,
                UrlImagen = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                UrlMini = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                Descripcion = @"El periodontograma está diseñado para representar gráficamente el estado del paciente durante el examen inicial del mismo, a lo largo del tratamiento, al finalizar la terapia y en el proceso de mantenimiento. La realización del periodontograma viene acompañada por el cálculo del índice de placa y de sangrado. Su utilización permite a los dentistas en Propdental idear un pronóstico en cuanto al estado futuro del diente con tratamiento."
            });

            Listado.Add(new Entidades.Menu()
            {
                Codigo = 3,
                Nombre = "Parametrizacion diagnosticos procedimientos",
                Pagina = Enumeradores.Paginas.Parametrizacion_Diagnosticos_Procedimientos,
                UrlImagen = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                UrlMini = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                Descripcion = @"Parametrizacion de diagnosticos procedimientos."
            });

            Listado.Add(new Entidades.Menu()
            {
                Codigo = 4,
                Nombre = "Parametrizacion niveles de severidad",
                Pagina = Enumeradores.Paginas.Parametrizacion_Niveles_Severidad,
                UrlImagen = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                UrlMini = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                Descripcion = @"Parametrizacion de niveles de severidad."
            });

            Listado.Add(new Entidades.Menu()
            {
                Codigo = 5,
                Nombre = "Parametrizacion odontologos",
                Pagina = Enumeradores.Paginas.Parametrizacion_Odontologos,
                UrlImagen = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                UrlMini = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                Descripcion = @"Parametrizacion odontologos."
            });

            Listado.Add(new Entidades.Menu()
            {
                Codigo = 6,
                Nombre = "Parametrizacion higienista",
                Pagina = Enumeradores.Paginas.Parametrizacion_Higienista,
                UrlImagen = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                UrlMini = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                Descripcion = @"Parametrizacion higienista."
            });

            Listado.Add(new Entidades.Menu()
            {
                Codigo = 7,
                Nombre = "Adicionar paciente",
                Pagina = Enumeradores.Paginas.Adicionar_Paciente,
                UrlImagen = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                UrlMini = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcR7lOANLXbHFELpd-le5-lFjBwmBkxehR62-HzpFrzE1wlOL_jhOw",
                Descripcion = @"Adicionar pacientes."
            });

            ElementoSeleccionado = Listado.First();
        }


        public ObservableCollection<Entidades.Menu> Listado { get; set; }

        private Entidades.Menu elementoSeleccionado;

        public Entidades.Menu ElementoSeleccionado
        {
            get 
            { 
                return elementoSeleccionado; 
            }
            set 
            { 
                elementoSeleccionado = value; 
                RaisePropertyChanged("ElementoSeleccionado"); 
            }
        }        
    }
}
