using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace BlueDataWeb.Helpers
{
    public class ImageUtils
    {
        public static string PathSaveImagesAdver()
        {
            return "~/Content/Images/Adver/";
        }

        public static string PathSaveImagesNation()
        {
            return "~/Content/Images/Nation/";
        }

        public static string PathSaveImagesCity()
        {
            return "~/Content/Images/City/";
        }

        public static string PathSaveImagesCategory()
        {
            return "~/Content/Images/Category/";
        }

        public static string PathSaveImagesSubCategory()
        {
            return "~/Content/Images/SubCategory/";
        }

        public static string PathSaveImagesAvata()
        {
            return "~/Content/Images/Avata/";
        }

        public static string PathSaveImagesMainProduct()
        {
            return "~/Content/Images/Product/";
        }

        public static string PathSaveImagesSlideProduct()
        {
            return "~/Content/Images/Product/Slide";
        }

        public static string PathEmailTemplateSendOrder()
        {
            return "~/Storage/SendOrderTemplate.cshtml";
        }

        public static string PathSaveImagesAvataSupplier()
        {
            return "~/Content/Images/Supplier/";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bmpUpload"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool RewriteImageFix(Bitmap bmpUpload, int width, int height, string filename)
        {
            try
            {
                int widthTh, heightTh;
                float widthOrig, heightOrig;
                float fx, fy, f;

                widthOrig = bmpUpload.Width;
                heightOrig = bmpUpload.Height;

                fx = widthOrig / width;
                fy = heightOrig / height;

                f = Math.Max(fx, fy);
                if (f < 1) f = 1;

                widthTh = (int)(widthOrig / f);
                heightTh = (int)(heightOrig / f);

                float beginX = (width - widthTh) / 2;
                float beginY = (height - heightTh) / 2;

                var bmpSave = new Bitmap(width, height);

                Graphics graph = Graphics.FromImage(bmpSave);
                graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graph.Clear(Color.White);
                graph.DrawImage(bmpUpload, beginX, beginY, widthTh, heightTh);

                ImageCodecInfo codec = GetEncoderInfo("image/png");
                if (codec == null)
                    codec = FindEncoder(ImageFormat.Png);
                EncoderParameters eps = new EncoderParameters();
                eps.Param[0] = new EncoderParameter(Encoder.Quality, 90L);
                bmpSave.Save(filename, codec, eps);
                bmpSave.Dispose();

                //Finish
                graph.Dispose();
                eps.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static bool RewriteImage(Bitmap sourceBitmap, string saveFilePath)
        {
            if (sourceBitmap != null)
            {
                EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, 90L);
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/png");
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;
                sourceBitmap.Save(saveFilePath, jpegCodec, encoderParams);

                qualityParam.Dispose();
                return true;
            }
            return false;
        }

        internal static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] myEncoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo myEncoder in myEncoders)
                if (myEncoder.MimeType == mimeType)
                    return myEncoder;

            return null;
        }

        internal static ImageCodecInfo FindEncoder(ImageFormat format)
        {
            ImageCodecInfo[] infoArray1 = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo[] infoArray2 = infoArray1;
            for (int num1 = 0; num1 < infoArray2.Length; num1++)
            {
                ImageCodecInfo info1 = infoArray2[num1];
                if (info1.FormatID.Equals(format.Guid))
                    return info1;
            }
            return null;
        }


     
    }

}