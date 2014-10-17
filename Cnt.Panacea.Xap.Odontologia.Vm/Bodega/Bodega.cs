using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Clases;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Std;
using GalaSoft.MvvmLight;
using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Cnt.Panacea.Xap.Odontologia.Assets.PopUp.vm
{
    public class Bodega : ViewModelBase
    {
        public Bodega()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                IdPaciente = Variables_Globales.IdPaciente;
                IdPx = 205;
                IdBodega = 89;
                EstadoControl = EstadosEntidad.Creado;
                AplicacionesCanasta = new AplicacionesCanastaDtoCollection();
                idBod = 89;
                IdIps = Variables_Globales.IdIps;

                RaisePropertyChanged("IdPaciente");
                RaisePropertyChanged("IdPx");
                RaisePropertyChanged("IdBodega");
                RaisePropertyChanged("EstadoControl");
                RaisePropertyChanged("AplicacionesCanasta");
                RaisePropertyChanged("idBod");
                RaisePropertyChanged("IdIps");                
            }
        }

        public int IdPaciente { get; set; }

        public int IdPx { get; set; }

        public int IdBodega { get; set; }

        public EstadosEntidad EstadoControl { get; set; }

        public int idBod { get; set; }

        public short IdIps { get; set; }

        public AplicacionesCanastaDtoCollection AplicacionesCanasta { get; set; }
    }
}
