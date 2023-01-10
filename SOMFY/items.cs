using System;

namespace SOMFY
{
    internal class items
    {
        public string SlNo { get; set; }
        public string OrdLineRef { get; set; }
        public string PrdSlNo { get; set; }
        public string PrdDesc { get; set; }
        public string HsnCd { get; set; }
        public string IsServc { get; set; }
        public string Barcde { get; set; }
        public int Qty { get; set; }
        public int FreeQty { get; set; }
        public string Unit { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal TotAmt { get; set; }
        public Decimal Discount { get; set; }
        public Decimal OthChrg { get; set; }
        public Decimal PreTaxVal { get; set; }
        public Decimal AssAmt { get; set; }
        public Decimal GstRt { get; set; }
        //public Decimal CesRt { get; set; }
        public Decimal StateCesRt { get; set; }
        public Decimal IgstAmt { get; set; }
        public Decimal CgstAmt { get; set; }
        public Decimal SgstAmt { get; set; }
        // public Decimal CesAmt { get; set; }
        public Decimal CesNonAdvlAmt { get; set; }
        public Decimal StateCesAmt { get; set; }
        public Decimal StateCesNonAdvlAmt { get; set; }
        public Decimal TotItemVal { get; set; }
        public string OrgCntry { get; set; }
        public object BchDtls;
        public object AttribDtls;
    }
}