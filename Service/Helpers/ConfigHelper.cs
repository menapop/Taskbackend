using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Service.Helpers.Enums;

namespace FoodSys.Service.Helper
{
    public static class ConfigHelper
    {
        public static IConfiguration Configuration { get; set; }
        private static IWebHostEnvironment _env;

        public static string PathBuilder(string EntityTypeId, string EntityId,int attachmentId,string extension)
        {
            return Path.Combine(_env.WebRootPath, "api", EntityTypeId, EntityId, attachmentId.ToString(), extension);
        }
       


        public static string TripleDESKey => GetValue("AppSettings:TripleDESKey", string.Empty);
        private static T GetValue<T>(string key, T defaultvalue) where T : IConvertible
        {
            #region Declare return Var with intial value
            T value = default(T);
            #endregion
            try
            {
                #region VARS
                dynamic result = null;
                Type valueType = typeof(T);
                #endregion
                #region Check whether the value is exists in the config or not.
                if (!string.IsNullOrWhiteSpace(Configuration.GetSection(key).Value))
                {
                    #region Casting the value into specified type.
                    if (valueType == typeof(int))
                    {
                        int keyValue = default(int);
                        if (int.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(double))
                    {
                        double keyValue = default(double);
                        if (double.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(long))
                    {
                        long keyValue = default(long);
                        if (long.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(byte))
                    {
                        byte keyValue = default(byte);
                        if (byte.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(bool))
                    {
                        bool keyValue = default(bool);
                        if (bool.TryParse(Configuration.GetSection(key).Value, out keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    else if (valueType == typeof(string))
                    {
                        string keyValue = Configuration.GetSection(key).Value;
                        if (!string.IsNullOrWhiteSpace(keyValue))
                            result = keyValue;
                        else
                            result = defaultvalue;
                    }
                    #endregion
                    #region Getting the value.
                    value = (T)Convert.ChangeType(result, valueType);
                    #endregion
                }
                else
                {

                }
                #endregion
            }
            catch { } // Wait Review
            return value;
        }
      
    }
}
