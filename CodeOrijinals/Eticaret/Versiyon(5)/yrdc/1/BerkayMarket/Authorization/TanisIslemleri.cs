using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BerkayMarket.Authorization
{
    public static class TanisIslemleri
    {
        public static OperationAuthorizationRequirement Create =   
          new OperationAuthorizationRequirement {Name=Constants.CreateOperationName};
        public static OperationAuthorizationRequirement Read = 
          new OperationAuthorizationRequirement {Name=Constants.ReadOperationName};  
        public static OperationAuthorizationRequirement Update = 
          new OperationAuthorizationRequirement {Name=Constants.UpdateOperationName}; 
        public static OperationAuthorizationRequirement Delete = 
          new OperationAuthorizationRequirement {Name=Constants.DeleteOperationName};
        public static OperationAuthorizationRequirement Onayla = 
          new OperationAuthorizationRequirement {Name=Constants.OnaylaIslemAdi};
        public static OperationAuthorizationRequirement Reject = 
          new OperationAuthorizationRequirement {Name=Constants.RedIslemAdi};
    }

    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string OnaylaIslemAdi = "Onayla";
        public static readonly string RedIslemAdi = "Red";

        public static readonly string AdminRole = "Admin";
        public static readonly string ManagerRole = "Manager";

        public static readonly string TanisAdminRolu ="tanisAdmini";
        public static readonly string TanisManagerRole = "TanisManageri";


        public static readonly string Uye = "Uye";
        public static readonly string Uretici = "Uretici";
        public static readonly string Toptanci = "Toptanci";
        public static readonly string Perakendeci = "Perakendeci";
        public static readonly string Alici = "Alici";
    }
}