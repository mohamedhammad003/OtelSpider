using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OtelSpider.Web.Helpers
{
    public class ConfigurationHelper
    {
        public static string EncryptionPassword
        {
            get
            {
                return GetLocalConfigParameter("EncryptionPassword");
            }
        }
        //public static bool IsSuperAdmin
        //{
        //    get
        //    {
        //        return GetGlobalConfigParameter("IsSuperAdmin") == "true";
        //    }
        //}

        private static string GetLocalConfigParameter(string parameterKey)
        {
            return ConfigurationManager.AppSettings[parameterKey].ToString();
        }

        //private static string GetGlobalConfigParameter(string parameterKey)
        //{
        //    if (HttpContext.Current.Application[UserHelper.Current.UserInfo.CompanyName] == null)
        //    {
        //        return "";
        //    }
        //    IEnumerable<ConfigurationParameter> confModel = (IEnumerable<ConfigurationParameter>)HttpContext.Current.Application[UserHelper.Current.UserInfo.CompanyName];
        //    if (confModel == null)
        //    {
        //        return "";
        //    }
        //    ConfigurationParameter parameterModel = confModel.Where(x => x.Name == parameterKey).FirstOrDefault();
        //    return parameterModel == null ? "" : parameterModel.Value;
        //}

        //public static void AddConfigrationParametersToCache(string companyName, IEnumerable<ConfigurationParameter> confParametersModelList)
        //{
        //    HttpContext.Current.Application[companyName] = confParametersModelList;
        //}
    }
}