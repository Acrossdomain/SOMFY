using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace SOMFY
{
    internal class csQRCode
    {
        frmSOMFY form1 = new frmSOMFY();
        public bool createQrImage(string irn, string invnumber, string Qrcode12)
        {
            try
            {
                // invnumber = "IN0000000051";
                string strinvnumber = invnumber.Replace("/", "");
                #region

                byte[] bytes = Convert.FromBase64String(Qrcode12);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }

               
                //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                //QRCodeData qrCodeData = qrGenerator.CreateQrCode(Qrcode12, QRCodeGenerator.ECCLevel.M);
                ////QRCodeData qrCodeData1u = qrGenerator.CreateQrCode(Qrcode12, QRCodeGenerator.ECCLevel.H,
                //// QRCodeData qrCodeData = qrGenerator.CreateQrCode(Qrcode12, QRCodeGenerator.ECCLevel.Q,false,false,QRCodeGenerator.EciMode.Utf8,-1);

                //QRCode qrCode = new QRCode(qrCodeData);
                //Bitmap qrCodeImage = qrCode.GetGraphic(1);
                //// Bitmap newImage = ResizeBitmap(qrCodeImage, 130, 130);
                //// int WD = newImage.Width;
                //// int HT = newImage.Height;

                //var resultImage = new Bitmap(qrCodeImage.Width, qrCodeImage.Height); // 20 is bottom padding, adjust to your text
                ////qrCodeImage.Save(@"C:/E-Invoice/QR/" + strinvnumber + "1.png", System.Drawing.Imaging.ImageFormat.Png);
                //using (var graphics = Graphics.FromImage(resultImage))
                //// using (var font = new System.Drawing.Font("Consolas", 12))
                //using (var brush = new SolidBrush(Color.Black))
                //// using (var format = new StringFormat()
                ////  {
                ////   Alignment = StringAlignment.Center, // Also, horizontally centered text, as in your example of the expected output
                ////    LineAlignment = StringAlignment.Far
                //// })
                //{
                //    //graphics.Clear(Color.White);
                //    graphics.DrawImage(qrCodeImage, new PointF());
                //    // graphics.DrawString(code, font, brush, resultImage.Width / 2, resultImage.Height, format);
                //}

                // Set the size of the PictureBox control.
                //form1.pictureBox1.Size = new System.Drawing.Size(145, 145);
                ////Set the SizeMode to center the image.
                //form1.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                //form1.pictureBox1.Image = resultImage;

                #endregion

                // Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                // qrcode.GetDefaultMetrics(100);

                //Zen.Barcode.CodeQrBarcodeDraw.QRCodeEncoder.divideDataBy8Bits(Int32[] data, SByte[] bits, Int32 maxDataCodewords)
                //  form1.pictureBox1.Image = qrcode.Draw(Qrcode12, 50, 1);

                //form1.pictureBox1.Image = qrcode.Draw(Qrcode12, 50);

                // form1.pictureBox1.Image.Save(@"QR/" + strinvnumber + ".png", System.Drawing.Imaging.ImageFormat.Png);

                image.Save(@"QR/" + strinvnumber + ".png", System.Drawing.Imaging.ImageFormat.Png);

                //form1.pictureBox1.Image.Save(@"QR/" + strinvnumber + ".png", System.Drawing.Imaging.ImageFormat.Png);

                return true;
            }
            catch (Exception ex)
            {
                string dd = ex.ToString();
                return false;
            }
        }
    }
}