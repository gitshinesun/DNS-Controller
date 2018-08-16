using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.IO.Compression;
//using System.Net.Json;

namespace MyHttpsClient
{
    class MyHttpsService
    {
        public struct PostParam
        {
            public string strPostKey;
            public string strPostValue;
        };

        string m_strResponseLocation = String.Empty;
        string m_strResponseResult = String.Empty;
        string m_strResponseDirectUrl = String.Empty;
        string m_strJsonPostData = String.Empty;

        List<PostParam> m_listParams = new List<PostParam>();
        Dictionary<string, string> m_Cookie = new Dictionary<string, string>();

        HttpWebRequest m_Request;
        HttpWebResponse m_Response;        
        CookieContainer m_cookieData;
        ASCIIEncoding m_Encoding;
        string m_strSetCookieData;

        Uri m_Uri;

        public void Init()
        {
            m_strResponseLocation = String.Empty;
            m_strResponseDirectUrl = String.Empty;
            m_listParams.Clear();

            m_Request = null;
            m_Response = null;
            m_cookieData = null;
            m_Encoding = null;
            m_Uri = null;
            m_strSetCookieData = "";
        }

        public void GetRequest( string strRequestUrl, bool bCookie, bool bAllowAutoRedirect, bool bShortAccept)
        {
            try
            {
                if (bCookie)
                    m_cookieData = new CookieContainer();

                string strRequestAccept = string.Empty;
                string strContentEnconding = string.Empty;
                
                if (!bShortAccept)
                    strRequestAccept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                else
                    strRequestAccept = "*/*";

                m_strResponseLocation = string.Empty;
                m_strResponseResult = string.Empty;
                m_strResponseDirectUrl = string.Empty;

                m_Uri = new Uri(strRequestUrl);
                m_Request = (HttpWebRequest)WebRequest.Create(m_Uri);
                m_Request.Method = WebRequestMethods.Http.Get;
                m_Request.Timeout = 20000;
                m_Request.Accept = strRequestAccept;

                if (bCookie)
                    m_Request.CookieContainer = m_cookieData;
                else
                    m_Request.Headers["Cookie"] = m_strSetCookieData;

                m_Request.AllowAutoRedirect = bAllowAutoRedirect;
                m_Request.KeepAlive = true;
                m_Request.Headers.Add("Accept-Encoding", "gzip, deflate");
                m_Request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; BTRS5841; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.3; AskTbFXTV5/5.9.1.14019)";

                m_Response = (HttpWebResponse)m_Request.GetResponse();

                m_strSetCookieData = m_Response.Headers["Set-Cookie"];
                m_strResponseLocation = m_Response.Headers["Location"];
                strContentEnconding = m_Response.Headers["Content-Encoding"];

                Stream responseStream = m_Response.GetResponseStream();

                if (string.IsNullOrEmpty(strContentEnconding))
                {
                    StreamReader readStream = new StreamReader(responseStream);
                    m_strResponseResult = readStream.ReadToEnd();
                    readStream.Close();
                }
                else
                {
                    if (strContentEnconding.ToLower().Equals("gzip"))
                    {
                        responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                    }
                    else if (strContentEnconding.ToLower().Equals("deflate"))
                    {
                        responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);
                    }

                    byte[] buf = new byte[1024];
                    while (responseStream.Read(buf, 0, 1024) > 0)
                    {
                        m_strResponseResult += System.Text.Encoding.Default.GetString(buf);
                    }
                }

                m_strResponseDirectUrl = m_Response.ResponseUri.AbsoluteUri;  
   
