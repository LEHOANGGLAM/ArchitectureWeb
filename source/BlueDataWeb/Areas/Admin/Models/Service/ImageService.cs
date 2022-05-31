using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace BlueDataWeb.Areas.Admin.Models.Service
{
    public class ImageService
    {
        public void Resize(string ImageSavePath, string fileName, int MaxWidthSideSize, int MaxHeightSidesize, Stream Buffer)
        {
            int intNewWidth;
            int intNewHeight;
            System.Drawing.Image imgInput = System.Drawing.Image.FromStream(Buffer);
            ImageCodecInfo myImageCodecInfo;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            //
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter;
            //Giá trị width và height nguyên thủy của ảnh;
            int intOldWidth = imgInput.Width;
            int intOldHeight = imgInput.Height;

            //Kiểm tra xem ảnh ngang hay dọc;
            int intMaxSideW;
            int intMaxSideH;
            /*if (intOldWidth >= intOldHeight)
            {
            intMaxSide = intOldWidth;
            }
            else
            {
            intMaxSide = intOldHeight;
            }*/
            //Để xác định xử lý ảnh theo width hay height thì bạn bỏ note phần trên;
            //Ở đây mình chỉ sử dụng theo width nên gán luôn intMaxSide= intOldWidth; ^^;
            intMaxSideW = intOldWidth;
            intMaxSideH = intOldHeight;
            if (intMaxSideW > MaxWidthSideSize | intMaxSideH > MaxHeightSidesize)
            {
                //Gán width và height mới.
                double dblCoefW = MaxWidthSideSize / (double)intMaxSideW;
                intNewWidth = Convert.ToInt32(dblCoefW * intOldWidth);
                double dblCoefH = MaxHeightSidesize / (double)intMaxSideH;
                intNewHeight = Convert.ToInt32(dblCoefH * intOldHeight);
            }
            else
            {
                //Nếu kích thước width/height (intMaxSide) cũ ảnh nhỏ hơn MaxWidthSideSize thì giữ nguyên //kích thước cũ;
                intNewWidth = intOldWidth;
                intNewHeight = intOldHeight;
            }


            //Tạo một ảnh bitmap mới;
            Bitmap bmpResized = new Bitmap(imgInput, intNewWidth, intNewHeight);
            //Phần EncoderParameter cho phép bạn chỉnh chất lượng hình ảnh ở đây mình để chất lượng tốt //nhất là 100L;
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            //Lưu ảnh;
            bmpResized.Save(ImageSavePath + fileName, myImageCodecInfo, myEncoderParameters);

            //Giải phóng tài nguyên;
            //Buffer.Close();
            imgInput.Dispose();
            bmpResized.Dispose();
        }
        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}