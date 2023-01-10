using SOMFY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DueAMT
{
    public partial class Config : Form
    {
        protected internal string colsestr ;
        List<string> txtzList = new List<string>();
        ReadWriteXML xml1 = new ReadWriteXML();
        public Config()
        {
            InitializeComponent();
			
				//bool conStatus = xml1.ReadXML();
				//if (conStatus == true)
				//{
				//this.Close();
				//this.Dispose();
				////Application.(new DueAMT.Config());
				//frmSOMFY sOMFY = new frmSOMFY();
				//	sOMFY.Show();
				//}
			}

        public bool validation()
        {
            
            bool rtn = false;
           
              if (string.IsNullOrWhiteSpace(txtServername.Text) && string.IsNullOrWhiteSpace(txtSageDB.Text))
            {
                MessageBox.Show("Please fill all fields of Sage DB");
                rtn = false;
            }                
            else
            {
                rtn = true;
            }
            return rtn;

        }
        private void btncreateConnection_Click(object sender, EventArgs e)
        {
            if (validation() == false)
                return;
            else
            {
    //            bool conStatus = xml1.ReadXML();
    //            if (conStatus == true)
    //            {
    //              this.Close();
				//	frmSOMFY sOMFY = new frmSOMFY();
				//	sOMFY.Show();
    //            }
				//else {

					XmlTextWriter writer = new XmlTextWriter(@"SOMFYCRD.xml", System.Text.Encoding.UTF8);
					writer.WriteStartDocument(false);
					writer.Formatting = Formatting.Indented;
					writer.Indentation = 2;
					writer.WriteStartElement("dbconfig");
					string SagePAss = xml1.EncryptString(txtsapass.Text, "AcrossDomain");  
					txtzList.Insert(0, txtServername.Text);
					txtzList.Insert(1, txtSageDB.Text);
					txtzList.Insert(2, txtsqluserid.Text);
					txtzList.Insert(3, SagePAss);

					createNode(writer, txtzList);
					writer.WriteEndElement();
					writer.WriteEndDocument();
					writer.Close();
				//}
				this.Close();
			}
        }
         private void createNode(XmlTextWriter writer, List<string> lstDetail)
        {            
            writer.WriteStartElement("SERVERNAME");
            writer.WriteString(lstDetail[0].ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("CDKEY");
            writer.WriteString("EMPTY");
            writer.WriteEndElement();
            writer.WriteStartElement("EINVUSERNAME");
            writer.WriteString("EMPTY");
            writer.WriteEndElement();
            writer.WriteStartElement("EINVPASSWORD");
            writer.WriteString("EMPTY");
            writer.WriteEndElement();
			writer.WriteStartElement("EFUSERNAME");
			writer.WriteString("EMPTY");
			writer.WriteEndElement();
			writer.WriteStartElement("EFPASSWORD");
			writer.WriteString("EMPTY");
			writer.WriteEndElement();
			writer.WriteStartElement("SAGEDB");
			writer.WriteString(lstDetail[1].ToString());
			writer.WriteEndElement();
			writer.WriteStartElement("SAA");
			writer.WriteString(lstDetail[2].ToString());
			writer.WriteEndElement();
			writer.WriteStartElement("SAPSS");
			writer.WriteString(lstDetail[3].ToString());
			writer.WriteEndElement();
			writer.WriteStartElement("SGSTIN");
			writer.WriteString("EMPTY");
			writer.WriteEndElement();
			writer.WriteStartElement("BGSTIN");
			writer.WriteString("EMPTY");
			writer.WriteEndElement();
			writer.WriteStartElement("VERSION");
			writer.WriteString("1.01");
			writer.WriteEndElement();
			writer.WriteStartElement("APIURL");
			writer.WriteString("");
			writer.WriteEndElement();
			writer.WriteStartElement("FLRDATE");
			writer.WriteString("20200101");
			writer.WriteEndElement();
		
		}

        private void Exit_Click(object sender, EventArgs e)
        {

            //this.Close();
            // 
            Application.Exit();

			//frmSOMFY frmSOMFY = new frmSOMFY();
			//frmSOMFY.Show();
        }

        private void Config_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
            //Application.Run(new FrmDueAmt());

        }

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            
        }
    }
}
