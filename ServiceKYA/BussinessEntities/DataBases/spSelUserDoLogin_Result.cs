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
    
    public partial class spSelUserDoLogin_Result
    {
        public System.Guid UserId { get; set; }
        public System.Guid IdOwner { get; set; }
        public Nullable<System.Guid> IdAdmin { get; set; }
        public string NameUser { get; set; }
        public string AddressUser { get; set; }
        public string PhoneUser { get; set; }
        public string UserUser { get; set; }
        public string PasswordUser { get; set; }
        public byte[] IconUser { get; set; }
        public string IconStringUser { get; set; }
        public Nullable<System.DateTime> DateCreatedUser { get; set; }
        public Nullable<System.DateTime> DateModifiedUser { get; set; }
        public bool StatusUser { get; set; }
    }
}