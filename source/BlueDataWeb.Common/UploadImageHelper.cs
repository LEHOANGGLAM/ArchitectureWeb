using System;
using System.IO;
using System.Web;

public static class UploadImageHelper
{
    public static string Upload(HttpPostedFileBase myFile,string pathImage,  params string[] path)
    {
        try
        {
            

            var fullPath = pathImage;
            foreach (var item in path)
            {
                fullPath = Path.Combine(fullPath, item);
            }

            string fileNameAvata = Path.GetFileNameWithoutExtension(myFile.FileName); // Xử lý seo tên hình xử hình trùng tên
            string fileNameToSave = HelperSEOName.ToSeoUrl($"{fileNameAvata}-{DateTime.Now.Ticks.ToString()}") + Path.GetExtension(myFile.FileName);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            fullPath = Path.Combine(fullPath, fileNameToSave);
            myFile.SaveAs(fullPath);
            return fileNameToSave;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }
}