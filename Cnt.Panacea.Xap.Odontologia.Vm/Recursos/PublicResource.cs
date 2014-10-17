
namespace Cnt.Panacea.Xap.Odontologia
{
    /// <summary>
    /// Clase auxiliar para utilizar directamente los mensajes en XAML como un recurso local
    /// </summary>
    /// <remarks><code>
    /// *-----------------------------------------------------------------------------------------*
    /// * Copyright (C) 2008 CNT Sistemas de Información S.A., Todos los Derechos Reservados
    /// * http://www.cnt.com.co - mailto:produccion_panacea@cnt.com.co
    /// *
    /// * Archivo:      PublicResource.cs
    /// * Tipo:         Clase Auxiliar
    /// * Autor:        Jaimir Guerrero
    /// * Fecha:        2008 Mar 03
    /// * Propósito:    Clase auxiliar para utilizar directamente los mensajes en XAML como un recurso local
    /// *-----------------------------------------------------------------------------------------*
    /// </code></remarks>
    public sealed class PublicResource 
    {
        /// <summary>
        /// Inicializa una nueva instancia de la class <see cref="PublicResource"/> .
        /// </summary>
        /// 08/09/2010 by Jaimir Guerrero
        public PublicResource()
        {
            this.Mensajes = new Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes();
        }

        /// <summary>
        /// Obtener o asignar the mensajes.
        /// </summary>
        /// <value>The mensajes.</value>
        /// 08/09/2010 by Jaimir Guerrero
        public Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes Mensajes { get; private set; }

        /// <summary>
        /// Obtener el recurso de mensajes local.
        /// </summary>
        /// <value>La instancia con los mensajes.</value>
        /// 08/09/2010 by Jaimir Guerrero
        //object IPublicResource.Mensajes { get { return this.Mensajes; } }


    }
}
