//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace icisleriKutuphaneWeb.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBHAREKET
    {
        public int ID { get; set; }
        public string uyeTcNumarasi { get; set; }
        public string kitapDemirbas { get; set; }
        public string personelTcNumarasi { get; set; }
        public Nullable<System.DateTime> alisTarih { get; set; }
        public Nullable<System.DateTime> iadeTarih { get; set; }
        public Nullable<bool> islemDurum { get; set; }
        public Nullable<System.DateTime> uyeGetirTarih { get; set; }
    
        public virtual TBKITAP TBKITAP { get; set; }
        public virtual TBPERSONEL TBPERSONEL { get; set; }
        public virtual TBUYELER TBUYELER { get; set; }
    }
}
