//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSpecialProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetailedInvoice
    {
        public int IDInvoice { get; set; }
        public int IDFood { get; set; }
        public Nullable<double> IntoMoney { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual Supplement Supplement { get; set; }
    }
}
