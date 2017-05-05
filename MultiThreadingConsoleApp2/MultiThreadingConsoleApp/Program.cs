using MultiThreadingConsoleApp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApi
{
    class Program
    {
        public delegate void TestUserData(object payload, SaveData data, string url);
        public static TestUserData method;

        static void Main(string[] args)
        {
            var url = "http://auth.bungii.ccigoa:8189/api/customer/login";

            string path = @"E:\test_data_generator\Data.xml";
            SaveData data = new SaveData();

            SaveXML.Getdata(ref data, path);

            foreach (Request request in data.RequestList)
            {

                JObject code = JObject.Parse(request.Payload);
               
                foreach(var jProperty in code)
                {
                    if (jProperty.Key == "PickupRequestID")
                        code["PickupRequestID"] = "xyz";
                    var key = jProperty.Key;
                    var value = jProperty.Value;
                }
            }



           // int userCount = 1;
            method = new TestUserData(UserAPIList);
            //IEnumerable<object> User = GetUser();
            Console.WriteLine("User signing in...");

            object user = new { PhoneNo="9999999230", Password="Cci12345" };
            
            method.BeginInvoke(user, data, url, null, null);

            //foreach (var user in User)
            //{
            //    method.BeginInvoke(user,data,url,null, null);

            //    if (userCount == 5)
            //    {
            //        Thread.Sleep(10000);
            //        userCount = 0;
            //    }

            //    userCount += 1;
            //}

            Console.ReadKey();

        }

        private static void UserAPIList(object users, SaveData data, string url)
        {

           Dictionary<string, string> headers = new Dictionary<string, string>();
            Dictionary<string, string> responseLoad = new Dictionary<string, string>();

            foreach (var common in data.CommonHeaders)
            {
                headers.Add(common.Name, common.Value);
            }

            foreach (var request in data.RequestList)
            {

                JObject code = JObject.Parse(request.Payload);

                foreach (var jProperty in code)
                {
                    if (responseLoad.Keys.Contains(jProperty.Key))
                    {
                        foreach(var load in responseLoad)
                        {
                            if (jProperty.Key == load.Key)
                                code[jProperty.Key] = load.Value;
                        }
                    }
                        code["PickupRequestID"] = "xyz";
                    var key = jProperty.Key;
                    var value = jProperty.Value;
                }
                request.Payload = code.ToString();



                switch (request.MethodType.ToString())
                {
                    case "init":
                        var response = InitRequestGroup(users, request.Url, headers);
                        var httpResponseResult = response.Result;
                        var payloadResponseData = (IDictionary<string, object>)httpResponseResult.Content.ReadAsAsync<ExpandoObject>().Result;
                        foreach (var getToken in payloadResponseData)
                        {
                            if (getToken.Key == "AccessToken" && getToken.Value != null)
                            {
                                headers.Add("AuthorizationToken", getToken.Value.ToString());
                            }
                        }
                        break;

                    case "mult":
                        PostFileData(request.Payload, request.Url, headers);
                        break;

                    case "post":
                        HttpPostRequest(request.Payload, request.Url, headers);
                        break;

                    case "get":
                        HttpGetRequest(request.Url, headers);
                        break;

                    default:
                        break;
                }
            }
        }
       
      
        private static IEnumerable<object> GetUser()
        {
            return File.ReadAllLines(@"C:\Users\prathamesh.mashelkar\Downloads\CustomerUsers.csv")    //CustomerUser
            .Select(y => y.Split(','))
                      .Select(x => new
                      {
                          PhoneNo = x[0],
                          password = x[1],
                          lan1 = x[2],
                          lan2 = x[3],
                          lat1 = x[4],
                          lat2 = x[5],
                      }).ToList();
        }

        private static void PostFileData(string payload,string url, Dictionary<string, string> headerList)
        {
            byte[] ImageData = ReadImageFile();
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);

            //The content type is a header of the content, not of the request, which is why this is failing if you say just add. 
            foreach (var header in headerList)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }


            var requestContent = new MultipartFormDataContent("--Boundary-9643FD0C-8796-4E2E-AC4C-5254CC300FDC");

            var values = new[]
       {
            new KeyValuePair<string, string>("PickupRequestID", "de2d092d-7c76-c13a-0529-a2c75cede89e"),
            new KeyValuePair<string, string>("PaymentMethodID", "2798a8cf-ed0b-4b45-bb9e-9ca0a6c22419"),
            new KeyValuePair<string, string>("Description", ""),
            new KeyValuePair<string, string>("WalletRef", "b74e7a80-d8b6-11e6-9234-010812182330")
        };

            foreach (var keyValuePair in values)
            {
                requestContent.Headers.TryAddWithoutValidation("Content-Disposition", "form-data"/*;name="+keyValuePair.Key*/);
                requestContent.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
            }


            //    here you can specify boundary if you need---^
            var imageContent = new ByteArrayContent(ImageData);
            imageContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse("image/jpeg");

            requestContent.Add(imageContent, "ItemImage", "image.jpg");
            var x = JsonConvert.SerializeObject(requestContent, Formatting.Indented);
            var httpResponse = client.PostAsync(url, requestContent).Result;
            var httpResponseResult = httpResponse.Content.ReadAsAsync<ExpandoObject>().Result;

        }



        public static byte[] ReadImageFile()
        {
            string imageLocation = @"C:\Users\prathamesh.mashelkar\Pictures\Capture.png";
            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(imageLocation);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            return imageData;
        }


        private static async Task<HttpResponseMessage> InitRequestGroup(object payload, string url,Dictionary<string,string> headerList)
        {
            HttpClient client = new HttpClient();
            
            foreach(var header in headerList)
            {
               var x= client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key.ToString(),header.Value.ToString());
            }
            
            var jsonObject = JsonConvert.SerializeObject(payload);

            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            
            var httpResponse = await client.PostAsync(url, content);

            return httpResponse;
            
        }

        private static async void HttpPostRequest(string payload, string url, Dictionary<string, string> headerList)
        {
            HttpClient client = new HttpClient();

            foreach (var header in headerList)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }
            
            var content = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");

            var httpResponse =await client.PostAsync(url, content);

        }

        private static async void HttpGetRequest(string url, Dictionary<string, string> headerList)
        {
            HttpClient client = new HttpClient();
            string getID = headerList.Where(x => x.Key == "AuthorizationToken").Select(x => x.Value).Single();

            foreach (var header in headerList)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }

            var httpResponse =await  client.GetAsync(url+getID);
            
        }

    }
}