using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace SOMFY
{
    internal class csCreateInvPdf
    {

        protected internal string USERNAME = null;
        protected internal string PASSWORD = null;
        protected internal string SAGEDB = null;
        protected internal string SERVERNAME = null;
        protected internal string SAA = null;
        protected internal string SAPSS = null;
       
        ReadWriteXML xml1 = new ReadWriteXML();
        public string SaveIRNNOByInvNo(string invn, String invtype, string irnno, String irnDate, string ewbno, string ewbdt, string ewbVdTo, string QRCODE)
        {
            String strreturn = "False";
            try
            {
                bool conStatus = xml1.ReadXML();
                if (conStatus == true)
                {
                    SERVERNAME = xml1.SERVERNAME;
                    SAGEDB = xml1.SAGEDB;
                    SAA = xml1.SAA;
                    SAPSS = xml1.SAPSS;
                }
                if (conStatus == true)
                {
                    SERVERNAME = xml1.SERVERNAME;
                    SAGEDB = xml1.SAGEDB;
                    SAA = xml1.SAA;
                    SAPSS = xml1.SAPSS;
                }
                string connectionstring = "Data Source=" + SERVERNAME + "; Initial Catalog=" + SAGEDB + "; User ID=" + SAA + "; Password=" + SAPSS + ";";

                System.Data.SqlClient.SqlConnection conn;
                System.Data.SqlClient.SqlCommand cmd;

                conn = new System.Data.SqlClient.SqlConnection(connectionstring);
                conn.Open();
                //string Querystring = "select value from arcuso where optfield='gstin' and idcust='"+custno +"'";

                string Querystring = "INSERT INTO OEIRNO VALUES ('" + invn + "','" + invtype.Substring(0, 1) + "','" + irnno + "','" + irnDate + "','" + ewbno + "','" + ewbdt + "','" + ewbVdTo + "','" + QRCODE + "')";
                cmd = new System.Data.SqlClient.SqlCommand(Querystring, conn);
                cmd.CommandTimeout = 180;
                cmd.CommandType = CommandType.Text;
                int i = cmd.ExecuteNonQuery();
                strreturn = "TRUE";
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                strreturn = "FALSE";
            }

            return strreturn;
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

	}
}