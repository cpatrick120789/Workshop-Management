//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Repair
    {
        public Repair()
        {
            this.RepairAccessory = new HashSet<RepairAccessory>();
        }
    
        public long Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public System.DateTime Date { get; set; }
        public long IdClient { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ICollection<RepairAccessory> RepairAccessory { get; set; }
    }
}
