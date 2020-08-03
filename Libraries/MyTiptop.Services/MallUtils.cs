using System;
using System.Web;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using MyTiptop.Core;
 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace MyTiptop.Services 
{ 
    public partial class MallUtils
    {
        #region  加密/解密

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr">加密字符串</param>
        public static string AESEncrypt(string encryptStr)
        {
            return SecureHelper.AESEncrypt(encryptStr, BMAConfig.MallConfig.SecretKey);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">解密字符串</param>
        public static string AESDecrypt(string decryptStr)
        {
            return SecureHelper.AESDecrypt(decryptStr, BMAConfig.MallConfig.SecretKey);
        }

        #endregion

        #region Cookie

        /// <summary>
        /// 获得用户sid
        /// </summary>
        /// <returns></returns>
        public static string GetSidCookie()
        {
            return WebHelper.GetCookie("bmasid");
        }

        /// <summary>
        /// 设置用户sid
        /// </summary>
        public static void SetSidCookie(string sid)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["bmasid"];
            if (cookie == null)
                cookie = new HttpCookie("bmasid");

            cookie.Value = sid;
            cookie.Expires = DateTime.Now.AddDays(15);
            string cookieDomain = BMAConfig.MallConfig.CookieDomain;
            if (cookieDomain.Length != 0)
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <returns></returns>
        public static int GetUidCookie()
        {
            return TypeHelper.StringToInt(GetBMACookie("uid"), -1);
        }

        /// <summary>
        /// 设置用户id
        /// </summary>
        public static void SetUidCookie(int uid)
        {
            SetBMACookie("uid", uid.ToString());
        }

        /// <summary>
        /// 获得cookie密码
        /// </summary>
        /// <returns></returns>
        public static string GetCookiePassword()
        {
            return WebHelper.UrlDecode(GetBMACookie("password"));
        }

        /// <summary>
        /// 解密cookie密码
        /// </summary>
        /// <param name="cookiePassword">cookie密码</param>
        /// <returns></returns>
        public static string DecryptCookiePassword(string cookiePassword)
        {
            return AESDecrypt(cookiePassword).Trim();
        }

        /// <summary>
        /// 设置cookie密码
        /// </summary>
        public static void SetCookiePassword(string password)
        {
            SetBMACookie("password", WebHelper.UrlEncode(AESEncrypt(password)));
        }

        /// <summary>
        /// 设置用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <param name="sid">sid</param>PartUserInfo
        /// <param name="expires">过期时间</param>xpGrid_User
        public static void SetUserCookie(xpGrid_User partUserInfo, int expires)
        {

            //cookie 的关键字改为 plant + bma ？待完成 ..
            //倒可以通过S31、S32、S80 相同的cookie关键字，实现无需再登录。

            //HttpCookie cookie = HttpContext.Current.Request.Cookies["bma"];
            HttpCookie cookie = HttpContext.Current.Request.Cookies["mytiptop"];
            if (cookie == null)
                cookie = new HttpCookie("mytiptop");

            cookie.Values["uid"] = partUserInfo.UserID.ToString();
            cookie.Values["password"] = WebHelper.UrlEncode(AESEncrypt(partUserInfo.Password));
            if (expires > 0)
            {
                cookie.Values["expires"] = expires.ToString();
                cookie.Expires = DateTime.Now.AddDays(expires);
            }
            string cookieDomain = BMAConfig.MallConfig.CookieDomain;
            if (cookieDomain.Length != 0)
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetBMACookie(string key)
        {
            return WebHelper.GetCookie("mytiptop", key);
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetBMACookie(string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["mytiptop"];
            if (cookie == null)
                cookie = new HttpCookie("mytiptop");

            cookie[key] = value;

            int expires = TypeHelper.StringToInt(cookie.Values["expires"]);
            if (expires > 0)
                cookie.Expires = DateTime.Now.AddDays(expires);

            string cookieDomain = BMAConfig.MallConfig.CookieDomain;
            if (cookieDomain.Length != 0)
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得访问referer
        /// </summary>
        public static string GetRefererCookie()
        {
            string referer = WebHelper.UrlDecode(WebHelper.GetCookie("referer"));
            if (referer.Length == 0)
                referer = "/";
            return referer;
        }

        /// <summary>
        /// 设置访问referer
        /// </summary>
        public static void SetRefererCookie(string url)
        {
            WebHelper.SetCookie("referer", WebHelper.UrlEncode(url));
        }

        /// <summary>
        /// 获得系统后台访问referer
        /// </summary>
        public static string GetMallAdminRefererCookie()
        {
            return GetAdminRefererCookie("/malladmin/home/mallruninfo");
        }

        ///// <summary>
        ///// 获得店铺后台访问referer
        ///// </summary>
        //public static string GetStoreAdminRefererCookie()
        //{
        //    return GetAdminRefererCookie("/storeadmin/home/storeruninfo");
        //}

        /// <summary>
        /// 获得后台访问referer
        /// </summary>
        public static string GetAdminRefererCookie(string defaultUrl)
        {
            string adminReferer = WebHelper.UrlDecode(WebHelper.GetCookie("adminreferer"));
            if (adminReferer.Length == 0)
                adminReferer = defaultUrl;
            return adminReferer;
        }

        /// <summary>
        /// 设置后台访问referer
        /// </summary>
        public static void SetAdminRefererCookie(string url)
        {
            WebHelper.SetCookie("adminreferer", WebHelper.UrlEncode(url));
        }






        #endregion

     


        ///// <summary>
        ///// datatable 转换为 json
        ///// </summary>
        ///// <param name="count"></param>
        ///// <param name="page"></param> 
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public static string DataTableToJson(DataTable dt)
        //{
        //    StringBuilder Json = new StringBuilder();
        //    Json.Append("[");
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                Json.Append("{");
        //                for (int j = 0; j < dt.Columns.Count; j++)
        //                {
        //                    Json.Append("\"" + dt.Columns[j].ColumnName.ToString() +
        //                     "\":\"" + GetDeleteBR(dt.Rows[i][j].ToString()) + "\"");
        //                    if (j < dt.Columns.Count - 1)
        //                    {
        //                        Json.Append(",");
        //                        Json.Append("\r\n");
        //                    }
        //                }
        //                Json.Append("}");
        //                if (i < dt.Rows.Count - 1)
        //                {
        //                    Json.Append(",");
        //                }
        //            }
        //        }
        //    }
        //    Json.Append("]");
        //    return Json.ToString();
        //}



        ///// <summary> 
        ///// 去掉换行符        
        ///// </summary>       
        ///// <param name="str"></param>    
        ///// <returns></returns>
        //public static string GetDeleteBR(string strinput)
        //{
        //    string p = "\\n|\r\n"; //数据库的的换行是\n
        //    string returnstr = System.Text.RegularExpressions.Regex.Replace(strinput, p, " ");
        //    return returnstr;
        //}



    }

    public partial class JSON
    {
        public static string DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
        public static string MyDateTimeFormat = "yyyy'-'MM'-'dd HH':'mm':'ss";
        public static string Encode(object o)
        {
            if (o == null || o.ToString() == "null") return null;

            if (o != null && (o.GetType() == typeof(String) || o.GetType() == typeof(string)))
            {
                return o.ToString();
            }
            IsoDateTimeConverter dt = new IsoDateTimeConverter();
            dt.DateTimeFormat = MyDateTimeFormat;
            return JsonConvert.SerializeObject(o, dt);
        }

        public static object Decode(string json)
        {
            if (String.IsNullOrEmpty(json)) return "";
            object o = JsonConvert.DeserializeObject(json);
            if (o.GetType() == typeof(String) || o.GetType() == typeof(string))
            {
                o = JsonConvert.DeserializeObject(o.ToString());
            }
            object v = toObject(o);
            return v;
        }

        public static object Decode(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        private static object toObject(object o)
        {
            if (o == null) return null;

            if (o.GetType() == typeof(string))
            {
                //判断是否符合2010-09-02T10:00:00的格式

                string s = o.ToString();
                if (s.Length == 19 && s[10] == 'T' && s[4] == '-' && s[13] == ':')
                {
                    o = System.Convert.ToDateTime(o);
                }
            }
            else if (o is JObject)
            {
                JObject jo = o as JObject;

                Hashtable h = new Hashtable();

                foreach (KeyValuePair<string, JToken> entry in jo)
                {
                    h[entry.Key] = toObject(entry.Value);
                }
                o = h;
            }
            else if (o is IList)
            {
                ArrayList list = new ArrayList();
                list.AddRange((o as IList));
                int i = 0, l = list.Count;
                for (; i < l; i++)
                {
                    list[i] = toObject(list[i]);
                }
                o = list;
            }
            else if (typeof(JValue) == o.GetType())
            {
                JValue v = (JValue)o;
                o = toObject(v.Value);
            }
            else
            {
            }
            return o;
        }

    }
} 
