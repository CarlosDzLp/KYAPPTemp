//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BussinessEntities.DataBases
{
    using System;
    
    public partial class spSelRoom_Result
    {
        public System.Guid IdRoom { get; set; }
        public System.Guid IdOwner { get; set; }
        public Nullable<System.Guid> IdAdmin { get; set; }
        public string NameRoom { get; set; }
        public Nullable<decimal> PriceRoom { get; set; }
        public Nullable<System.DateTime> DateCreatedRoom { get; set; }
        public Nullable<System.DateTime> DateModifiedRoom { get; set; }
        public int TypeStatusRoom { get; set; }
        public bool StatusRoom { get; set; }
    }
}