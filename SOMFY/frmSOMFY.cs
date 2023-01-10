using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SOMFY
{
    public partial class frmSOMFY : Form
    {
        DataSet dsGenEWB;
        public Dictionary<string, string> errList;
        protected internal string SERVERNAME = null;
        protected internal string CDKEY = null;
        protected internal string EINVUSERNAME = null;
        protected internal string EINVPASSWORD = null;
        protected internal string EFUSERNAME = null;
        protected internal string EFPASSWORD = null;
        protected internal string SAGEDB = null;
        protected internal string SAA = null;
        protected internal string SAPSS = null;
        protected internal string SGSTIN = null;
        protected internal string BGSTIN = null;
        protected internal string VERSION = null;
        protected internal string APIURL = null;
        protected internal string FLRDATE = null;
        protected internal string apitype = null;
        public string tabname = "";
        public string CRDNOTEstr = "";
        DataSet dsLocList;
        dynamic person = new ExpandoObject();

        public frmSOMFY()
        {

            InitializeComponent();

            dsLocList = new DataSet();
            CredentialsXml();
            InvList();
            cmbtype.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            cmbreport.SelectedIndex = 0;
            dsLocList.ReadXml(@"LocationList.xml");			

        }
        public void CredentialsXml()
        {
			if (!System.IO.File.Exists("SOMFYCRD.xml"))
			{
				DueAMT.Config nn = new DueAMT.Config();
				//frmSOMFY.ActiveForm.MdiParent = nn;
				nn.ShowDialog(this);
				//XmlTextWriter writer = new XmlTextWriter(@"SOMFYCRD.xml", System.Text.Encoding.UTF8);
				//writer.WriteStartDocument(false);
				//writer.Formatting = System.Xml.Formatting.Indented;
				//writer.Indentation = 2;
				//writer.WriteStartElement("dbconfig");
				//writer.WriteStartElement("SERVERNAME");
				//writer.WriteString(".");
				//writer.WriteEndElement();
				//writer.WriteStartElement("CDKEY");
				//writer.WriteString("ADMIN");
				//writer.WriteEndElement();
				//writer.WriteStartElement("EINVUSERNAME");
				//writer.WriteString("ADMIN");
				//writer.WriteEndElement();
				//writer.WriteStartElement("EINVPASSWORD");
				//writer.WriteString("ADMIN");
				//writer.WriteEndElement();
				//writer.WriteStartElement("EFUSERNAME");
				//writer.WriteString("ADMIN");
				//writer.WriteEndElement();
				//writer.WriteStartElement("EFPASSWORD");
				//writer.WriteString("ADMIN");
				//writer.WriteEndElement();
				//writer.WriteStartElement("SAGEDB");
				//writer.WriteString("GSTMAS");
				//writer.WriteEndElement();
				//writer.WriteStartElement("SAA");
				//writer.WriteString("sa");
				//writer.WriteEndElement();
				//writer.WriteStartElement("SAPSS");
				//writer.WriteString("Erp#12345");
				//writer.WriteEndElement();
				//writer.WriteStartElement("SGSTIN");
				//writer.WriteString("EMPTY");
				//writer.WriteEndElement();
				//writer.WriteStartElement("BGSTIN");
				//writer.WriteString("EMPTY");
				//writer.WriteEndElement();
				//writer.WriteStartElement("VERSION");
				//writer.WriteString("EMPTY");
				//writer.WriteEndElement();
				//writer.WriteStartElement("APIURL");
				//writer.WriteString("EMPTY");
				//writer.WriteEndElement();
				//writer.WriteStartElement("FLRDATE");
				//writer.WriteString("EMPTY");
				//writer.WriteEndElement();
				//writer.WriteEndElement();
				//writer.WriteEndDocument();
				//writer.Close();
				ReadWriteXML xml1 = new ReadWriteXML();
				bool conStatus = xml1.ReadXML();
				if (conStatus == true)
				{
					SERVERNAME = xml1.SERVERNAME;
					CDKEY = xml1.CDKEY;
					EINVUSERNAME = xml1.EINVUSERNAME;
					EINVPASSWORD = xml1.EINVPASSWORD;
					EFUSERNAME = xml1.EFUSERNAME;
					EFPASSWORD = xml1.EFPASSWORD;
					SAGEDB = xml1.SAGEDB;
					SAA = xml1.SAA;
					SAPSS = xml1.SAPSS;
					SGSTIN = xml1.SGSTIN;
					BGSTIN = xml1.BGSTIN;
					VERSION = xml1.VERSION;
					APIURL = xml1.APIURL;
					FLRDATE = xml1.FLRDATE;
				}
			}

			else
			{
				// LocationList
				ReadWriteXML xml1 = new ReadWriteXML();
				bool conStatus = xml1.ReadXML();
				if (conStatus == true)
				{
					SERVERNAME = xml1.SERVERNAME;
					CDKEY = xml1.CDKEY;
					EINVUSERNAME = xml1.EINVUSERNAME;
					EINVPASSWORD = xml1.EINVPASSWORD;
					EFUSERNAME = xml1.EFUSERNAME;
					EFPASSWORD = xml1.EFPASSWORD;
					SAGEDB = xml1.SAGEDB;
					SAA = xml1.SAA;
					SAPSS = xml1.SAPSS;
					SGSTIN = xml1.SGSTIN;
					BGSTIN = xml1.BGSTIN;
					VERSION = xml1.VERSION;
					APIURL = xml1.APIURL;
					FLRDATE = xml1.FLRDATE;
				}
			}
        }
        public void InvList()
        {
            System.Data.SqlClient.SqlConnection conn;
            System.Data.SqlClient.SqlCommand cmd;
            DataTable dt;
            string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
            conn = new System.Data.SqlClient.SqlConnection(connectionstring);
            try
            {  
                conn.Open();
                string Querystring = "SELECT RTRIM(INVNUMBER) INVNUMBER FROM  OEINVH WHERE INVDATE>"+FLRDATE;
                cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
                cmd.CommandTimeout = 180;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter())
                {
                    DataRow dr;
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    dt = new DataTable();
                    sda.Fill(dt);
                    dr = dt.NewRow();
                    dt.Rows.InsertAt(dr, 0);
                    cmbxInvoice.DataSource = dt;
                    cmbxInvoice.ValueMember = "INVNUMBER";
                    cmbxInvoice.DisplayMember = "INVNUMBER";
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Error in Invoice List!"+ex.Message);
                conn.Close();
            }
        }
        public string checkINRNOExist(string invn)
        {
            DataSet dt;
            String strReturn = "FALSE";
            string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
            System.Data.SqlClient.SqlConnection conn;
            System.Data.SqlClient.SqlCommand cmd;
            conn = new System.Data.SqlClient.SqlConnection(connectionstring);
            conn.Open();
            string Querystring = "SELECT * FROM OEIRNO H WHERE H.INVNUMBER='" + invn + "'";
            cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
            cmd.CommandTimeout = 180;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter())
            {
                cmd.Connection = conn;
                sda.SelectCommand = cmd;
                using (dt = new DataSet())
                {
                    sda.Fill(dt);
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("IRN Number already Genereted of this Invoice!");
                        //txtEwaybillNo.Text = dt.Tables[0].Rows[0]["EWAYBILNO"].ToString();
                        //txtEWayDate.Text = dt.Tables[0].Rows[0]["EWBDATE"].ToString();
                        //txtEwatValTo.Text = dt.Tables[0].Rows[0]["EWBVLTO"].ToString();
                        string qrcode = dt.Tables[0].Rows[0]["QRCODe"].ToString();
                        string IRNNO = dt.Tables[0].Rows[0]["IRNNO"].ToString();
                        if (File.Exists(@"QR/" + invn.Replace("/", "") + ".png"))
                        {
                        }
                        else
                        {
                            listBox1.Items.Add("QR Thumb not existing, Please wait creating QR Thumb! ");
                            csQRCode objQr = new csQRCode();
                            if (qrcode != "" || qrcode != null)
                            {
                                if (objQr.createQrImage(IRNNO, invn, qrcode.ToString()) == true)
                                {
                                    listBox1.Items.Add("QR Thumb created!");
                                }

                                listBox1.Items.Add("QR Code  Not Existing! ");
                            }
                        }
                        strReturn = "TRUE";
                        conn.Close();
                    }
                    else
                        strReturn = "FALSE";
                    //listBox1.Items.Add("Error in CDT/DBT List!" + ex.Message);
                    conn.Close();
                }
            }
            return strReturn;
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if(cmbxInvoice.SelectedValue==null)
            {
                MessageBox.Show("Pasted Invoice number not exist!");
                return;
            }
            if (checkINRNOExist(cmbxInvoice.SelectedValue.ToString().Trim()) == "TRUE")
            {
                //MessageBox.Show("IRN Number already Genereted of this Invoice!");
                return;
            }

            if (checkGtnAvl(cmbxInvoice.SelectedValue.ToString().Trim()) == "FALSE")
            {
                MessageBox.Show("GSTIN not avaible of this Invoice, Please try another invoice!");
                return;
            }
            //  btnGo.Enabled = false;
            //listBox1.Items.Add("Please wait,fetching detailes.... ");
            clearControls();
            GetInvDetbyinvNo(cmbxInvoice.SelectedValue.ToString().Trim());
            //listBox1.Items.Add("Please Create IRN No on click GENERATE Button.");
            // btnGo.Enabled = true;
        }
        public string checkGtnAvl(string invn)
        {
            string strReturn = "FALSE";
            DataSet dt;
            string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
            //MessageBox.Show(connectionstring);
            //constr = "Provider=SQLOLEDB;Data Source=ERP-DATABASE; Initial Catalog=TSTDAT;User ID=sa; Password=Vspl@4321"
            //string connectionstring = "Data Source=ERP-DATABASE; Initial Catalog=TSTDAT; User ID=sa; Password=Vspl@4321;";
            System.Data.SqlClient.SqlConnection conn;
            System.Data.SqlClient.SqlCommand cmd;
            conn = new System.Data.SqlClient.SqlConnection(connectionstring);
            conn.Open();
            string Querystring = "";
            if (comboBox1.SelectedItem.ToString()=="INV")
            {  Querystring = "select ar.value from arcuso ar inner join oeinvh h on h.CUSTOMER=ar.IDCUST where ar.optfield='gstin' and h.invnumber='" + invn + "'"; }
            else
            {
                 Querystring = "select ar.value from arcuso ar inner join OECRDH H on h.CUSTOMER=ar.IDCUST where ar.optfield='gstin' and H.CRDNUMBER='" + invn + "'";
            }
            

            cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
            cmd.CommandTimeout = 180;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter())
            {
                cmd.Connection = conn;
                sda.SelectCommand = cmd;
                using (dt = new DataSet())
                {
                    sda.Fill(dt);
                    if (dt.Tables[0].Rows.Count > 0) { 
                    String ss = dt.Tables[0].Rows[0]["value"].ToString().Trim();
                    if (string.IsNullOrEmpty(ss))
                        return "FALSE";
                    int lntg = ss.Length;
                    if (lntg >= 3 & lntg <= 15)
                    {
                            strReturn= "TRUE";
                            conn.Close();
                        }
                    else
                            strReturn = "FALSE";
                        conn.Close();
                    }
                }
            }
            return strReturn;
        }
        public void clearControls()
        {
            // txtDocType.Text = dsGenEWB.Tables[0].Rows[0]["Typ"].ToString();
            txtSupplyType.Text = string.Empty;
            //Seller Detail
            SGst.Text = string.Empty;
            sname.Text = string.Empty;
            sState.Text = string.Empty;
            SrichTextBox1.Text = string.Empty;
            SCity.Text = string.Empty;


            //Buyer Detail
            Bgst.Text = string.Empty;
            Bname.Text = string.Empty;
            Bstate.Text = string.Empty;
            BrichTextBox2.Text = string.Empty;
            BCity.Text = string.Empty;

            //Shipment Detail
            SHCity.Text = string.Empty;
            SHToPin.Text = string.Empty;
            SHState.Text = string.Empty;
            SHrichTextBox4.Text = string.Empty;
            SHPTO.Text = string.Empty;

            txtTotASSVal.Text = string.Empty;
            txtTotGST.Text = string.Empty;
            txtTotInvVal.Text = string.Empty;
        }
        public void GetInvDetbyinvNo(string strInvoice)
        {
            string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
            System.Data.SqlClient.SqlConnection conn;
            System.Data.SqlClient.SqlCommand cmd;
            conn = new System.Data.SqlClient.SqlConnection(connectionstring);
            conn.Open();
            string Querystring = "  SELECT  'GST' AS TAXSCH,CASE WHEN H1.VALUE='R' THEN 'B2B' WHEN  H1.VALUE='SEWP' THEN 'SEZWP' WHEN  H1.VALUE='SEWOP' THEN 'SEZWOP'  ";
            Querystring += "  WHEN  H1.VALUE='EXPWP' THEN 'EXPWP' WHEN  H1.VALUE='EXPWOP' THEN 'EXPWOP' WHEN  H1.VALUE='DE' THEN 'DEXP'  ";
            Querystring += "  ELSE 'B2B' END SUPTYP,'N' REGREV,'INV' TYP,RTRIM(H.INVNUMBER) DOCUMENTNO,H.INVDATE INVOICEDATE,  ";
            Querystring += "  RTRIM((SELECT VALUE FROM  CSOPTFD WHERE OPTFIELD='GSTNOS' AND LEFT(VALUE,2)=LEFT(H.TAXGROUP,2))) SUPPLIER_GSTIN,  ";
            Querystring += "  (SELECT RTRIM(CONAME) FROM  CSCOM) SUPPLIER_LEGAL_NAME,  (SELECT RTRIM(ADDRESS1) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_ADDRESS1,  ";
            Querystring += "  (SELECT RTRIM(ADDRESS2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_ADDRESS2,(SELECT RTRIM(CITY) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_LOC,  ";
            Querystring += "  (SELECT RTRIM(ZIP) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_PIN,(SELECT LEFT(H.TAXGROUP,2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_STATE,  ";
            Querystring += "  CASE WHEN H1.VALUE IN ('EXPWP','EXPWOP') THEN 'URP' ELSE RTRIM(AR.VALUE) END BUYERGSTIN, (SELECT RTRIM(VALUE) FROM ARCuso WHERE  OPTFIELD='GSTCODE' AND RTRIM(IDCUST)=RTRIM(H.CUSTOMER)) BILLING_STCD ,  ";
            Querystring += "  RTRIM(H.BILNAME) BUYERNAME,RTRIM(ISNULL((SELECT VALUE FROM  OEINVHO WHERE INVUNIQ=H.INVUNIQ AND OPTFIELD='GPOS'),'')) BILLING_POS,  ";
            Querystring += "  RTRIM(H.BILADDR1) BUYER_ADDRESS1,RTRIM(H.BILADDR2) BUYER_ADDRESS2,RTRIM(H.BILCITY) BUYER_LOC,RTRIM(H.BILZIP) BILLING_PINCODE,  ";
            Querystring += "  (SELECT RTRIM(CONAME) FROM  CSCOM) DISPATCH_NAME,(SELECT RTRIM(ADDRESS1) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_ADDRESS1,  ";
            Querystring += "  (SELECT RTRIM(ADDRESS2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_ADDRESS2,(SELECT RTRIM(CITY) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_LOC,  ";
            Querystring += "  (SELECT RTRIM(ZIP) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_PIN,(SELECT LEFT(H.TAXGROUP,2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_STATE,  ";
            Querystring += "  RTRIM(H.SHPNAME) SHIPTONAME,RTRIM(H.SHPADDR1) SHIPTOADDR1,RTRIM(H.SHPADDR2) SHIPTOADDR2,RTRIM(H.SHPCITY) LOC,RTRIM(H.SHPZIP) SHIPTO_PIN,  ";
            // Querystring += "  CASE WHEN H1.VALUE IN ('EXPWP','EXPWOP') THEN '97' ELSE (CASE WHEN H.SHIPTO='' THEN LEFT(AR.VALUE,2) ELSE (SELECT RTRIM(VALUE) FROM ARCSPO WHERE RTRIM(IDCUSTSHPT)=H.SHIPTO AND OPTFIELD='GSTCODE' AND RTRIM(IDCUST)=H.CUSTOMER) END)  END SHIPTO_STCD,  ";
            Querystring += "  CASE  WHEN H1.VALUE IN ('EXWP','EXWOP') THEN (SELECT RTRIM(VALUE) FROM ARCUSO WHERE  OPTFIELD='GSTCODE' AND RTRIM(IDCUST)=RTRIM(H.CUSTOMER) ) WHEN H.SHIPTO='' THEN LEFT(AR.VALUE,2)  ELSE (SELECT RTRIM(VALUE) FROM ARCSPO  WHERE RTRIM(IDCUSTSHPT)=H.SHIPTO AND OPTFIELD='GSTCODE' AND RTRIM(IDCUST)=RTRIM(H.CUSTOMER)) END SHIPTO_STCD, ";
            Querystring += "  ROW_NUMBER() OVER(PARTITION BY D.INVUNIQ ORDER BY D.INVUNIQ) SLNO,  ISNULL((SELECT RTRIM(VDESC) FROM CSOPTFD WHERE OPTFIELD='GUOM' AND RTRIM([VALUE])=D.INVUNIT),'OTH') UOM, ";
            // Querystring += "  CASE  WHEN ISNULL((SELECT VALUE FROM  ICITEMO WHERE ITEMNO=I.ITEMNO AND OPTFIELD='GITEMTYPE'),'N')='S' THEN 'Y'  ELSE 'N' END ISSERVC,  ";
            //Querystring += " CASE  WHEN D.LINETYPE=1 AND ISNULL((SELECT VALUE FROM  ICITEMO WHERE ITEMNO=I.ITEMNO AND OPTFIELD='GITEMTYPE'),( CASE WHEN ISNULL((SELECT VALUE FROM  OEMISCO  WHERE MISCCHARGE=D.MISCCHARGE AND D.LINETYPE=2   AND OPTFIELD='GITEMTYPE'),'N')='S' THEN 'Y' END))='S' THEN 'Y'    ELSE 'N' END ISSERVC , ";
            Querystring += "  CASE  WHEN D.LINETYPE=1 AND  ISNULL((SELECT VALUE FROM  ICITEMO WHERE ITEMNO=I.ITEMNO AND OPTFIELD='GITEMTYPE'),'N')='S' THEN 'Y'  WHEN D.LINETYPE=2 AND ISNULL((SELECT VALUE FROM  OEMISCO  WHERE MISCCHARGE=D.MISCCHARGE AND OPTFIELD='GITEMTYPE' AND CURRENCY=H.INSOURCURR),'N')='S' THEN 'Y' ELSE 'N' END ISSERVC,  ";

            Querystring += "  ISNULL((SELECT SUBSTRING(VALUE,1,4) VALUE FROM  OEINVDO WHERE INVUNIQ=D.INVUNIQ AND LINENUM=D.LINENUM AND OPTFIELD='GHSNCODE'),'') HSNCD,  ";
            Querystring += "  D.UNITPRICE UNITPRICE, D.QTYSHIPPED QUANTITY, D.EXTINVMISC TOTAMT , D.EXTINVMISC PRETAXVAL, D.INVDISC DISCOUNT, ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3) IN ('IGN', 'CGN') THEN D.TBASE1	 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN D.TBASE1+D.TBASE2   ";
            Querystring += "  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN D.TBASE1+D.TBASE2 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGX' AND H1.VALUE IN ('SEWP','SEWOP') THEN D.TBASE1 ELSE 0 END ASSAMT,  ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3) IN ('IGN', 'IGM') THEN D.TRATE1	 WHEN SUBSTRING(H.TAXGROUP,3,3)IN ('CGN','CGM') THEN (D.TRATE1*2) ELSE 0 END GSTRT,  ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='IGN' THEN D.TAMOUNT1 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN D.TAMOUNT1+D.TAMOUNT2 ELSE 0 END IGSTAMT,  ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN D.TAMOUNT1  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN D.TAMOUNT1+D.TAMOUNT2 ELSE 0 END CGSTAMT,  ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN D.TAMOUNT2 WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN D.TAMOUNT3+D.TAMOUNT4 ELSE 0 END SGSTAMT,  ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN  D.TAMOUNT3	 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGN' THEN  D.TAMOUNT2  ";
            Querystring += "  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN  D.TAMOUNT3   WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN  D.TAMOUNT5 ELSE 0 END OTHCHRG,  ";
            Querystring += "  (D.EXTINVMISC-D.INVDISC) + D.TAMOUNT1+D.TAMOUNT2+D.TAMOUNT3+D.TAMOUNT4+D.TAMOUNT5 TOTITEMVAL,  ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='IGN' THEN H.TBASE1  WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN H.TBASE1+H.TBASE2  ";
            Querystring += "  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TBASE1  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TBASE1+H.TBASE2 	 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGX' AND H1.VALUE IN ('SEWP','SEWOP') THEN H.TBASE1  ELSE 0 END ASSVAL,  ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='IGN' THEN H.TEAMOUNT1	 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN H.TEAMOUNT1+H.TEAMOUNT2 ELSE 0 END IGSTVAL,  ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TEAMOUNT1	 WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TEAMOUNT1+H.TEAMOUNT2 ELSE 0 END CGSTVAL,  ";
            Querystring += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TEAMOUNT2	 WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TEAMOUNT3+H.TEAMOUNT4 ELSE 0 END SGSTVAL,  ";

            Querystring += " ( CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='IGN' THEN H.TEAMOUNT1	 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN H.TEAMOUNT1+H.TEAMOUNT2 ELSE 0 END)+  ";
            Querystring += " ( CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TEAMOUNT1	 WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TEAMOUNT1+H.TEAMOUNT2 ELSE 0 END)+  ";
            Querystring += " ( CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TEAMOUNT2	 WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TEAMOUNT3+H.TEAMOUNT4 ELSE 0 END ) TOTGST,  ";
            
            Querystring += "  ISNULL((SELECT VALUE FROM  OEINVHO WHERE INVUNIQ=D.INVUNIQ AND LINENUM=D.LINENUM AND OPTFIELD='GSTTR'),'') TRANSID,H.INVNETWTX TOTINVVAL, ISNULL((SELECT SUM(D1.EXTINVMISC) EXTINVMISC FROM OEINVD D1 WHERE D.INVUNIQ=D1.INVUNIQ AND D1.MISCCHARGE='DISC'),0)*-1 HDISCOUNT FROM OEINVH H   ";
            Querystring += "  LEFT OUTER JOIN OEINVHO H1 ON H.INVUNIQ=H1.INVUNIQ AND RTRIM(H1.OPTFIELD)='GINVTYPE'  ";
            Querystring += "  LEFT OUTER JOIN OEINVD D ON H.INVUNIQ=D.INVUNIQ  ";
            Querystring += "  LEFT OUTER JOIN ICITEM I ON D.ITEM=I.FMTITEMNO LEFT OUTER JOIN ICITEMO O ON I.ITEMNO=O.ITEMNO AND O.OPTFIELD='HSNCODE'   ";
            Querystring += "  LEFT OUTER JOIN ARCUSO AR ON H.CUSTOMER=AR.IDCUST AND AR.OPTFIELD='GSTIN'      ";
            Querystring += "  WHERE SUBSTRING(H.TAXGROUP,3,3) IN('IGN','CGN','IGM','CGM','IGX') AND D.EXTINVMISC>=0 AND H.INVNUMBER='" + cmbxInvoice.SelectedValue.ToString() + "'   ";

            string QuerystringCRN = " SELECT  'GST' AS TAXSCH,CASE WHEN H1.VALUE='R' THEN 'B2B' WHEN  H1.VALUE='SEWP' THEN 'SEZWP' WHEN  H1.VALUE='SEWOP' THEN 'SEZWOP'    ";
            QuerystringCRN += " WHEN  H1.VALUE='EXWP' THEN 'EXPWP' WHEN  H1.VALUE='EXWOP' THEN 'EXPWOP' WHEN  H1.VALUE='DE' THEN 'DEXP'  ";
            QuerystringCRN += " ELSE 'B2B' END SUPTYP,'N' REGREV,'CRN' TYP,RTRIM(H.CRDNUMBER) DOCUMENTNO,H.CRDDATE INVOICEDATE,    ";
            QuerystringCRN += "  RTRIM((SELECT [VALUE] FROM  CSOPTFD WHERE OPTFIELD='GSTNOS' AND LEFT([VALUE],2)=LEFT(H.TAXGROUP,2))) SUPPLIER_GSTIN,   ";
            QuerystringCRN += "  (SELECT RTRIM(CONAME) FROM  CSCOM) SUPPLIER_LEGAL_NAME,  ";
            QuerystringCRN += " (SELECT RTRIM(CITY) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_LOC,   ";
            QuerystringCRN += " (SELECT RTRIM(ADDRESS1)+' '+RTRIM(ADDRESS2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_ADDRESS1, ";
            QuerystringCRN += " (SELECT RTRIM(ADDRESS3)+' '+RTRIM(ADDRESS4) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_ADDRESS2,   ";
            QuerystringCRN += " (SELECT LEFT(H.TAXGROUP,2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_STATE,   ";
            QuerystringCRN += " (SELECT RTRIM(ZIP) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_PIN,   ";

            QuerystringCRN += " ISNULL((SELECT VALUE FROM  OECRDHO WHERE CRDUNIQ=H.CRDUNIQ AND OPTFIELD='GINVTYPE'),'') INVOICE_TYPE_CODE,   ";
            QuerystringCRN += " CASE WHEN SUBSTRING(H.TAXGROUP,5,1)='R' THEN 'Y' ELSE 'N' END REVERSECHARGE,'CRN' INVOICE_SUBTYPE_CODE,RTRIM(H.INVNUMBER) INVOICENUM,H.INVNUMBER PRECEEDING_INVOICE_NUMBER,  ";
            QuerystringCRN += " SUBSTRING(CAST(INVH.INVDATE AS CHAR),7,2)+'-'+SUBSTRING(CAST(INVH.INVDATE AS CHAR),5,2)+'-'+SUBSTRING(CAST(INVH.INVDATE AS CHAR),1,4) PRECEEDING_INVOICE_DATE,  ";
            QuerystringCRN += " RTRIM(H.BILNAME) BUYERNAME, RTRIM(ISNULL((SELECT VALUE FROM  OECRDHO WHERE CRDUNIQ=H.CRDUNIQ AND OPTFIELD='GPOS'),'')) BILLING_POS,  ";
            QuerystringCRN += " AR.VALUE BUYERGSTIN, H.BILCITY BUYER_LOC, RTRIM(ISNULL((SELECT VALUE FROM  OECRDHO WHERE CRDUNIQ=H.CRDUNIQ AND OPTFIELD='GPOS'),'')) BILLING_STATE, (SELECT RTRIM(VALUE) FROM ARCuso WHERE  OPTFIELD='GSTCODE' AND RTRIM(IDCUST)=RTRIM(H.CUSTOMER)) BILLING_STCD ,  ";
            QuerystringCRN += " RTRIM(H.BILADDR1) BUYER_ADDRESS1, RTRIM(H.BILADDR2) BUYER_ADDRESS2,RTRIM(H.BILZIP) BILLING_PINCODE,  ";

            QuerystringCRN += " (SELECT RTRIM(CONAME) FROM  CSCOM) DISPATCH_NAME,(SELECT RTRIM(ADDRESS1) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_ADDRESS1,  ";
            QuerystringCRN += " (SELECT RTRIM(ADDRESS2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_ADDRESS2,  ";
            QuerystringCRN += " (SELECT RTRIM(CITY) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_LOC,  ";
            QuerystringCRN += " (SELECT RTRIM(ZIP) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_PIN,  ";
            QuerystringCRN += " (SELECT LEFT(H.TAXGROUP,2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_STATE,  ";

            QuerystringCRN += " RTRIM(H.SHPNAME) SHIPTONAME,RTRIM(H.SHPADDR1) SHIPTOADDR1,RTRIM(H.SHPADDR2) SHIPTOADDR2,RTRIM(H.SHPCITY) LOC,RTRIM(H.SHPZIP) SHIPTO_PIN,  ";
            QuerystringCRN += " CASE WHEN H1.VALUE IN('EXWP','EXWOP') THEN(SELECT RTRIM(VALUE) FROM ARCUSO WHERE  OPTFIELD = 'GSTCODE' AND RTRIM(IDCUST) = RTRIM(H.CUSTOMER)) WHEN H.SHIPTO = '' THEN LEFT(AR.VALUE,2)  ELSE(SELECT RTRIM(VALUE) FROM ARCSPO  WHERE RTRIM(IDCUSTSHPT) = H.SHIPTO AND OPTFIELD = 'GSTCODE' AND RTRIM(IDCUST) = RTRIM(H.CUSTOMER)) END SHIPTO_STCD, ";
            QuerystringCRN += " CASE WHEN SUBSTRING(H.TAUTH1 ,3,3)='CGN' THEN H.TEAMOUNT1 ELSE 0 END CGSTVAL,  CASE WHEN SUBSTRING(H.TAUTH2 ,3,3)='SGN' THEN H.TEAMOUNT2 ELSE 0 END SGSTVAL,    ";
            QuerystringCRN += " CASE WHEN SUBSTRING(H.TAUTH1 ,3,3)='IGN' THEN H.TEAMOUNT1 ELSE 0 END IGSTVAL,  CASE WHEN SUBSTRING(H.TAUTH2 ,3,3)='CEN' THEN H.TEAMOUNT2 ELSE   ";
            QuerystringCRN += " CASE WHEN SUBSTRING(H.TAUTH3 ,3,3)='CEN' THEN H.TEAMOUNT3  ELSE 0 END END CESSVALUE, CASE WHEN SUBSTRING(D.TAUTH2 ,4,3)='TCS' THEN D.TAMOUNT2 ELSE CASE WHEN SUBSTRING(D.TAUTH3 ,4,3)='TCS' THEN D.TAMOUNT3  ELSE 	0 END END OTHCHRG,0 ROUNDOFF,  H.CRDNETWTX TOTINVVAL,  ";
            QuerystringCRN += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='IGN' THEN H.TBASE1  WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN H.TBASE1+H.TBASE2  ";
            QuerystringCRN += "  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TBASE1  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TBASE1+H.TBASE2 	 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGX' AND H1.VALUE IN ('SEWP','SEWOP') THEN H.TBASE1  ELSE 0 END ASSVAL,  ";
            QuerystringCRN += " 0 VAL_FOR_CUR,ROW_NUMBER() OVER(PARTITION BY D.CRDUNIQ ORDER BY D.CRDUNIQ) SLNO,   ";
            QuerystringCRN += " ISNULL((SELECT RTRIM(VDESC) FROM CSOPTFD WHERE OPTFIELD='GUOM' AND RTRIM([VALUE])=D.INVUNIT),'OTH') UOM,    ";
            // QuerystringCRN += "  CASE  WHEN ISNULL((SELECT VALUE FROM  ICITEMO WHERE ITEMNO=I.ITEMNO AND OPTFIELD='GITEMTYPE'),'N')='S' THEN 'Y' WHEN ISNULL((SELECT VALUE FROM  OEMISCO CO WHERE CO.MISCCHARGE=D.MISCCHARGE  AND  CO.CURRENCY=H.CRSOURCURR AND OPTFIELD='GITEMTYPE'),'N')='S' THEN 'Y'  ELSE 'N' END ISSERVC,  ";
            QuerystringCRN += "  CASE  WHEN D.LINETYPE=1 AND  ISNULL((SELECT VALUE FROM  ICITEMO WHERE ITEMNO=I.ITEMNO AND OPTFIELD='GITEMTYPE'),'N')='S' THEN 'Y'  WHEN D.LINETYPE=2 AND ISNULL((SELECT VALUE FROM  OEMISCO  WHERE MISCCHARGE=D.MISCCHARGE AND OPTFIELD='GITEMTYPE' AND CURRENCY=H.INSOURCURR),'N')='S' THEN 'Y' ELSE 'N' END ISSERVC,  ";
            QuerystringCRN += " ISNULL((SELECT RTRIM(VALUE) VALUE FROM  OECRDDO WHERE CRDUNIQ=D.CRDUNIQ AND LINENUM=D.LINENUM AND OPTFIELD='GHSNCODE'),'') HSNCD,  ";
            QuerystringCRN += " D.UNITPRICE UNITPRICE,D.QTYSHIPPED QUANTITY,D.EXTCRDMISC GROSSAMOUNT, D.CRDDISC+D.HDRDISC DISCOUNT,  D.EXTCRDMISC PRETAXVAL,  ";
            QuerystringCRN += " ( CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='IGN' THEN H.TEAMOUNT1 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN H.TEAMOUNT1+H.TEAMOUNT2 ELSE 0 END)+   ";
            QuerystringCRN += " ( CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TEAMOUNT1 WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TEAMOUNT1+H.TEAMOUNT2 ELSE 0 END)+   ";
            QuerystringCRN += " ( CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TEAMOUNT2 WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TEAMOUNT3+H.TEAMOUNT4 ELSE 0 END ) TOTGST, ";
            QuerystringCRN += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3) IN ('IGN','CGN') THEN D.TBASE1 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN D.TBASE1+D.TBASE2   ";
            QuerystringCRN += "  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN D.TBASE1+D.TBASE2 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGX' AND H1.VALUE IN ('SEWP','SEWOP') THEN D.TBASE1 ELSE 0 END ASSAMT,  ";
            QuerystringCRN += " CASE WHEN SUBSTRING(D.TAUTH1 ,3,3)='IGN' THEN D.TRATE1 ELSE 0  END IGST_RT, D.EXTCRDMISC TOTAMT, ";
            QuerystringCRN += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3) IN ('IGN', 'IGM') THEN D.TRATE1 WHEN SUBSTRING(H.TAXGROUP,3,3)IN ('CGN','CGM') THEN (D.TRATE1*2) ELSE 0 END GSTRT,    ";
            QuerystringCRN += " CASE WHEN SUBSTRING(D.TAUTH2 ,3,3)='CEN' THEN D.TRATE2 ELSE CASE WHEN SUBSTRING(D.TAUTH3 ,3,3)='CEN' THEN D.TRATE3  ELSE 0 END END CESS_RT,   ";
            QuerystringCRN += " CASE WHEN SUBSTRING(D.TAUTH1 ,3,3)='IGN' THEN D.TAMOUNT1 ELSE 0 END IGSTAMT, CASE WHEN SUBSTRING(D.TAUTH1 ,3,3)='CGN' THEN D.TAMOUNT1 ELSE 0 END CGSTAMT,  ";
            QuerystringCRN += "  CASE WHEN SUBSTRING(D.TAUTH2 ,3,3)='SGN' THEN D.TAMOUNT2 ELSE 0 END SGSTAMT, CASE WHEN SUBSTRING(D.TAUTH2 ,3,3)='CEN' THEN D.TAMOUNT2 ELSE   ";
            QuerystringCRN += " CASE WHEN SUBSTRING(D.TAUTH3 ,3,3)='CEN' THEN D.TAMOUNT3  ELSE 0 END END CSAMT,D.TBASE1+D.TAMOUNT1+D.TAMOUNT2+D.TAMOUNT3 TOTITEMVAL, ISNULL((SELECT SUM(D1.EXTCRDMISC) EXTINVMISC FROM OECRDD D1 WHERE D.CRDUNIQ=D1.CRDUNIQ AND D1.MISCCHARGE='DINR'),0)*-1 HDISCOUNT  ";
            QuerystringCRN += " FROM  OECRDH H LEFT OUTER JOIN OECRDHO H1 ON H.CRDUNIQ=H1.CRDUNIQ AND RTRIM(H1.OPTFIELD)='GINVTYPE'   ";
            QuerystringCRN += " LEFT OUTER JOIN OECRDD D ON H.CRDUNIQ=D.CRDUNIQ  LEFT OUTER JOIN OECRDH INVH ON H.CRDNUMBER=INVH.CRDNUMBER  ";
            QuerystringCRN += " LEFT OUTER JOIN ICITEM I ON D.ITEM=I.FMTITEMNO    ";
            QuerystringCRN += " LEFT OUTER JOIN ICITEMO O ON I.ITEMNO=O.ITEMNO AND O.OPTFIELD='HSNCODE'   ";
            QuerystringCRN += " LEFT OUTER JOIN ARCUSO AR ON H.CUSTOMER=AR.IDCUST AND AR.OPTFIELD='GSTIN'   ";
            QuerystringCRN += " WHERE SUBSTRING(H.TAXGROUP,3,3) IN('IGN','CGN','IGM','CGM','IGX') AND D.EXTCRDMISC>=0 AND  H.CRDNUMBER='" + cmbxInvoice.SelectedValue.ToString() + "'  ";


            string QuerystringDBN = " SELECT  'GST' AS TAXSCH,CASE WHEN H1.VALUE='R' THEN 'B2B' WHEN  H1.VALUE='SEWP' THEN 'SEZWP' WHEN  H1.VALUE='SEWOP' THEN 'SEZWOP'    ";
            QuerystringDBN += " WHEN  H1.VALUE='EXWP' THEN 'EXPWP' WHEN  H1.VALUE='EXWOP' THEN 'EXPWOP' WHEN  H1.VALUE='DE' THEN 'DEXP'  ";
            QuerystringDBN += " ELSE 'B2B' END SUPTYP,'N' REGREV,'CRN' TYP,RTRIM(H.CRDNUMBER) DOCUMENTNO,H.CRDDATE INVOICEDATE,    ";
            QuerystringDBN += "  RTRIM((SELECT [VALUE] FROM  CSOPTFD WHERE OPTFIELD='GSTNOS' AND LEFT([VALUE],2)=LEFT(H.TAXGROUP,2))) SUPPLIER_GSTIN,   ";
            QuerystringDBN += "  (SELECT RTRIM(CONAME) FROM  CSCOM) SUPPLIER_LEGAL_NAME,  ";
            QuerystringDBN += " (SELECT RTRIM(CITY) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_LOC,   ";
            QuerystringDBN += " (SELECT RTRIM(ADDRESS1)+' '+RTRIM(ADDRESS2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_ADDRESS1, ";
            QuerystringDBN += " (SELECT RTRIM(ADDRESS3)+' '+RTRIM(ADDRESS4) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_ADDRESS2,   ";
            QuerystringDBN += " (SELECT LEFT(H.TAXGROUP,2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_STATE,   ";
            QuerystringDBN += " (SELECT RTRIM(ZIP) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) SUPPLIER_PIN,   ";

            QuerystringDBN += " ISNULL((SELECT VALUE FROM  OECRDHO WHERE CRDUNIQ=H.CRDUNIQ AND OPTFIELD='GINVTYPE'),'') INVOICE_TYPE_CODE,   ";
            QuerystringDBN += " CASE WHEN SUBSTRING(H.TAXGROUP,5,1)='R' THEN 'Y' ELSE 'N' END REVERSECHARGE,'DBN' INVOICE_SUBTYPE_CODE,RTRIM(H.INVNUMBER) INVOICENUM,H.INVNUMBER PRECEEDING_INVOICE_NUMBER,  ";
            QuerystringDBN += " SUBSTRING(CAST(INVH.INVDATE AS CHAR),7,2)+'-'+SUBSTRING(CAST(INVH.INVDATE AS CHAR),5,2)+'-'+SUBSTRING(CAST(INVH.INVDATE AS CHAR),1,4) PRECEEDING_INVOICE_DATE,  ";
            QuerystringDBN += " RTRIM(H.BILNAME) BUYERNAME, RTRIM(ISNULL((SELECT VALUE FROM  OECRDHO WHERE CRDUNIQ=H.CRDUNIQ AND OPTFIELD='GPOS'),'')) BILLING_POS,  ";
            QuerystringDBN += " AR.VALUE BUYERGSTIN, H.BILCITY BUYER_LOC, RTRIM(ISNULL((SELECT VALUE FROM  OECRDHO WHERE CRDUNIQ=H.CRDUNIQ AND OPTFIELD='GPOS'),'')) BILLING_STATE, (SELECT RTRIM(VALUE) FROM ARCuso WHERE  OPTFIELD='GSTCODE' AND RTRIM(IDCUST)=RTRIM(H.CUSTOMER)) BILLING_STCD , ";
            QuerystringDBN += " RTRIM(H.BILADDR1) BUYER_ADDRESS1, RTRIM(H.BILADDR2) BUYER_ADDRESS2,RTRIM(H.BILZIP) BILLING_PINCODE,  ";

            QuerystringDBN += " (SELECT RTRIM(CONAME) FROM  CSCOM) DISPATCH_NAME,(SELECT RTRIM(ADDRESS1) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_ADDRESS1,  ";
            QuerystringDBN += " (SELECT RTRIM(ADDRESS2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_ADDRESS2,  ";
            QuerystringDBN += " (SELECT RTRIM(CITY) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_LOC,  ";
            QuerystringDBN += " (SELECT RTRIM(ZIP) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_PIN,  ";
            QuerystringDBN += " (SELECT LEFT(H.TAXGROUP,2) FROM ICLOC A1 WHERE A1.LOCATION=H.LOCATION) DESPATCH_STATE,  ";

            QuerystringDBN += " RTRIM(H.SHPNAME) SHIPTONAME,RTRIM(H.SHPADDR1) SHIPTOADDR1,RTRIM(H.SHPADDR2) SHIPTOADDR2,RTRIM(H.SHPCITY) LOC,RTRIM(H.SHPZIP) SHIPTO_PIN,  ";
            QuerystringDBN += " CASE WHEN H1.VALUE IN('EXWP','EXWOP') THEN(SELECT RTRIM(VALUE) FROM ARCUSO WHERE  OPTFIELD = 'GSTCODE' AND RTRIM(IDCUST) = RTRIM(H.CUSTOMER)) WHEN H.SHIPTO = '' THEN LEFT(AR.VALUE,2)  ELSE(SELECT RTRIM(VALUE) FROM ARCSPO  WHERE RTRIM(IDCUSTSHPT) = H.SHIPTO AND OPTFIELD = 'GSTCODE' AND RTRIM(IDCUST) = RTRIM(H.CUSTOMER)) END SHIPTO_STCD,  ";
            QuerystringDBN += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='IGN' THEN H.TBASE1  WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN H.TBASE1+H.TBASE2  ";
            QuerystringDBN += "  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TBASE1  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TBASE1+H.TBASE2 	 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGX' AND H1.VALUE IN ('SEWP','SEWOP') THEN H.TBASE1  ELSE 0 END ASSVAL,  ";

            QuerystringDBN += " CASE WHEN SUBSTRING(H.TAUTH1 ,3,3)='CGN' THEN H.TEAMOUNT1 ELSE 0 END CGSTVAL,  CASE WHEN SUBSTRING(H.TAUTH2 ,3,3)='SGN' THEN H.TEAMOUNT2 ELSE 0 END SGSTVAL,    ";
            QuerystringDBN += " CASE WHEN SUBSTRING(H.TAUTH1 ,3,3)='IGN' THEN H.TEAMOUNT1 ELSE 0 END IGSTVAL,  CASE WHEN SUBSTRING(H.TAUTH2 ,3,3)='CEN' THEN H.TEAMOUNT2 ELSE   ";
            QuerystringDBN += " CASE WHEN SUBSTRING(H.TAUTH3 ,3,3)='CEN' THEN H.TEAMOUNT3  ELSE 0 END END CESSVALUE,  CASE WHEN SUBSTRING(D.TAUTH2 ,4,3)='TCS' THEN D.TAMOUNT2 ELSE CASE WHEN SUBSTRING(D.TAUTH3 ,4,3)='TCS' THEN D.TAMOUNT3  ELSE 	0 END END OTHCHRG,0 ROUNDOFF,  H.CRDNETWTX TOTINVVAL,  ";
            QuerystringDBN += " 0 VAL_FOR_CUR,ROW_NUMBER() OVER(PARTITION BY D.CRDUNIQ ORDER BY D.CRDUNIQ) SLNO,   ";
            QuerystringDBN += " ISNULL((SELECT RTRIM(VDESC) FROM CSOPTFD WHERE OPTFIELD='GUOM' AND RTRIM([VALUE])=D.INVUNIT),'OTH') UOM,    ";
            // QuerystringDBN += " CASE  WHEN ISNULL((SELECT VALUE FROM  ICITEMO WHERE ITEMNO=I.ITEMNO AND OPTFIELD='GITEMTYPE'),'N')='S' THEN 'Y'  ELSE 'N' END ISSERVC,  ";
            QuerystringDBN += "  CASE  WHEN D.LINETYPE=1 AND  ISNULL((SELECT VALUE FROM  ICITEMO WHERE ITEMNO=I.ITEMNO AND OPTFIELD='GITEMTYPE'),'N')='S' THEN 'Y'  WHEN D.LINETYPE=2 AND ISNULL((SELECT VALUE FROM  OEMISCO  WHERE MISCCHARGE=D.MISCCHARGE AND OPTFIELD='GITEMTYPE' AND CURRENCY=H.INSOURCURR),'N')='S' THEN 'Y' ELSE 'N' END ISSERVC,  ";
            QuerystringDBN += " ISNULL((SELECT RTRIM(VALUE) VALUE FROM  OECRDDO WHERE CRDUNIQ=D.CRDUNIQ AND LINENUM=D.LINENUM AND OPTFIELD='GHSNCODE'),'') HSNCD,  ";
            QuerystringDBN += " D.UNITPRICE UNITPRICE,D.QTYSHIPPED QUANTITY,D.EXTCRDMISC GROSSAMOUNT, D.CRDDISC+D.HDRDISC DISCOUNT,   D.EXTCRDMISC PRETAXVAL, ";
            QuerystringDBN += " ( CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='IGN' THEN H.TEAMOUNT1 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN H.TEAMOUNT1+H.TEAMOUNT2 ELSE 0 END)+   ";
            QuerystringDBN += " ( CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TEAMOUNT1  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TEAMOUNT1+H.TEAMOUNT2 ELSE 0 END)+   ";
            QuerystringDBN += " ( CASE WHEN SUBSTRING(H.TAXGROUP,3,3)='CGN' THEN H.TEAMOUNT2 WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN H.TEAMOUNT3+H.TEAMOUNT4 ELSE 0 END ) TOTGST, ";
            QuerystringDBN += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3) IN ('IGN', 'CGN') THEN D.TBASE1 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGM' THEN D.TBASE1+D.TBASE2   ";
            QuerystringDBN += "  WHEN SUBSTRING(H.TAXGROUP,3,3)='CGM' THEN D.TBASE1+D.TBASE2 WHEN SUBSTRING(H.TAXGROUP,3,3)='IGX' AND H1.VALUE IN ('SEWP','SEWOP') THEN D.TBASE1 ELSE 0 END ASSAMT,  ";
            QuerystringDBN += " CASE WHEN SUBSTRING(D.TAUTH1 ,3,3)='IGN' THEN D.TRATE1 ELSE 0  END IGST_RT,  D.EXTCRDMISC TOTAMT, ";
            QuerystringDBN += "  CASE WHEN SUBSTRING(H.TAXGROUP,3,3) IN ('IGN', 'IGM') THEN D.TRATE1	 WHEN SUBSTRING(H.TAXGROUP,3,3)IN ('CGN','CGM') THEN (D.TRATE1*2) ELSE 0 END GSTRT,    ";
            QuerystringDBN += " CASE WHEN SUBSTRING(D.TAUTH2 ,3,3)='CEN' THEN D.TRATE2 ELSE CASE WHEN SUBSTRING(D.TAUTH3 ,3,3)='CEN' THEN D.TRATE3  ELSE 0 END END CESS_RT,   ";
            QuerystringDBN += " CASE WHEN SUBSTRING(D.TAUTH1 ,3,3)='IGN' THEN D.TAMOUNT1 ELSE 0 END IGSTAMT, CASE WHEN SUBSTRING(D.TAUTH1 ,3,3)='CGN' THEN D.TAMOUNT1 ELSE 0 END CGSTAMT,  ";
            QuerystringDBN += "  CASE WHEN SUBSTRING(D.TAUTH2 ,3,3)='SGN' THEN D.TAMOUNT2 ELSE 0 END SGSTAMT, CASE WHEN SUBSTRING(D.TAUTH2 ,3,3)='CEN' THEN D.TAMOUNT2 ELSE   ";
            QuerystringDBN += " CASE WHEN SUBSTRING(D.TAUTH3 ,3,3)='CEN' THEN D.TAMOUNT3  ELSE 0 END END CSAMT,D.TBASE1+D.TAMOUNT1+D.TAMOUNT2+D.TAMOUNT3 TOTITEMVAL , ISNULL((SELECT SUM(D1.EXTCRDMISC) EXTINVMISC FROM OECRDD D1 WHERE D.CRDUNIQ=D1.CRDUNIQ AND D1.MISCCHARGE='DISC'),0)*-1 HDISCOUNT   ";
            QuerystringDBN += " FROM  OECRDH H LEFT OUTER JOIN OECRDHO H1 ON H.CRDUNIQ=H1.CRDUNIQ AND RTRIM(H1.OPTFIELD)='GINVTYPE'   ";
            QuerystringDBN += " LEFT OUTER JOIN OECRDD D ON H.CRDUNIQ=D.CRDUNIQ  LEFT OUTER JOIN OECRDH INVH ON H.CRDNUMBER=INVH.CRDNUMBER  ";
            QuerystringDBN += " LEFT OUTER JOIN ICITEM I ON D.ITEM=I.FMTITEMNO    ";
            QuerystringDBN += " LEFT OUTER JOIN ICITEMO O ON I.ITEMNO=O.ITEMNO AND O.OPTFIELD='HSNCODE'   ";
            QuerystringDBN += " LEFT OUTER JOIN ARCUSO AR ON H.CUSTOMER=AR.IDCUST AND AR.OPTFIELD='GSTIN'   ";
            QuerystringDBN += " WHERE SUBSTRING(H.TAXGROUP,3,3) IN('IGN','CGN','IGM','CGM','IGX') AND D.EXTCRDMISC>=0 AND  H.CRDNUMBER='" + cmbxInvoice.SelectedValue.ToString() + "'  ";
            string strQry = "";
            if (comboBox1.SelectedItem.ToString() == "INV")
                strQry = Querystring;
            else if(comboBox1.SelectedItem.ToString() == "CRN")
                strQry = QuerystringCRN;
            else if(comboBox1.SelectedItem.ToString() == "DBN")
                strQry = QuerystringDBN;
            else
            {
                listBox1.Items.Add("Please select document type!");
                return;
            }
            try
            {
                cmd = new System.Data.SqlClient.SqlCommand(strQry, conn);
                //cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
                cmd.CommandTimeout = 180;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter())
                {
                    var objerrList = (IDictionary<string, object>)person;
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    using (dsGenEWB = new DataSet())
                    {
                        int linenumber = 0;
                        sda.Fill(dsGenEWB);
                        if (dsGenEWB.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in dsGenEWB.Tables[0].Rows)
                            {
                                errList = new Dictionary<string, string>();
                                linenumber++;
                                CheckValidation(row);
                                if (errList.Count != 0)
                                {
                                    objerrList.Add(linenumber.ToString(), errList);
                                }
                            }
                            if (objerrList.Count != 0)
                            {
                                var serializationResult = JsonConvert.SerializeObject(objerrList);
                                listBox1.Items.Add("Validation failed!");
                                listBox1.Items.Add(serializationResult.ToString());
                                // webBrowser1.DocumentText = "INVOICE DETAIL NOT FOUND! OR FOUND ERROR! " + serializationResult.ToString();
                            }
                            else
                            {
                                // txtDocType.Text = dsGenEWB.Tables[0].Rows[0]["Typ"].ToString();
                                txtSupplyType.Text = dsGenEWB.Tables[0].Rows[0]["SupTyp"].ToString();
                                //Seller Detail
                                SGst.Text = dsGenEWB.Tables[0].Rows[0]["supplier_GSTIN"].ToString();
                                sname.Text = dsGenEWB.Tables[0].Rows[0]["supplier_Legal_Name"].ToString();
                                sState.Text = dsGenEWB.Tables[0].Rows[0]["supplier_State"].ToString();
                                SrichTextBox1.Text = dsGenEWB.Tables[0].Rows[0]["supplier_Address1"].ToString()+ " " + dsGenEWB.Tables[0].Rows[0]["supplier_Address2"].ToString();
                                SCity.Text = dsGenEWB.Tables[0].Rows[0]["supplier_Loc"].ToString();


                                //Buyer Detail
                                Bgst.Text = dsGenEWB.Tables[0].Rows[0]["buyerGSTIN"].ToString();
                                Bname.Text = dsGenEWB.Tables[0].Rows[0]["buyerName"].ToString();
                                Bstate.Text = dsGenEWB.Tables[0].Rows[0]["BILLING_STCD"].ToString();
                                BrichTextBox2.Text = dsGenEWB.Tables[0].Rows[0]["buyer_Address1"].ToString()+" "+ dsGenEWB.Tables[0].Rows[0]["buyer_Address2"].ToString();
                                BCity.Text = dsGenEWB.Tables[0].Rows[0]["buyer_loc"].ToString();

                                //Shipment Detail
                                SHCity.Text = dsGenEWB.Tables[0].Rows[0]["LOC"].ToString();
                                SHToPin.Text = dsGenEWB.Tables[0].Rows[0]["SHIPTO_PIN"].ToString();
                                SHState.Text = dsGenEWB.Tables[0].Rows[0]["shipto_stcd"].ToString();
                                SHrichTextBox4.Text = dsGenEWB.Tables[0].Rows[0]["shiptoAddr1"].ToString()+" "+ dsGenEWB.Tables[0].Rows[0]["shiptoAddr2"].ToString();
                                SHPTO.Text = dsGenEWB.Tables[0].Rows[0]["shiptoName"].ToString();

                                txtTotASSVal.Text = dsGenEWB.Tables[0].Rows[0]["ASSVAL"].ToString();
                                txtTotGST.Text = dsGenEWB.Tables[0].Rows[0]["TOTGST"].ToString();
                                txtTotInvVal.Text = dsGenEWB.Tables[0].Rows[0]["TotInvVal"].ToString();
                                conn.Close();
                            }
                        }
                        else
                        {
                            listBox1.Items.Add("Details not found against this invoice, Please try another invoice!");
                            conn.Close();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Something wrong!");
            }
          
            conn.Close();
        }
        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        public void CheckValidation(DataRow row)
        {
            if (row["supplier_GSTIN"].ToString().Length == 15)//* mandatory
            { }
            else
            {
                errList.Add("supplier_GSTIN", row["supplier_GSTIN"].ToString());
            }
            if (row["supplier_Legal_Name"].ToString().Length >= 3 && row["supplier_Legal_Name"].ToString().Length <= 100)//* mandatory
            { }
            else
            {
                errList.Add("supplier_Legal_Name", row["supplier_Legal_Name"].ToString());
            }
            if (row["SUPPLIER_LOC"].ToString().Length >= 3 && row["SUPPLIER_LOC"].ToString().Length <= 50)//* mandatory
            { }
            else
            {
                errList.Add("SUPPLIER_LOC", row["SUPPLIER_LOC"].ToString());
            }
            if (row["supplier_Address1"].ToString().Length >= 3 && row["supplier_Address1"].ToString().Length <= 100)//* mandatory
            { }
            else
            {
                errList.Add("supplier_Address1", row["supplier_Address1"].ToString());
            }
            if (row["supplier_State"].ToString().Length <= 2)//* mandatory
            { }
            else
            {
                errList.Add("supplier_State", row["supplier_State"].ToString());
            }
            if (row["SUPPLIER_PIN"].ToString().Length == 6)//* mandatory
            {
                if (IsNumeric(row["SUPPLIER_PIN"].ToString()) == false) {
                    errList.Add("SUPPLIER_PIN", row["SUPPLIER_PIN"].ToString());
                    listBox1.Items.Add("SUPPLIER_PIN+" + row["SUPPLIER_PIN"].ToString());
                }
            }
            else
            {
                errList.Add("SUPPLIER_PIN", row["SUPPLIER_PIN"].ToString());
                listBox1.Items.Add("SUPPLIER_PIN+" +row["SUPPLIER_PIN"].ToString());
            }

            //Shiping
            if (row["SHIPTO_PIN"].ToString().Length == 6)//* mandatory
            { if (IsNumeric(row["SHIPTO_PIN"].ToString()) == false) {
                    errList.Add("SHIPTO_PIN", row["SHIPTO_PIN"].ToString());
                    listBox1.Items.Add("SHIPTO_PIN+" + row["SHIPTO_PIN"].ToString());
                }
            }
            else
            {
                errList.Add("SHIPTO_PIN", row["SHIPTO_PIN"].ToString());
                listBox1.Items.Add("SHIPTO_PIN+" + row["SHIPTO_PIN"].ToString());
            }

            //if (row["supplier_Phone"].ToString().Length == 15)
            //{
            //}
            //if (row["supplier_Email"].ToString().Length == 15)
            //{
            //}
            //if (row["supplier_Address2"].ToString().Length<=3)
            //{
            //}
            //if (row["supplier_trading_name"].ToString().Length <= 3)
            //{
            //}
            ///transaction_details
            //if (row["transactionMode"].ToString().Length == 3)//* mandatory
            // { }
            // else
            // {
            //  errList.Add("transactionMode", row["transactionMode"].ToString());
            // }
            // if (row["invoice_type_code"].ToString().Trim().Length >= 3 && row["invoice_type_code"].ToString().Trim().Length <= 10)//* mandatory
            // { }
            //  else
            // {
            //   errList.Add("invoice_type_code", row["invoice_type_code"].ToString());
            //  }
            //if (row["reversecharge"].ToString().Length == 1)//* mandatory
            //{ }
            //else
            //{
            //    errList.Add("reversecharge", row["reversecharge"].ToString());
            //}
            //if (row["ecom_GSTIN"].ToString().Length == 15)//* mandatory
            //{
            //}
            //if (row["IgstOnIntra"].ToString().Length == 15)
            //{
            //}

            //document_details
            //if (row["invoice_subtype_code"].ToString().Length == 3)//* mandatory
            //{ }
            //else
            //{
            //    errList.Add("invoice_subtype_code", row["invoice_subtype_code"].ToString());
            //}
            if (row["DocumentNo"].ToString().Length >= 1 && row["DocumentNo"].ToString().Length <= 16)//* mandatory
            { }
            else
            {
                listBox1.Items.Add("DocumentNo=" + row["DocumentNo"].ToString());
                errList.Add("DocumentNo", row["DocumentNo"].ToString());
            }
            //if (row["invoiceDate"].ToString().Length == 10)//* mandatory
            //{ }
            //else
            //{
            //    errList.Add("invoiceDate", row["invoiceDate"].ToString());
            //}
            //if (row["transaction_id"].ToString().Length == 15)
            //{
            //}
            //if (row["plant"].ToString().Length == 15)
            //{
            //}
            //if (row["custom"].ToString().Length == 15)
            //{
            //}

            //export_details

            //if (row["shipping_bill_no"].ToString().Length == 15)
            //{
            //}
            //if (row["shipping_bill_date"].ToString().Length == 15)
            //{
            //}
            //if (row["port_code"].ToString().Length == 15)
            //{
            //}
            //if (row["invoice_currency_code"].ToString().Length == 15)
            //{
            //}
            //if (row["cnt_code"].ToString().Length == 15)
            //{
            //}
            //if (row["RefClm"].ToString().Length == 15)
            //{
            //}
            //if (row["ExpDuty"].ToString().Length == 15)
            //{
            //}

            //extra_Information 

            //if (row["remarks"].ToString().Length == 15)
            //{
            //}
            //if (row["invoice_Period_Start_Date"].ToString().Length == 15)
            //{
            //}
            //if (row["invoice_Period_End_Date"].ToString().Length == 15)
            //{
            //}
            if (tabname == "CREDITNOTE")
            {
                //    if (row["preceeding_Invoice_Number"].ToString().Trim().Length <= 20)
                //    {
                //        //MessageBox.Show("True"+ row["preceeding_Invoice_Number"].ToString());
                //    }
                //    else
                //    {
                //        // MessageBox.Show("false" + row["preceeding_Invoice_Number"].ToString());
                //        errList.Add("preceeding_Invoice_Number", row["preceeding_Invoice_Number"].ToString());
                //    }
                //    if (row["preceeding_Invoice_Date"].ToString().Length == 10)
                //    { }
                //    else { errList.Add("preceeding_Invoice_Date", row["preceeding_Invoice_Date"].ToString()); }
            }
            //if (row["invoice_Document_Reference"].ToString().Length == 15)
            //{
            //}
            //if (row["receipt_Advice_ReferenceNo"].ToString().Length == 15)
            //{
            //}
            //if (row["receipt_Advice_ReferenceDt"].ToString().Length == 15)
            //{
            //}
            //if (row["tender_or_Lot_Reference"].ToString().Length == 15)
            //{
            //}
            //if (row["contract_Reference"].ToString().Length == 15)
            //{
            //}
            //if (row["external_Reference"].ToString().Length == 15)
            //{
            //}
            //if (row["project_Reference"].ToString().Length == 15)
            //{
            //}
            //if (row["refNum"].ToString().Length == 15)
            //{
            //}
            //if (row["refDate"].ToString().Length == 15)
            //{
            //}
            //if (row["Url"].ToString().Length == 15)
            //{
            //}
            //if (row["Docs"].ToString().Length == 15)
            //{
            //}
            //if (row["Info"].ToString().Length == 15)
            //{
            //}

            //billing_Information/buyer information

            if (row["buyerName"].ToString().Length >= 3 && row["buyerName"].ToString().Length <= 100)//* mandatory
            { }
            else
            {
                errList.Add("buyerName", row["buyerName"].ToString());
            }
            if (row["buyerGSTIN"].ToString().Length >= 3 && row["buyerGSTIN"].ToString().Length <= 15)//* mandatory
            { }
            else
            {
                //errList.Add("billing_GSTIN", row["buyerGSTIN"].ToString());
            }
            if (row["billing_POS"].ToString().Length >= 1 && row["billing_POS"].ToString().Length <= 2)//* mandatory
            { }
            else
            {
                listBox1.Items.Add("billing_POS=" + row["billing_POS"].ToString());
                errList.Add("billing_POS", row["billing_POS"].ToString());
            }
            if (row["buyer_loc"].ToString().Length >= 3 && row["buyer_loc"].ToString().Length <= 100)//* mandatory
            { }
            else
            {
                listBox1.Items.Add("buyer_loc=" + row["buyer_loc"].ToString());
                errList.Add("buyer_loc", row["buyer_loc"].ToString());
            }
            //if (row["billing_State"].ToString().Length >= 1 && row["billing_State"].ToString().Length <= 2)//* mandatory
            //{ }
            //else
            //{
            //    errList.Add("billing_State", row["billing_State"].ToString());
            //}
            if (row["buyer_Address1"].ToString().Length >= 3 && row["buyer_Address1"].ToString().Length <= 100)//* mandatory
            { }
            else
            {
                listBox1.Items.Add("buyer_Address1=" + row["buyer_Address1"].ToString());
                errList.Add("buyer_Address1", row["buyer_Address1"].ToString());
            }
            if (row["BILLING_PINCODE"].ToString().Length == 6)//* mandatory
            { if (IsNumeric(row["BILLING_PINCODE"].ToString()) == false) { errList.Add("BILLING_PINCODE", row["BILLING_PINCODE"].ToString());
                    listBox1.Items.Add("BILLING_PINCODE+" + row["BILLING_PINCODE"].ToString());
                } }
            else
            {
                listBox1.Items.Add("BILLING_PINCODE+" + row["BILLING_PINCODE"].ToString());
                errList.Add("BILLING_PINCODE", row["BILLING_PINCODE"].ToString());
            }
            //Dispatch
            if (row["DESPATCH_PIN"].ToString().Length == 6)//* mandatory
            { if (IsNumeric(row["DESPATCH_PIN"].ToString()) == false) { errList.Add("DESPATCH_PIN", row["DESPATCH_PIN"].ToString());
                    listBox1.Items.Add("DESPATCH_PIN+" + row["DESPATCH_PIN"].ToString());
                } }
            else
            {
                listBox1.Items.Add("DESPATCH_PIN+" + row["DESPATCH_PIN"].ToString());
                errList.Add("DESPATCH_PIN", row["DESPATCH_PIN"].ToString());
            }
            //document_Total
            if (Convert.ToDouble(row["assVal"].ToString()) <= 99999999999999.99)//* mandatory
            { }
            else
            {
                listBox1.Items.Add("assVal=" + row["assVal"].ToString());
                errList.Add("total_assVal", row["assVal"].ToString());
            }
            //if (row["roundoff"].ToString().Length == 15)//* mandatory
            //{
            //    errList.Add("roundoff", row["roundoff"].ToString());
            //}
            if (Convert.ToDouble(row["TotInvVal"].ToString()) <= 99999999999999.99)//* mandatory
            { }
            else
            {
                errList.Add("TotInvVal", row["TotInvVal"].ToString());
                listBox1.Items.Add("TotInvVal="+ row["TotInvVal"].ToString());
            }

            //if (row["cgstvalue"].ToString().Length == 15)//optional
            //{
            //}
            //if (row["sgstvalue"].ToString().Length == 15)//optional
            //{
            //}
            //if (row["igstvalue"].ToString().Length == 15)//optional
            //{
            //}
            //if (row["cessvalue"].ToString().Length == 15)//optional
            //{
            //}
            //if (row["stateCessValue"].ToString().Length == 15)//optional
            //{
            //}
            //if (row["Discount"].ToString().Length == 15)//optional
            //{
            //}
            //if (row["OthChrg"].ToString().Length == 15)//optional
            //{
            //}
            //if (row["val_for_cur"].ToString().Length == 15)//optional
            //{
            //}

            //items           
            if (row["slno"].ToString().Length >= 1 && row["slno"].ToString().Length <= 6)//* mandatory
            { }
            else
            {

                errList.Add("slno", row["slno"].ToString());
            }
            //if (row["service"].ToString().Length == 1)//* mandatory
            //{ }
            //else
            //{
            //    errList.Add("service", row["service"].ToString());
            //}
            if (row["hsncd"].ToString().Trim().Length >= 4 && row["hsncd"].ToString().Trim().Length <= 8)//* mandatory
            { }
            else
            {
                listBox1.Items.Add("hsncd=" + row["hsncd"].ToString());
                errList.Add("hsncd", row["hsncd"].ToString().Trim());
            }
            if (Convert.ToDouble(row["GstRt"].ToString()) <= 999999999999.99)//* mandatory
            { }
            else
            {
                listBox1.Items.Add("GstRt=" + row["GstRt"].ToString());
                errList.Add("GstRt", row["GstRt"].ToString());
            }

            if (Convert.ToDouble(row["assVal"].ToString()) <= 999999999999.99)//* mandatory
            { }
            else
            {
                listBox1.Items.Add("assVal=" + row["assVal"].ToString());
                errList.Add("assesseebleValue", row["assVal"].ToString());
            }
            //if (Convert.ToDouble(row["igst_rt"].ToString()) <= 999.999)//* mandatory
            //{ }
            //else
            //{
            //    errList.Add("igst_rt", row["igst_rt"].ToString());
            //}
            //if (Convert.ToDouble(row["cgst_rt"].ToString()) <= 999.999)//* mandatory
            //{ }
            //else
            //{
            //    errList.Add("cgst_rt", row["cgst_rt"].ToString());
            //}
            //if (Convert.ToDouble(row["sgst_rt"].ToString()) <= 999.999)//* mandatory
            //{
            //}
            //else
            //{
            //    errList.Add("sgst_rt", row["sgst_rt"].ToString());
            //}
            //if (Convert.ToDouble(row["cess_rt"].ToString()) <= 999.999)//* mandatory
            //{ }
            //else
            //{
            //    errList.Add("cess_rt", row["cess_rt"].ToString());
            //}
            //if (row["otherCharges"].ToString().Length == 15)//* mandatory
            //{
            //    errList.Add("otherCharges", row["otherCharges"].ToString());
            //}
            if (Convert.ToDouble(row["TOTITEMVAL"].ToString()) <= 999999999999.99)//* mandatory
            { }
            else
            {
                listBox1.Items.Add("TOTITEMVAL=" + row["TOTITEMVAL"].ToString());
                errList.Add("TOTITEMVAL", row["TOTITEMVAL"].ToString());
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (cmbreport.Text == "")
            { MessageBox.Show("Please select Report!"); return; }

            listBox1.Items.Add("Please wait..... ");
			if (dsGenEWB.Tables[0].Rows.Count > 0)
			{
				CreateJsonString(dsGenEWB.Tables[0]);
				dsGenEWB.Tables[0].Rows.Clear();
				dsGenEWB.Clear();
			}
			else
			{ MessageBox.Show("Please click on Go Button !"); return; }

        }
        public string convertDate(string sagedate)
        {
            string year = sagedate.Substring(0, 4);
            string month = sagedate.Substring(4, 2);
            string daye = sagedate.Substring(6, 2);
            string dates = daye + '/' + month + '/' + year;
            return dates;
        }

        public void CreateJsonString(DataTable crJsonTb)
        {
            try
            {
                var objMain = (IDictionary<string, object>)person;
                if (crJsonTb.Rows.Count > 0)
                {
                    List<inv> mainJson = new List<inv>();
                    int HeadeIndx = 0;
                    inv objinv = new inv();
                    foreach (DataRow dr in crJsonTb.Rows)
                    {
                        // GenueUserRow = userlist.Select(userid + " AND " + password).CopyToDataTable();
                        string strCompanyGSt = "CompanyGST='" + dr["supplier_GSTIN"].ToString().Trim() + "'";
                        DataTable tbUsrPass = dsLocList.Tables["USERTB"].Select(strCompanyGSt).CopyToDataTable();
                        if (HeadeIndx == 0)
                        {
                            string bvv = "";
                            if (SGSTIN == "EMPTY")
                               bvv = dr["supplier_GSTIN"].ToString().Trim(); 
                            //bvv= "29AAACW3775F000";                           
                            else bvv = SGSTIN;
                            string sts = "";
                            sts = dr["supplier_State"].ToString().Trim();
                            int pncd;
                            pncd = Convert.ToInt32(dr["supplier_Pin"].ToString().Replace(" ", ""));

                            listBox1.Items.Add("Please wait, your request in proccess...");

                            objinv.CDKey = CDKEY;
                            //listBox1.Items.Add(CDKEY);
                            //listBox1.Items.Add(tbUsrPass.Rows[0]["UserName"].ToString());
                            //listBox1.Items.Add(tbUsrPass.Rows[0]["Password"].ToString());
                            objinv.EInvUserName = tbUsrPass.Rows[0]["UserName"].ToString();
                            objinv.EInvPassword = tbUsrPass.Rows[0]["Password"].ToString();
                            //objinv.EInvUserName = EINVUSERNAME;
                            //objinv.EInvPassword = EINVPASSWORD;
                            objinv.EFUserName = EFUSERNAME;
                            objinv.EFPassword = EFPASSWORD;
                            objinv.GSTIN = bvv;
                            objinv.GetQRImg = "1";
                            objinv.GetSignedInvoice = "1";
                            objinv.Version = VERSION;
                            objinv.IRN = "";
                            var SellerDtls = new
                            {
                                Gstin = bvv,//dr["supplier_GSTIN"].ToString(); // "06AACCC1596Q002",
                                LglNm = dr["supplier_Legal_Name"].ToString().Trim(),
                                //TrdNm= "VSPL",
                                Loc = dr["Loc"].ToString().Trim(),
                                Addr1 = dr["supplier_Address1"].ToString().Trim(),
                                Addr2 = dr["supplier_Address2"].ToString().Trim(),
                                Stcd =  sts, //"29";//dr["supplier_State"].ToString();
                                Pin = pncd
                                // "560087";// dr["Pinc"].ToString();
                                // Ph = "9810885187",
                                //Em= "Supplier@inv.com"
                            };
                            //"Pin": 380004,
                            //"Stcd": "24",
                            objinv.SellerDtls = SellerDtls;
                            HeadeIndx++;

                            var transaction = new
                            {
                                TaxSch = dr["TaxSch"].ToString().Trim(),
                                SupTyp = dr["suptyp"].ToString().Trim(),// "B2B",
                                RegRev = dr["RegRev"].ToString().Trim() //"N",/
                                                                        //EcmGstin = "",
                                                                        // IgstOnIntra = "N"
                            };
                            objinv.TranDtls = transaction;
                            string binv = "";
                            binv = dr["DocumentNo"].ToString().Trim();

                            string sqlFormattedDate = convertDate(dr["invoiceDate"].ToString().Trim());
                            
                            var document = new
                            {
                                Typ = dr["Typ"].ToString().Trim(),
                                No = binv,// "INV21/00180",//binv,//"INV21/00190",//b  //"IRN2-3306500027"
                                Dt = sqlFormattedDate//dr["invoiceDate"].ToString().Trim().ToString(yyyy-MM-dd)   // "01/11/2020",

                            };
                            objinv.DocDtls = document;

                            //var export = new
                            //{
                            //    RefClm = "Y",
                            //    ShipBNo = "BLNO932083",
                            //    ShipBDt = "11/12/2020",
                            //    Port = "INABG1",
                            //    ForCur = "USD",
                            //    CntCode = "IN",
                            //    ExpDuty = 1
                            //};
                            //objinv.ExpDtls = export;

                            string bGst = "";
                            bGst = dr["buyerGSTIN"].ToString().Trim();

                            string bstate = "";
                            bstate = dr["BILLING_STCD"].ToString().Trim(); //shipment state code


                            var BuyerDtls = new
                            {
                                LglNm = dr["buyerName"].ToString().Trim(),
                                Gstin = bGst,//"29AABCS0858G1Z9",//dr["billing_GSTIN"].ToString(),
                                Pos = dr["billing_POS"].ToString().Trim(),
                                Loc = dr["buyer_loc"].ToString().Trim(),
                                Stcd = bstate,//"29",//dr["billing_State"].ToString(),
                                Addr1 = dr["buyer_Address1"].ToString().Trim(),
                                Addr2 = dr["buyer_Address2"].ToString().Trim(),
                                Pin = Convert.ToInt32(dr["BILLING_PINCODE"].ToString())
                                // Ph = "9999999999",
                                //  Em = "billing@go4gst.com"
                            };
                            objinv.BuyerDtls = BuyerDtls;

                            var shipping = new
                            {
                                LglNm = dr["shiptoName"].ToString().Trim(),
                                //TrdNm = dr["SHIPTO"].ToString().Trim(),
                                Loc = dr["LOC"].ToString().Trim(),
                                Stcd = dr["shipto_stcd"].ToString().Trim(),//bstate,
                                Addr1 = dr["shiptoAddr1"].ToString().Trim(),
                                Addr2 = dr["shiptoAddr2"].ToString().Trim(),
                                Pin = Convert.ToInt32(dr["SHIPTO_PIN"].ToString().Trim()),
                            };
                            objinv.ShipDtls = shipping;

                            //delivery_Information/Dispatch from information
                            var delivery = new
                            {
                                Nm = dr["DISPATCH_NAME"].ToString().Trim(),//"VSPL",
                                Loc = dr["DESPATCH_LOC"].ToString().Trim(),//"Gurgaon",
                                Stcd = dr["DESPATCH_STATE"].ToString().Trim(),// "37",
                                Addr1 = dr["DESPATCH_ADDRESS1"].ToString().Trim(),// "M22, Old DLF,Sector-14",
                                Addr2 = dr["DESPATCH_ADDRESS2"].ToString().Trim(),
                                Pin = Convert.ToInt32(dr["DESPATCH_PIN"].ToString().Trim()),//515001
                            };
                            objinv.DispDtls = delivery;


                            //var payee = new
                            //{
                            //    Nm = "Pay name",
                            //    Mode = "Cash",
                            //    FinInsBr = "ABCD1234567",
                            //    PayTerm = "Debit",
                            //    PayInstr = "Receive on payee behalf",
                            //    CrTrn = "Credit Transfer for Payee",
                            //    DirDr = "Debit Transfer for Payee",
                            //    CrDay = 110,
                            //    PaidAmt = 9999999999.99,
                            //    PaymtDue = 9999999999.99,
                            //    AccDet = "23807238078320"
                            //};
                            //objinv.PayDtls = payee;

                            var documentTotal = new
                            {
                                AssVal = Math.Round(Convert.ToDecimal(dr["AssVal"].ToString()), 2),
                                IgstVal = Math.Round(Convert.ToDouble(dr["igstval"].ToString()), 2),
                                CgstVal = Math.Round(Convert.ToDouble(dr["cgstval"].ToString()), 2),
                                SgstVal = Math.Round(Convert.ToDouble(dr["sgstval"].ToString()), 2),
                                //CesVal = Math.Round(Convert.ToDouble(dr["cessval"].ToString()), 2),
                                //StCesVal = Math.Round(Convert.ToDouble(dr["cessval"].ToString()), 2),
                                // val_for_cur = 0,
                                 Discount = Math.Round(Convert.ToDouble(dr["HDISCOUNT"].ToString()), 2),
                                //OthChrg = Math.Round(Convert.ToDouble(dr["OTHCHRG"].ToString()), 2),
                                //RndOffAmt = 0,
                                TotInvVal = Math.Round(Convert.ToDecimal(dr["TotInvVal"].ToString()), 2)
                            };
                            objinv.ValDtls = documentTotal;
                            objinv.ItemList = new List<items>();

                        }

                        // item .......................................

                        items objitem = new items();
                        objitem.SlNo = dr["SLno"].ToString();
                        //objitem.PrdDesc = "Abvcd";
                        objitem.IsServc = dr["IsServc"].ToString().Trim();
                        objitem.HsnCd = dr["hsnCd"].ToString().Trim();//"1001";//hsncode;//
                        objitem.Qty = Convert.ToInt32(dr["quantity"]);
                        objitem.Unit = dr["uom"].ToString(); //"bag";// dr["uqc"].ToString();
                        objitem.UnitPrice = Math.Round(Convert.ToDecimal(dr["UnitPrice"].ToString()), 2);
                        objitem.TotAmt = Math.Round(Convert.ToDecimal(dr["TotAmt"].ToString()), 2);
                        objitem.Discount = Math.Round(Convert.ToDecimal(dr["discount"].ToString()), 2);
                        objitem.AssAmt = Math.Round(Convert.ToDecimal(dr["AssAmt"].ToString()), 2);
                        objitem.GstRt = Math.Round(Convert.ToDecimal(dr["GstRt"].ToString()), 3);
                        //objitem.CesRt = Math.Round(Convert.ToDecimal(dr["cess_rt"].ToString()), 3); ;
                        objitem.OthChrg = Math.Round(Convert.ToDecimal(dr["OTHCHRG"].ToString()), 2);
                        objitem.IgstAmt = Math.Round(Convert.ToDecimal(dr["IGSTAMT"].ToString()), 2);
                        objitem.CgstAmt = Math.Round(Convert.ToDecimal(dr["CGSTAMT"].ToString()), 2);
                        objitem.SgstAmt = Math.Round(Convert.ToDecimal(dr["SGSTAMT"].ToString()), 2);
                        //objitem.CesAmt = Math.Round(Convert.ToDecimal(dr["csamt"].ToString()), 2);
                        objitem.TotItemVal = Math.Round(Convert.ToDecimal(dr["TotItemVal"].ToString()), 2);
                        //Math.Round(Convert.ToDecimal(106.20), 2);//                                                                                                       
                        //var batchstr = new
                        //{
                        //    Nm = "PQR",
                        //    ExpDt = "30/12/2020",
                        //    WrDt = "20/12/2020"
                        //};
                        //objitem.BchDtls = batchstr;
                        //var attribDtlsSR = new
                        //{
                        //    Nm = "PQR",
                        //    Val = "12345"
                        //};
                        //objitem.AttribDtls = new[] { attribDtlsSR };
                        objinv.ItemList.Add(objitem);
                    }
                    mainJson.Add(objinv);
                    string json = JsonConvert.SerializeObject(objinv);
                    // webBrowser1.DocumentText = json;
                    listBox1.Items.Add("Waiting for API Response.....");
                    // APIURL = "http://EinvSandbox.webtel.in/v1.03/";
                    dynamic Response = POSTData(objinv, APIURL);
                    //dynamic Response = POSTData(mainJson, "https://api.einvoice.aw.navigatetax.pwc.co.in/sm2/v1/en/push");
                    // dynamic deserialized = JsonConvert.DeserializeObject(Response.ToString());

                    dynamic deserialized = null;
                    if (Response != null)
                    {
                        listBox1.Items.Add("API Response success!");
                        deserialized = JsonConvert.DeserializeObject(Response.ToString());
                    }
                    else { listBox1.Items.Add("API Response Failed....."); return; }
                    webBrowser1.DocumentText = json + "Repsponse" + JsonConvert.DeserializeObject(Response.ToString());

                    //if (deserialized.status == false)
                    //{
                    //    listBox1.Items.Add(deserialized.error.ToString());
                    //    return;
                    //}
                    //webBrowser2.DocumentText = deserialized.ToString();
                    var InfoDtls1 = deserialized;
                    string statusheader = deserialized[0].Status;
                    //string statuscode = InfoDtls1.document_type;
                    if (statusheader == "1") //irp_response
                    {
                        string invno = cmbxInvoice.SelectedValue.ToString();
                        string irn = deserialized[0].Irn;
                        string SignedQRCode = deserialized[0].SignedQRCode;

                        string cretedDate = deserialized[0].AckDate;
                        listBox1.Items.Clear();
                        listBox1.Items.Add("IRN NO:" + irn);
                        listBox1.Items.Add("Created Date:" + cretedDate);
                        try
                        {
                            //listBox1.Items.Add("Please wait, QR Thumb creating in process.........");
                            csCreateInvPdf objinvoce = new csCreateInvPdf();
                            csQRCode objQr = new csQRCode();
                            listBox1.Items.Add("Please wait,Creating QR image in Proccess......");
                            if (objinvoce.SaveIRNNOByInvNo(cmbxInvoice.SelectedValue.ToString(), comboBox1.Text, irn, cretedDate, "", "", "", SignedQRCode.ToString()) == "TRUE")
                            {
                                // listBox1.Items.Add("IRN  Processed Successfully.");
                                // MessageBox.Show("IRN Processed Successfully.");
                            }
                            if (objQr.createQrImage(irn, invno, SignedQRCode.ToString()) == true)
                            {
                                listBox1.Items.Add("QR Thumb created!");
                                listBox1.Items.Add("IRN  Processed Successfully.");
                                MessageBox.Show("IRN Processed Successfully.");
                                // listBox1.Items.Add("Please wait,IRN number saving in process.........");
                            }
                            SuccessXmlList(invno, irn, cretedDate);
                        }
                        catch (Exception)
                        { throw; }
                    }
                    if (statusheader == "0") //irp_response
                    {
                        string ErrorMessage = deserialized[0].ErrorMessage;
                        string ErrorCode = deserialized[0].ErrorCode;
                        string message = deserialized[0].InfoDtls;
                        if (ErrorCode == "2150")
                        {
                            var fff = deserialized[0].InfoDtls;
                            // string dd = fff[0].InfoDtls;
                            var ddss = fff[0].Desc;
                            string irn = ddss.Irn;
                            string AckDt = ddss.AckDt;
                            csCreateInvPdf objinvoce = new csCreateInvPdf();
                            MessageBox.Show("IRN Number already generated !! ");
                            listBox1.Items.Clear();
                            listBox1.Items.Add("IRN NO:" + irn);
                            listBox1.Items.Add("AckDt:" + AckDt);
                            if (objinvoce.SaveIRNNOByInvNo(cmbxInvoice.SelectedValue.ToString(), comboBox1.Text, irn, "", "", "", "", "") == "TRUE")
                            {
                                listBox1.Items.Add("IRN Already Processed.");
                                MessageBox.Show("IRN Already Processed.");
                            }
                        }
                        else
                        {
                            string[] ssCode = ErrorCode.Split(';');
                            string[] ssMessage = ErrorMessage.Split(';');
                            for (int i = 0; i < ssCode.Length; i++)
                            {
                                listBox1.Items.Add("Error Code:" + ssCode[i].ToString() + "  " + " Error Message  " + ssMessage[i].ToString());
                            }
                        }
                    }

                }
                else { MessageBox.Show("Data Empty!!!"); }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
          
        }
        public object POSTData(object json, string url)
        {
            object returnValue = null;
            try
            {
                using (var content = new StringContent(JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8, "application/json"))
                {
                    //using (var httpClientHandler = new HttpClientHandler { Credentials = new NetworkCredential("Mitsui_TN", "MitsuiTN@123") })

                    //using (var httpClientHandler = new HttpClientHandler { Credentials = new NetworkCredential("sch9650", "09102010") })
                    using (var _httpClient = new HttpClient()) //httpClientHandler
					{
                        _httpClient.BaseAddress = new Uri(url);
                        _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic TWl0c3VpX1ROOk1pdHN1aVROQDEyMw==");
                        _httpClient.DefaultRequestHeaders.Add("Cookie", "JSESSIONID=940BDAB1D8EF9A30B8657CA25A610E18; AWSALB=rgrhwx17vz3cDbea785P7grZ9/4VBQ4YysHkxaqJKGqBzHphfapEOdproZcjZfSzMK59qj/l4YhSaY9niC1/cTKLWmhvHQZYkpkEZuu7t5drJ4oYpzSjrD5Del+b; AWSALBCORS=rgrhwx17vz3cDbea785P7grZ9/4VBQ4YysHkxaqJKGqBzHphfapEOdproZcjZfSzMK59qj/l4YhSaY9niC1/cTKLWmhvHQZYkpkEZuu7t5drJ4oYpzSjrD5Del+b");
                        _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
                        _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                        _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                      //  _httpClient.DefaultRequestHeaders.Add("token", "RGVtbzI=");
                       // _httpClient.DefaultRequestHeaders.Add("customerid", "513");
                        HttpResponseMessage result = _httpClient.PostAsync(url, content).Result;
                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            //return true;
                            returnValue = result.Content.ReadAsStringAsync().Result;
                            dynamic deserialized = JsonConvert.DeserializeObject(returnValue.ToString());
                        }
                    }
                }
            }
            catch (Exception)
            { throw; }
            return returnValue;
        }
        public void SuccessXmlList(string inv, string irn, string irndt)
        {
            if (!System.IO.File.Exists("SuccessList.xml"))
            {
                XElement element = new XElement("QRCODE");
                element.Save("SuccessList.xml");
            }
            XmlDocument xmlEmloyeeDoc = new XmlDocument();
            xmlEmloyeeDoc.Load("SuccessList.xml");
            XmlElement ParentElement = xmlEmloyeeDoc.CreateElement("QRCODE");
            XmlElement ID = xmlEmloyeeDoc.CreateElement("InvNumber");
            ID.InnerText = inv;
            XmlElement Name = xmlEmloyeeDoc.CreateElement("Irn");
            Name.InnerText = irn;
            XmlElement Designation = xmlEmloyeeDoc.CreateElement("IrnDate");
            Designation.InnerText = irndt;
            ParentElement.AppendChild(ID);
            ParentElement.AppendChild(Name);
            ParentElement.AppendChild(Designation);
            xmlEmloyeeDoc.DocumentElement.AppendChild(ParentElement);
            xmlEmloyeeDoc.Save("SuccessList.xml");
        }

        public void ErrorXmlList(string inv, string errcode, string errdetail)
        {
            if (!System.IO.File.Exists("ErrorList.xml"))
            {
                XElement element = new XElement("QRCODE");
                element.Save("ErrorList.xml");
            }
            XmlDocument xmlEmloyeeDoc = new XmlDocument();
            xmlEmloyeeDoc.Load("ErrorList.xml");
            XmlElement ParentElement = xmlEmloyeeDoc.CreateElement("QRCODE");
            XmlElement ID = xmlEmloyeeDoc.CreateElement("InvNumber");
            ID.InnerText = inv;
            XmlElement Name = xmlEmloyeeDoc.CreateElement("ErrorCode");
            Name.InnerText = errcode;
            XmlElement Designation = xmlEmloyeeDoc.CreateElement("ErrorMessage");
            Designation.InnerText = errdetail;
            ParentElement.AppendChild(ID);
            ParentElement.AppendChild(Name);
            ParentElement.AppendChild(Designation);
            xmlEmloyeeDoc.DocumentElement.AppendChild(ParentElement);
            xmlEmloyeeDoc.Save("ErrorList.xml");
        }

        private void label27_DoubleClick(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            webBrowser1.Visible = true;
        }

        private void label17_DoubleClick(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            webBrowser1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "INV")
                InvList();
            if (comboBox1.SelectedItem.ToString() == "CRN")
                CRNList(1);
            if (comboBox1.SelectedItem.ToString() == "DBN")
                CRNList(2);
        }

        private void CRNList(int ss)
        {
            DataTable dt;
            string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";
            System.Data.SqlClient.SqlConnection conn;
            System.Data.SqlClient.SqlCommand cmd;

            conn = new System.Data.SqlClient.SqlConnection(connectionstring);
            try
            {
                conn.Open();
                string Querystring = "select RTRIM(CRDNUMBER) CRDNUMBER from OECRDH where ADJTYPE=" + ss + "  ORDER BY CRDDATE DESC ";
                cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
                cmd.CommandTimeout = 180;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter())
                {
                    DataRow dr;
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    dt = new DataTable();
                    sda.Fill(dt);
                    dr = dt.NewRow();
                    dt.Rows.InsertAt(dr, 0);
                    cmbxInvoice.DataSource = dt;
                    cmbxInvoice.ValueMember = "CRDNUMBER";
                    cmbxInvoice.DisplayMember = "CRDNUMBER";
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("Error in CDT/DBT List!" + ex.Message);
                conn.Close();
            }
        }
    }
}
