using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic.Library
{
    public class UploadImage
    {

        private OpenFileDialog fd = new OpenFileDialog();
        
        public void LoadImage(PictureBox pictureBox)
        {
            pictureBox.WaitOnLoad = true;
            fd.Filter = "Imagenes|*.jpg;*.gif;*.png;*.bmp";
            fd.ShowDialog();

            if(fd.FileName !=string.Empty)
            {
                pictureBox.ImageLocation=fd.FileName;
            }
        }
        public Image byteArrayToImage(byte[] arrayImage)
        {
            MemoryStream ms = new MemoryStream(arrayImage);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

    
    }
}
