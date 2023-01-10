using System;
using System.Collections.Generic;

namespace SOMFY
{
    internal class inv
    {
        public String CDKey { get; set; }
        public string EInvUserName { get; set; }
        public String EInvPassword { get; set; }
        public string EFUserName { get; set; }
        public String EFPassword { get; set; }
        public string GSTIN { get; set; }
        public String GetSignedInvoice { get; set; }
        public String GetQRImg { get; set; }
        public string Version { get; set; }
        public string IRN { get; set; }
        public object TranDtls { get; set; }
        public object DocDtls { get; set; }
        public object SellerDtls { get; set; }
        public object BuyerDtls { get; set; }
        public object DispDtls { get; set; }
        public object ShipDtls { get; set; }
        public object ValDtls { get; set; }
        public object PayDtls { get; set; }
        public object RefDtls { get; set; }
        //public List<items> AddlDocDtls;
        public object ExpDtls { get; set; }
        public List<items> ItemList;
        public object EwbDtls { get; set; }
      
    }
}