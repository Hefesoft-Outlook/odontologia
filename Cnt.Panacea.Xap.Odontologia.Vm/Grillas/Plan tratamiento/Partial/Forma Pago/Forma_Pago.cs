using Cnt.Std;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cnt.Panacea.Entities.Odontologia;
using System.Collections.ObjectModel;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.General;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp;
using Microsoft.Practices.ServiceLocation;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental
{
    public partial class UserControlGuardarPlanTratamiento : ViewModelBase, IDisposable
    {
        #region Propiedades

        public string Sesiones { get; set; }

        public string CuotaInicial { get; set; }

        public string ValorCuota { get; set; }

        /// <summary>
        /// Numero sesiones tratamiento.
        /// </summary>
        /// <value>The numero sesiones tratamiento.</value>
        public short NumeroSesionesTratamiento { get; set; }

        /// <summary>
        /// Valor cuota inicial.
        /// </summary>
        /// <value>The valor cuota inicial.</value>
        public decimal ValorCuotaInicial { get; set; }
        /// <summary>
        /// Tiene cuota inicial].
        /// </summary>
        /// <value><c>true</c> if [tiene cuota inicial]; otherwise, <c>false</c>.</value>
        public bool TieneCuotaInicial { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [es cotizacion].
        /// </summary>
        /// <value><c>true</c> if [es cotizacion]; otherwise, <c>false</c>.</value>
        public bool EsCotizacion { get; set; }


        public List<KeyValueTriplet<Entities.Odontologia.FormaPago, byte, string>> FormaPagoOdontologia { get; set; }

        private KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string> formaPagoSeleccionado = new KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string>();
        public KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string> FormaPagoSeleccionado
        {
            get { return formaPagoSeleccionado; }
            set
            {
                formaPagoSeleccionado = value;
                CalculoValoresTratamiento();
                RaisePropertyChanged("FormaPagoSeleccionado");
            }
        }

        private Cnt.Panacea.Entities.Parametrizacion.ComprobanteEntity comprobanteSeleccionado;

        public ObservableCollection<int> NumeroSesiones { get; set; }

        public ObservableCollection<Cnt.Panacea.Entities.Parametrizacion.ComprobanteEntity> Comprobantes { get; set; }

        public Cnt.Panacea.Entities.Parametrizacion.ComprobanteEntity ComprobanteSeleccionado { get; set; }

        public RelayCommand CalculoValoresTratamientoCommand { get; set; }

        public bool HabilitarControlesPago { get; set; }

        /// <summary>
        /// Obtiene o establece la propiedad maximoProcedimientosSesion.
        /// </summary>
        /// <value>MaximoProcedimientosSesion.</value>
        /// LFDO Bug 16469
        public int MaximoProcedimientosSesion
        {
            get { return Variables_Globales.MaximaCantidadProcedimientosSesion; }
        }

        /// <summary>
        /// Habilitar controles pago sesion.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [habilitar controles pago sesion]; otherwise, <c>false</c>.
        /// </value>
        public bool HabilitarControlesPagoSesion { get; set; }

        public RelayCommand<string> ValorCuotaInicialTratamientoCommand { get; set; }

        public decimal ValorTotalTratamiento { get; set; }

        public decimal ValorCuotaTratamiento { get; set; }

        #endregion

        #region metodos
        private void inicializarElementosReferentesPago()
        {
            //Disparar el mensaje de carga
            Busy.UserControlCargando();
            TratamientoPadre = Variables_Globales.TratamientosPadre;

            cargarFormasPago();

            EsCotizacion = TratamientoPadre.Cotizacion;

            if (TratamientoPadre.CuotaInicial != null)
            {
                ValorCuotaInicial = (decimal)TratamientoPadre.CuotaInicial;
            }
            if (TratamientoPadre.Cuotas != null)
            {
                NumeroSesionesTratamiento = (short)TratamientoPadre.Cuotas;
            }
            if (TratamientoPadre.ValorCuota != null)
            {
                ValorCuotaTratamiento = (decimal)TratamientoPadre.ValorCuota;
            }
            if (TratamientoPadre.TipoPago != null)
            {
                FormaPagoSeleccionado = FormaPagoOdontologia.Where(a => a.NumericKey == TratamientoPadre.TipoPago).First();
            }

            Sesiones = TratamientoPadre.Sesiones.ToString();
            CuotaInicial = TratamientoPadre.CuotaInicial.ToString();
            ValorCuota = TratamientoPadre.ValorCuota.ToString();


            RaisePropertyChanged("MuestraOpcionesNuevoTratamiento");
            RaisePropertyChanged("HabilitarTipoTratamiento");
            RaisePropertyChanged("DescripcionTratamiento");
            RaisePropertyChanged("TiposTratamientoSeleccionado");
            Busy.UserControlCargando(false);
        }

        private void cargarFormasPago()
        {
            FormaPagoOdontologia = typeof(FormaPago).ToExtendedList<FormaPago, byte>();
            FormaPagoOdontologia.RemoveAt(0);
        }

        

        /// <summary>
        /// Calcula los valores del plan de tratamiento.
        /// </summary>
        public void CalculoValoresTratamiento()
        {
            // Llamar por messenger para hacer el calculo

            NumeroSesionesTratamiento = short.Parse(ListadoGrillaPlanTratamiento.Sum(a => a.NumeroSesionesProcedimiento).ToString());
            ValorTotalTratamiento = Decimal.Parse(ListadoGrillaPlanTratamiento.Where(a => a.Cobra == true).Sum(a => a.ValorPaciente).ToString());

            if (TieneCuotaInicial)
            {
                ValorCuotaTratamiento = ((ValorTotalTratamiento - ValorCuotaInicial) / NumeroSesionesTratamiento);
            }
            else
            {
                if (NumeroSesionesTratamiento > 0)
                {
                    ValorCuotaTratamiento = (ValorTotalTratamiento / NumeroSesionesTratamiento);
                }
            }

            HabilitarControlesPago = true;
            RaisePropertyChanged("NumeroSesionesTratamiento");
            RaisePropertyChanged("HabilitarControlesPago");
            RaisePropertyChanged("ValorCuotaTratamiento");
            RaisePropertyChanged("ValorTotalTratamiento");
        }

        /// <summary>
        /// Recibe la cuota inicial del plan de tratamiento
        /// </summary>
        /// <param name="CuotaInicialTexto">The cuota inicial texto.</param>
        public void CuotaInicialTratamiento(string CuotaInicialTexto)
        {
            CuotaInicialTexto = CuotaInicialTexto.Replace("$", "");
            CuotaInicialTexto = CuotaInicialTexto.Replace(",", "");
            Decimal cuota;
            if (Decimal.TryParse(CuotaInicialTexto, out cuota))
            {
                ValorCuotaInicial = Decimal.Parse(CuotaInicialTexto);
                RaisePropertyChanged("ValorCuotaInicial");
                CalculoValoresTratamiento();
            }
        }

        #endregion

    }
}
