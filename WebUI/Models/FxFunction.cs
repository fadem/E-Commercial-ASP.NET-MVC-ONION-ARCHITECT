using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public enum ImageFile
    {
        Categories,
        SubCategories,
        ThirdCategories,
        Brands,
        Products,
        AppUsers,
        ImagePath

    }
    public static class FxFunction
    {
        public static string ImageUpload(HttpPostedFileBase resim, ImageFile ımageFile,out bool isCompleted)
        {
            string errorMessage = null;
            if(resim!=null)
            {
                if (resim.ContentLength <= 2097152)
                {
                    if (resim.ContentType.Contains("image"))
                    {
                        string uploadImage = $"{ımageFile.ToString()}/{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{resim.ContentType.Split('/')[1]}";
                        resim.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/"+ uploadImage));
                        isCompleted = true;
                        return uploadImage;

                    }
                    else
                    {
                        errorMessage = "Lütfen sadece resim formatında yükleme yapınız";
                    }

                }
                else
                {
                    errorMessage = "Yüklemek istediğiniz resmin boyutu 2MB'ın altında olması gerekmektedir";
                }
            }
            else
            {
                errorMessage = "Lütfen Bir Resim Seçin";
            }
            isCompleted = false;
            return errorMessage;
        }
    }
}