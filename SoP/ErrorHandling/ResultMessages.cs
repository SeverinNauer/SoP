using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoP.ErrorHandling
{
    public class ResultMessages
    {
        public static string User_Creation_AlreadyExists = "User.Creation.AlreadyExists";
        public static string User_Login_Invalid = "User.Login.Invalid";
        public static string User_Authorization_NotOnDb = "User.Authorization.NotOnDb";

        public static string Creation_Failed = "Creation.Failed";
        public static string Creation_Success = "Creation.Success";
        public static string Update_Success = "Update.Success";
        public static string Delete_Success = "Delete.Success";

        public static string Category_NotFound = "Category.NotFound";
        public static string Catergoy_Delete_Error_Has_References = "Catergoy.Delete.Error.Has.References";

        public static string PasswordEntry_NotFound = "PasswordEntry.NotFound";
        public static string PasswordEntry_IdNotSet = "PasswordEntry.IdNotSet";

        public static string Bearer(string token) 
        {
            return "Bearer " + token;
        }
    }
}
