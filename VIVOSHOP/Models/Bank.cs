﻿
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace VIVOSHOP.Models
{

using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Bank
{

        [DisplayName("หมายเลขบัญชี")]
        public string Bank_Number { get; set; }
        [DisplayName("ชื่อธนาคาร")]
        public string Bank_Name { get; set; }
        [DisplayName("ชื่อเจ้าของร้าน")]
        public string Bank_User { get; set; }
    }

}