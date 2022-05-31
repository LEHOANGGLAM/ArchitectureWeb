using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace BlueDataWeb.Helpers
{
    public enum ResultUpload
    {
        Exception,
        NotFile,
        SaveFileSuccess,
        NotFileSupportExtension,
        OverLimited,
        CheckOk
    }

    public class LinkSaveImages
    {
        public static string PathSaveImagesPromotion()
        {
            return "~/Content/Images/Promotion/";
        }
    }

    public static class ImageHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="httpPostedFileBase"></param>
        /// <param name="height"></param>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <returns>
        /// <c>0</c>:Exception
        /// <c>1</c>:Not file
        /// <c>2</c>:Save file success
        /// <c>3</c>:Not file support extension
        /// <c>4</c>:Over Limited
        /// </returns>
        public static ResultUpload UploadFile(string filePath, HttpPostedFileBase httpPostedFileBase, int width, int height, ref string fileName, bool? upSmallImage = false)
        {
            //VietLe: fixed bug upload hinh
            //Remove all non-word-char in filename
            fileName = Regex.Replace(fileName, @"\W", "-");

            var check = CheckUpload(httpPostedFileBase);
            if (check == ResultUpload.CheckOk)
            {
                var ext = Path.GetExtension(httpPostedFileBase.FileName).ToLower();
                ext = ext.Replace(".", "");

                try
                {
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        if (Directory.Exists(filePath) == false)
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        Bitmap bitmap = new Bitmap(httpPostedFileBase.InputStream);

                        var path = Path.Combine(filePath, string.Format("{0}.{1}", fileName, ext));
                        ImageUtils.RewriteImageFix(bitmap, width, height, path);

                        if (upSmallImage == true)
                        {
                            var path1 = Path.Combine(filePath, string.Format("Small\\{0}.{1}", fileName, ext));
                            ImageUtils.RewriteImageFix(bitmap, 13, 13, path1);
                        }

                        fileName = string.Format("{0}.{1}", fileName, ext);

                        bitmap.Dispose();

                        return ResultUpload.SaveFileSuccess;
                    }
                }
                catch (Exception)
                {
                    return ResultUpload.Exception;
                }
            }
            return check;
        }

        public static ResultUpload CheckUpload(HttpPostedFileBase httpPostedFileBase)
        {
            const string extension = "jpg,png,gif,bmp,jpeg";
            const int limitedLength = 10 * 1024;

            if (httpPostedFileBase == null) return ResultUpload.NotFile;
            if (httpPostedFileBase.ContentLength == 0) return ResultUpload.NotFile;

            var ext = Path.GetExtension(httpPostedFileBase.FileName).ToLower();
            ext = ext.Replace(".", "");
            if (extension.IndexOf(ext) < 0)
            {
                return ResultUpload.NotFileSupportExtension;
            }

            var isAccess = httpPostedFileBase.ContentLength <= limitedLength * 1024;
            if (!isAccess)
            {
                return ResultUpload.OverLimited;
            }

            return ResultUpload.CheckOk;
        }

        /// <summary>
        /// dowload file anh từ url cho sẵn và resize anh với kích thước anh  tùy chon
        /// </summary>
        /// <param name="Url">Link anh từ nguồn internet</param>
        /// <param name="pathSaveFile">Thu muc save anh tren sever</param>
        /// <param name="fileName">Ten file muon dat ban dau se khác với return</param>
        /// <param name="typeImage"></param>
        /// <param name="newWidth"></param>
        /// <param name="newHeight"></param>
        /// <returns> name của file đã edit thành cong</returns>
        public static string DownloadFileFromUrl(String Url, string pathSaveFile, string fileName, string typeImage, int newWidth, int newHeight, bool? stratransparent = false)
        {
            string newFileName = string.Empty;
            if (fileName.Length > 250)
            {
                fileName = fileName.Substring(0, 250);
            }
            var newPath = Path.Combine(pathSaveFile, string.Format("{0}.{1}", fileName + "_new", typeImage));
            var localFilename = Path.Combine(pathSaveFile, string.Format("{0}.{1}", fileName, typeImage));
            //Download file from remove server to
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(Url, localFilename);
                }
            }
            catch (Exception e)
            {
                return newFileName;
            }

            //Resize image da download
            Image original = Image.FromFile(localFilename);
            MemoryStream st = new MemoryStream();
            Bitmap bitmap = new Bitmap(original);

            if (bitmap.Height < 376)
            {
                return newFileName = fileName;
            }

            try
            {
                if (stratransparent == true)
                {
                    RewriteImageFix_Compress(bitmap, newWidth, newWidth, newPath, true);
                }
                else
                {
                    RewriteImageFix_Compress(bitmap, newWidth, newWidth, newPath);
                }

                original.Dispose();
                FileInfo myfileinf = new FileInfo(localFilename);
                //deletefile goc
                myfileinf.Delete();
                newFileName = fileName + "_new";
            }
            catch (Exception e)
            {
                return newFileName;
            }
            return newFileName;
        }

        /// <summary>
        /// Code Resize ảnh và compress ảnh va luu anh với định dạng ảnh jpg
        /// </summary>
        /// <param name="bmpUpload"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool RewriteImageFix_Compress(Bitmap bmpUpload, int width, int height, string filename, bool? stratransparent = false)
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
                graph.Clear(Color.Transparent);
                graph.DrawImage(bmpUpload, beginX, beginY, widthTh, heightTh);

                ImageCodecInfo codec = GetEncoderInfo("image/jpeg");
                EncoderParameters eps = new EncoderParameters();

                if (codec == null)
                    codec = FindEncoder(ImageFormat.Jpeg);
                eps.Param[0] = new EncoderParameter(Encoder.Quality, 70L);

                if (stratransparent == true)
                {
                    codec = GetEncoderInfo("image/png");
                    if (codec == null)
                        codec = FindEncoder(ImageFormat.Png);
                    eps.Param[0] = new EncoderParameter(Encoder.Quality, 30L);
                }

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