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
    
    public partial class manboss_servicios
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double tarifa_base_ex { get; set; }
        public double costo_minuto_ex { get; set; }
        public double costo_km_ex { get; set; }
        public double tarifa_base_co { get; set; }
        public double costo_minuto_co { get; set; }
        public double costo_km_co { get; set; }
        public string foto { get; set; }
        public int ubicaciones { get; set; }
    }
}
