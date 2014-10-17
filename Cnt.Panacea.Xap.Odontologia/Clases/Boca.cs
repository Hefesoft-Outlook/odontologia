using System.Collections.Generic;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;

namespace Cnt.Panacea.Xap.Odontologia.Clases
{
 /// <summary>
 /// Clase que sirve como apoyo para lograr la funcionalidad grafica de los elementos de la boca en el control Odontograma
 /// </summary>
    public class Boca : GalaSoft.MvvmLight.ViewModelBase
    {
        /// <summary>
        /// Gets or sets the superficie.
        /// </summary>
        /// <value>The superficie.</value>
        public string Superficie { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Boca"/> is activo.
        /// </summary>
        /// <value><c>true</c> if activo; otherwise, <c>false</c>.</value>
        public bool Activo { get; set; }
        /// <summary>
        /// Gets or sets the procedimientos diagnosticos boca.
        /// </summary>
        /// <value>The procedimientos diagnosticos boca.</value>
        public string ProcedimientosDiagnosticosBoca { get; set; }
        /// <summary>
        /// Gets or sets the procedimiento diagnosticos.
        /// </summary>
        /// <value>The procedimiento diagnosticos.</value>
        public List<DiagnosticoProcedimiento> ProcedimientoDiagnosticos { get; set; }
        /// <summary>
        /// Gets or sets the primer procedimiento.
        /// </summary>
        /// <value>The primer procedimiento.</value>
        public string PrimerProcedimiento { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [estado plan tratamiento presionado].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [estado plan tratamiento presionado]; otherwise, <c>false</c>.
        /// </value>
        public bool EstadoPlanTratamientoPresionado { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [estado plan tratamiento sobreescribir].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [estado plan tratamiento sobreescribir]; otherwise, <c>false</c>.
        /// </value>
        public bool EstadoPlanTratamientoSobreescribir { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [estado plan tratamiento adicionar].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [estado plan tratamiento adicionar]; otherwise, <c>false</c>.
        /// </value>
        public bool EstadoPlanTratamientoAdicionar { get; set; }
        /// <summary>
        /// Gets or sets the mostrar boca.
        /// </summary>
        /// <value>The mostrar boca.</value>
        public Visibility  MostrarBoca { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Boca"/> is convenio.
        /// </summary>
        /// <value><c>true</c> if convenio; otherwise, <c>false</c>.</value>
        public bool Convenio { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [plan tratamiento diagnostico boca].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [plan tratamiento diagnostico boca]; otherwise, <c>false</c>.
        /// </value>
        public bool PlanTratamientoDiagnosticoBoca { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [boca evolucion].
        /// </summary>
        /// <value><c>true</c> if [boca evolucion]; otherwise, <c>false</c>.</value>
        public bool BocaEvolucion { get; set; }        
    }
}
