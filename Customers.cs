//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Insurance_Management_System
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customers()
        {
            this.Policies = new HashSet<Policies>();
        }
    
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime DOB { get; set; }
        public string ContactNumber { get; set; }
        public string AlternateContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Sex { get; set; }
        public string MaritalStatus { get; set; }
        public string Nationality { get; set; }
        public string Occupation { get; set; }
        public string NationalInsurance { get; set; }
        public bool isApproved { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Policies> Policies { get; set; }
    }
}
