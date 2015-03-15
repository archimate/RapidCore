using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

/// <summary>
///JsonParse 的摘要说明
/// </summary>
public static class JsonParse
{

    public static List<T> JSONStringToList<T>(this string JsonStr)
    {
        JavaScriptSerializer Serializer = new JavaScriptSerializer();
        List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
        return objs;
    }
    public static T Deserialize<T>(string json)
    {
        T obj = Activator.CreateInstance<T>();
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            return (T)serializer.ReadObject(ms);
        }
    }
}
