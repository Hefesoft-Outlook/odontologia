using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Hefesoft.Standard.BusyBox;
using Microsoft.Practices.ServiceLocation;
using Hefesoft.Standard.Util.Collection.Observables;
using Hefesoft.Periodontograma.Elastic.Entidades;

namespace Hefesoft.Periodontograma.Elastic.ViewModel
{
    public partial class Periodontograma : ViewModelBase
    {
        private void placaMetodonMetodo(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.Placa1 == Enumeradores.Placa.ninguno)
            {
                obj.Placa1 = Enumeradores.Placa.blue;
            }
            else if (obj.Placa1 == Enumeradores.Placa.blue)
            {
                obj.Placa1 = Enumeradores.Placa.ninguno;
            }
        }
        private void placaMetodonMetodo2(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.Placa2 == Enumeradores.Placa.ninguno)
            {
                obj.Placa2 = Enumeradores.Placa.blue;
            }
            else if (obj.Placa2 == Enumeradores.Placa.blue)
            {
                obj.Placa2 = Enumeradores.Placa.ninguno;
            }
        }
        private void placaMetodonMetodo3(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.Placa3 == Enumeradores.Placa.ninguno)
            {
                obj.Placa3 = Enumeradores.Placa.blue;
            }
            else if (obj.Placa3 == Enumeradores.Placa.blue)
            {
                obj.Placa3 = Enumeradores.Placa.ninguno;
            }
        }

        private void sangradoSupuracionMetodo(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.SangradoSupuracion1 == Enumeradores.Sangrado_Supuracion.ninguno)
            {
                obj.SangradoSupuracion1 = Enumeradores.Sangrado_Supuracion.red;
            }
            else if (obj.SangradoSupuracion1 == Enumeradores.Sangrado_Supuracion.red)
            {
                obj.SangradoSupuracion1 = Enumeradores.Sangrado_Supuracion.red_yellow;
            }
            else if (obj.SangradoSupuracion1 == Enumeradores.Sangrado_Supuracion.red_yellow)
            {
                obj.SangradoSupuracion1 = Enumeradores.Sangrado_Supuracion.ninguno;
            }
        }
        private void sangradoSupuracionMetodo2(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.SangradoSupuracion2 == Enumeradores.Sangrado_Supuracion.ninguno)
            {
                obj.SangradoSupuracion2 = Enumeradores.Sangrado_Supuracion.red;
            }
            else if (obj.SangradoSupuracion2 == Enumeradores.Sangrado_Supuracion.red)
            {
                obj.SangradoSupuracion2 = Enumeradores.Sangrado_Supuracion.red_yellow;
            }
            else if (obj.SangradoSupuracion2 == Enumeradores.Sangrado_Supuracion.red_yellow)
            {
                obj.SangradoSupuracion2 = Enumeradores.Sangrado_Supuracion.ninguno;
            }
        }
        private void sangradoSupuracionMetodo3(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.SangradoSupuracion3 == Enumeradores.Sangrado_Supuracion.ninguno)
            {
                obj.SangradoSupuracion3 = Enumeradores.Sangrado_Supuracion.red;
            }
            else if (obj.SangradoSupuracion3 == Enumeradores.Sangrado_Supuracion.red)
            {
                obj.SangradoSupuracion3 = Enumeradores.Sangrado_Supuracion.red_yellow;
            }
            else if (obj.SangradoSupuracion3 == Enumeradores.Sangrado_Supuracion.red_yellow)
            {
                obj.SangradoSupuracion3 = Enumeradores.Sangrado_Supuracion.ninguno;
            }
        }

        private void furcaMetodo2(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.Furca2 == Enumeradores.Furca.ninguno)
            {
                obj.Furca2 = Enumeradores.Furca.vacio;
            }
            else if (obj.Furca2 == Enumeradores.Furca.vacio)
            {
                obj.Furca2 = Enumeradores.Furca.mediolleno;
            }
            else if (obj.Furca2 == Enumeradores.Furca.mediolleno)
            {
                obj.Furca2 = Enumeradores.Furca.lleno;
            }
            else if (obj.Furca2 == Enumeradores.Furca.lleno)
            {
                obj.Furca2 = Enumeradores.Furca.cuadrado;
            }
            else if (obj.Furca2 == Enumeradores.Furca.cuadrado)
            {
                obj.Furca2 = Enumeradores.Furca.ninguno;
            }
        }

        private void furcaMetodo(PeriodontogramaEntity item)
        {
            var obj = (PeriodontogramaEntity)(item);

            if (obj.Furca == Enumeradores.Furca.ninguno)
            {
                obj.Furca = Enumeradores.Furca.vacio;
            }
            else if (obj.Furca == Enumeradores.Furca.vacio)
            {
                obj.Furca = Enumeradores.Furca.mediolleno;
            }
            else if (obj.Furca == Enumeradores.Furca.mediolleno)
            {
                obj.Furca = Enumeradores.Furca.lleno;
            }
            else if (obj.Furca == Enumeradores.Furca.lleno)
            {
                obj.Furca = Enumeradores.Furca.cuadrado;
            }
            else if (obj.Furca == Enumeradores.Furca.cuadrado)
            {
                obj.Furca = Enumeradores.Furca.ninguno;
            }
        }

        private void implanteMetodo(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.Implante == Enumeradores.Implante.ninguno)
            {
                obj.Tipo_Pieza = Enumeradores.Tipo_Pieza.tornillo;
                obj.Implante = Enumeradores.Implante.black;
            }
            else if (obj.Implante == Enumeradores.Implante.black)
            {
                obj.Tipo_Pieza = Enumeradores.Tipo_Pieza.normal;
                obj.Implante = Enumeradores.Implante.ninguno;
            }
        }

    }
}
