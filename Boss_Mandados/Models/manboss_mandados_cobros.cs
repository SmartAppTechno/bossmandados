//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Boss_Mandados.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class manboss_mandados_cobros
    {
        public int id { get; set; }
        public int mandado { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public System.DateTime tiempoInicio { get; set; }
        public Nullable<System.DateTime> tiempoFin { get; set; }
        public double distancia { get; set; }
        public double tiempo { get; set; }
    }
}