                responseStream.Close();                
                m_Response.Close();                
            }
            catch
            {
            }
        }              

        //public void GetJsonRequest( string strRequestUrl, string strReferer, bool bCookie, bool bAllowAutoRedirect)
        //{
        //    try
        //    {
        //        if (bCookie)
        //            m_cookieData = new CookieContainer();

        //        string strContentEnconding = string.Empty;

        //        m_strResponseLocation = String.Empty;
        //        m_strResponseResult = String.Empty;
        //        m_Uri = new Uri(strRequestUrl);
        //        m_Request = (HttpWebRequest)WebRequest.Create(m_Uri);
        //        m_Request.Method = WebRequestMethods.Http.Get;
        //        m_Request.Timeout = 20000;
        //        m_Request.Referer = strReferer;
        //        m_Request.Accept = "*/*";

        //        if (bCookie)
        //            m_Request.CookieContainer = m_cookieData;
        //        else
        //            m_Request.Headers["Cookie"] = m_strSetCookieData;

        //        m_Request.AllowAutoRedirect = bAllowAutoRedirect;
        //        m_Request.KeepAlive = true;
        //        m_Request.Headers.Add("x-requested-with", "XMLHttpRequest");
        //        m_Request.Headers.Add("Accept-Encoding", "gzip, deflate");
        //        m_Request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; BTRS5841; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.3; AskTbFXTV5/5.9.1.14019)";

        //        m_Response = (HttpWebResponse)m_Request.GetResponse();

        //        m_strSetCookieData = m_Response.Headers["Set-Cookie"];
        //        m_strResponseLocation = m_Response.Headers["Location"];
        //        strContentEnconding = m_Response.Headers["Content-Encoding"];

        //        Stream responseStream = m_Response.GetResponseStream();

        //        if (string.IsNullOrEmpty(strContentEnconding))
        //        {
        //            StreamReader readStream = new StreamReader(responseStream);
        //            m_strResponseResult = readStream.ReadToEnd();
        //            readStream.Close();
        //        }
        //        else
        //        {
        //            if (strContentEnconding.ToLower().Equals("gzip"))
        //            {
        //                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
        //            }
        //            else if (strContentEnconding.ToLower().Equals("deflate"))
        //            {
        //                responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);
        //            }

        //            byte[] buf = new byte[1024];
        //            while (responseStream.Read(buf, 0, 1024) > 0)
        //            {
        //                m_strResponseResult += System.Text.Encoding.Default.GetString(buf);
        //            }
        //        }

        //        m_strResponseDirectUrl = m_Response.ResponseUri.AbsoluteUri;

        //        responseStream.Close();
        //        m_Response.Close();
        //    }
        //    catch
        //    {
        //    }
        //}

        public void GetRequestImg(string strRequestUrl, string strReferer, bool bCookie, bool bAllowAutoRedirect, bool bShortAccept)
        {
            try
            {

                if (bCookie)
                    m_cookieData = new CookieContainer();

                string strRequestAccept = String.Empty;

                if (!bShortAccept)
                    strRequestAccept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                else
                    strRequestAccept = "*/*";

                m_strResponseLocation = String.Empty;
                m_strResponseResult = String.Empty;
                m_Uri = new Uri(strRequestUrl);
                m_Request = (HttpWebRequest)WebRequest.Create(m_Uri);
                m_Request.Method = WebRequestMethods.Http.Get;
                m_Request.Timeout = 20000;
                m_Request.Accept = strRequestAccept;

                if (bCookie)
                    m_Request.CookieContainer = m_cookieData;
                else
                    m_Request.Headers["Cookie"] = m_strSetCookieData;

                m_Request.AllowAutoRedirect = bAllowAutoRedirect;
                m_Request.Referer = strReferer;
                m_Request.KeepAlive = true;
                m_Request.Headers.Add("Accept-Encoding", "gzip, deflate");
                m_Request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";

                m_Response = (HttpWebResponse)m_Request.GetResponse();

                byte[] ByteRes;
                byte[] buffer = new byte[4096];

                Stream responseStream = m_Response.GetResponseStream();
                MemoryStream memoryStream = new MemoryStream();

                int count = 0;
                do
                {
                    count = responseStream.Read(buffer, 0, buffer.Length);
                    memoryStream.Write(buffer, 0, count);

                } while (count != 0);

                ByteRes = memoryStream.ToArray();

                //WriteLog("c:\\captcha.jpg", ByteRes, ByteRes.Length);
                m_strResponseResult = Convert.ToBase64String(ByteRes);

                memoryStream.Close();
                responseStream.Close();
                m_Response.Close();
            }
            catch
            {
            }
        }

        public void InitPostParams()
        {
            m_listParams.Clear();
        }

        public void AddPostParams(string strKey, string strValue)
        {
            PostParam data = new PostParam();
            data.strPostKey = strKey;
            data.strPostValue = strValue;

            m_listParams.Add(data);
        }

        public string GetPostParams()
        {
            string strNode = String.Empty;
            string strPostData = String.Empty;

            foreach (PostParam data in m_listParams)
            {
                strNode = data.strPostKey + "=" + data.strPostValue;
                if (strPostData == "")
                {
                    strPostData = strNode;
                }
                else
                {
                    strPostData += "&" + strNode;
                }
            }

            return strPostData;
        }

        public void PostRequest(string strRequestUrl, string strReferer, bool bCookie, bool bAutoRedirect)
        {
            string strPostData = GetPostParams();

            try
            {
                if (bCookie)
                    m_cookieData = new CookieContainer();

                string strContentEnconding = string.Empty;

                m_strResponseResult = String.Empty;
                m_strResponseLocation = String.Empty;
                m_Uri = new Uri(strRequestUrl);
                m_Request = (HttpWebRequest)WebRequest.Create(m_Uri);
                m_Encoding = new ASCIIEncoding();
                byte[] data = m_Encoding.GetBytes(strPostData);
                m_Request.Method = WebRequestMethods.Http.Post;
                m_Request.Timeout = 20000;
                m_Request.ProtocolVersion = HttpVersion.Version11;
                m_Request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                m_Request.Referer = strReferer;
                m_Request.ContentType = "application/x-www-form-urlencoded";
                m_Request.Headers.Add("UA-CPU", "x86");
                m_Request.Headers.Add("Accept-Encoding", "gzip, deflate");
                m_Request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; BTRS5841; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.3; AskTbFXTV5/5.9.1.14019)";
                m_Request.KeepAlive = false;
                m_Request.Headers.Add("Cache-Control", "no-cache");

                if (bCookie)
                    m_Request.CookieContainer = m_cookieData;
                else
                    m_Request.Headers["Cookie"] = m_strSetCookieData;

                m_Request.ContentLength = data.Length;
                m_Request.AllowAutoRedirect = bAutoRedirect;

                Stream requestStream = m_Request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                m_Response = (HttpWebResponse)m_Request.GetResponse();

                m_strSetCookieData = m_Response.Headers["Set-Cookie"];
                m_strResponseLocation = m_Response.Headers["Location"];
                strContentEnconding = m_Response.Headers["Content-Encoding"];

                Stream responseStream = m_Response.GetResponseStream();

                if (string.IsNullOrEmpty(strContentEnconding))
                {
                    StreamReader readStream = new StreamReader(responseStream);
                    m_strResponseResult = readStream.ReadToEnd();
                    readStream.Close();
                }
                else
                {
                    if (strContentEnconding.ToLower().Equals("gzip"))
                    {
                        responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                    }
                    else if (strContentEnconding.ToLower().Equals("deflate"))
                    {
                        responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);                        
                    }

                    byte[] buf = new byte[1024];
                    while (responseStream.Read(buf, 0, 1024) > 0)
                    {
                        m_strResponseResult += System.Text.Encoding.Default.GetString(buf);
                    }
                }

                responseStream.Close();           
                m_Response.Close();
            }
            catch
            {
            }
        }

        public string GetCookieData()
        {
            return m_strSetCookieData;
        }

        public void SetCookieData(string strData)
        {
            m_strSetCookieData = strData;
        }
                
        public string GetResponseLocation()
        {
            return m_strResponseLocation;
        }

        public string GetResponseResult()
        {
            return m_strResponseResult;
        }

        public string GetResponseDirectUrl()
        {
            return m_strResponseDirectUrl;
        }

        public void WriteLog(string strFile, byte[] arr, int nLen)
        {
            System.IO.FileStream oFS = null;
            System.IO.BinaryWriter oSW = null;

            try
            {
                oFS = new System.IO.FileStream(strFile,
                                                System.IO.FileMode.CreateNew,
                                                System.IO.FileAccess.Write);

                oSW = new System.IO.BinaryWriter(oFS);

                for (int i = 0; i < nLen; i++)
                {
                    oSW.Write(arr[i]);
                }
                oSW.Flush();
                oSW.Close();
            }
            catch
            {
            }
            finally
            {
                oSW = null;
                oFS = null;
            }
        }

        public string UrlEncode(string str)
        {
            if (str == null) return "";

            Encoding enc = Encoding.ASCII;
            StringBuilder result = new StringBuilder();

            foreach (char symbol in str)
            {
                byte[] bs = enc.GetBytes(new char[] { symbol });
                for (int i = 0; i < bs.Length; i++)
                {
                    byte b = bs[i];
                    if (b >= 48 && b < 58 || b >= 65 && b < 65 + 26 || b >= 97 && b < 97 + 26) // decode non numalphabet
                    {
                        result.Append(Encoding.ASCII.GetString(bs, i, 1));
                    }
                    else
                    {
                        result.Append('%' + String.Format("{0:X2}", (int)b));
                    }
                }
            }

            return result.ToString();
        }
    }
}
