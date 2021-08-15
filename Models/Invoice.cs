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
    
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            this.DetailedInvoices = new HashSet<DetailedInvoice>();
        }
    
        public int ID { get; set; }
        public Nullable<int> IDCustomer { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Status { get; set; }
        public string PayCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailedInvoice> DetailedInvoices { get; set; }
        public virtual User User { get; set; }
    }
}
