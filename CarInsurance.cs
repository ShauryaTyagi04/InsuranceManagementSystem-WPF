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
    
    public partial class CarInsurance
    {
        public int CInsuranceID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string Registration { get; set; }
        public string License_Plate { get; set; }
        public string Council { get; set; }
        public string License_Driver { get; set; }
        public double CoverageAmount { get; set; }
        public bool IsApproverd { get; set; }
        public int PolicyID { get; set; }
    
        public virtual Policies Policies { get; set; }
    }
}
