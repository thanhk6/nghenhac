using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace VSW.Lib.Global
{
    public class JsonEntity
    {
        public string Node1 { get; set; }
        public string Node2 { get; set; }
        public string Node3 { get; set; }
        public string Node4 { get; set; }
    }

    public class Json
    {
        public JsonEntity Instance { get; set; }

        public Json()
        {
            Instance = new JsonEntity
            {
                Node1 = string.Empty,
                Node2 = string.Empty,
                Node3 = string.Empty,
                Node4 = string.Empty
            };
        }

        public void Create()
        {
            var json = JsonSerializer(Instance);

            var response = System.Web.HttpContext.Current.Response;

            response.Clear();
            response.ContentType = "application/json; charset=utf-8";
            response.Write(json);
            response.End();
        }

        #region private func

        private string JsonSerializer<T>(T t)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, t);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        private T JsonDeserialize<T>(string jsonString)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)serializer.ReadObject(stream);
            }
        }

        private string GetResponse(string url)
        {
            var uri = new Uri(url);
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;

            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            if (responseStream == null) return string.Empty;

            var reader = new StreamReader(responseStream);
            var output = reader.ReadToEnd();

            response.Close();

            return output;
        }

        #endregion
    }
}