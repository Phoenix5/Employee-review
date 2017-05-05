using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadingConsoleApp
{
    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(Header))]
    [System.Xml.Serialization.XmlInclude(typeof(Request))]
    public class SaveData
    {
        public SaveData()
        {
            CommonHeaders = new List<Header>();
            RequestList = new List<Request>();
        }
        public string Path { get; set; }
        public string HeaderList { get; set; }
        public List<Header> CommonHeaders { get; set; }
        public List<Request> RequestList { get; set; }
    }


    [System.Serializable]
    public class Header
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(Header))]
    public class Request
    {
        public Request()
        {
            Headers = new List<Header>();
        }
        public string Url { get; set; }
        public string MethodType { get; set; }
        public string Payload { get; set; }
        public List<Header> Headers { get; set; }
    }


}


