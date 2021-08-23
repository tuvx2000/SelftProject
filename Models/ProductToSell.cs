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
    
    public partial class ProductToSell
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductToSell()
        {
            this.ProductOnCarts = new HashSet<ProductOnCart>();
        }
    
        public int ID { get; set; }
        public string ProductName { get; set; }
        public Nullable<double> Price { get; set; }
        public int IDType { get; set; }
        public Nullable<int> QuanlityRemain { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Origin { get; set; }
    
        public virtual TypeOfProduct TypeOfProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOnCart> ProductOnCarts { get; set; }
    }
}