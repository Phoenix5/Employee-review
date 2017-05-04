using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApiLoadTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(Headers))]
    [System.Xml.Serialization.XmlInclude(typeof(Request))]
    public class SaveData
    {
        public SaveData()
        {
            CommonHeaders = new List<Headers>();
            RequestList = new List<Request>();
        }
        public string Path { get; set; }
        public string HeaderList { get; set; }
        public List<Headers> CommonHeaders { get; set; }
        public List<Request> RequestList { get; set; }
    }


    [System.Serializable]
    public class Headers
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(Headers))]
    public class Request
    {
        public Request()
        {
            Headers = new List<Headers>();
        }
        public string Url { get; set; }
        public string MethodType { get; set; }
        public string Payload { get; set; }
        public List<Headers> Headers { get; set; }
    }

    
}
