using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApi
{
    public class ReusableContent : HttpContent
    {
        private readonly HttpContent _innerContent;

        public ReusableContent(HttpContent innerContent)
        {
            _innerContent = innerContent;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            await _innerContent.CopyToAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            // Don't call base dispose
            //base.Dispose(disposing);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            var url = "http://auth.bungii.ccigoa:8189/";
            var api = "api/customer/login";

            IEnumerable<object> result = GetCustomers();

            foreach (var o in result)
            {
                var response = PostData(o, url, api);
                //var response2 = PostPickup(response);
                PostMultipart(response);
            }
            Console.WriteLine("...........Finished...........");
            Console.ReadKey();

        }

        private static IEnumerable<object> GetCustomers()
        {
            return File.ReadAllLines(@"C:\Users\prathamesh.mashelkar\Downloads\CustomerUsers.csv")
          .Select(y => y.Split(','))
                      .Select(x => new
                      {
                          PhoneNo = x[0],
                          Password = x[1]
                      }).ToList();
        }

        //private static object PostPickup(Dictionary<string, object> response)
        //{
        //    HttpClient client = new HttpClient();
        //    string url = "http://bungiinodejs.chapora.ccigoa:8282";
        //    string api = "";

        //    client.BaseAddress = new Uri(url);

        //    //The content type is a header of the content, not of the request, which is why this is failing if you say just add. 
        //    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        //    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic Zjg5ZTY2ZDQtZjhmMi00NDhhLWFmYTUtYjdjMDk0OTNmMTk2OmRjZGFmNDFiLWFmMzctNGRhYy05Y2ZlLThkNTUyMzZmYmY1Zg==");
        //    client.DefaultRequestHeaders.TryAddWithoutValidation("AppVersion", "2.2.0");
        //    foreach (var data in response)
        //    {
        //        if (data.Key == "AccessToken" && data.Value != null)
        //        {
        //            client.DefaultRequestHeaders.TryAddWithoutValidation("AuthorizationToken", data.Value.ToString());
        //        }
        //    }
        //}

        private static void PostMultipart(Dictionary<string,object> response)
        {
            byte[] ImageData = ReadImageFile();
            string api = "api/pickup/customerconfirmation";
            string url= "http://bungiinodejs.chapora.ccigoa:8282";
            HttpClient client = new HttpClient();
            
            client.BaseAddress = new Uri(url);

            //The content type is a header of the content, not of the request, which is why this is failing if you say just add. 
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic Zjg5ZTY2ZDQtZjhmMi00NDhhLWFmYTUtYjdjMDk0OTNmMTk2OmRjZGFmNDFiLWFmMzctNGRhYy05Y2ZlLThkNTUyMzZmYmY1Zg==");
            client.DefaultRequestHeaders.TryAddWithoutValidation("AppVersion", "2.2.0");

            foreach (var data in response)
            {
                if (data.Key == "AccessToken" && data.Value!=null)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("AuthorizationToken", data.Value.ToString());
                }
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
            var x= JsonConvert.SerializeObject(requestContent, Formatting.Indented);
            var httpResponse= client.PostAsync(api, requestContent).Result;
            var httpResponseResult = httpResponse.Content.ReadAsAsync<ExpandoObject>().Result;

        }



        public static byte[] ReadImageFile()
        {
            string imageLocation = "C:\\Users\\prathamesh.mashelkar\\Pictures\\Capture.png";
            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(imageLocation);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            return imageData;
        }



        private static Dictionary<string,object> PostData(object payload,string url,string api)
        {
            
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);

            //The content type is a header of the content, not of the request, which is why this is failing if you say just add. 
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic Zjg5ZTY2ZDQtZjhmMi00NDhhLWFmYTUtYjdjMDk0OTNmMTk2OmRjZGFmNDFiLWFmMzctNGRhYy05Y2ZlLThkNTUyMzZmYmY1Zg==");
            
            var jsonObject = JsonConvert.SerializeObject(payload);

            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            ReusableContent contentNoDispose = new ReusableContent(content);
            
            var httpResponseResult = client.PostAsync(api, contentNoDispose).Result;

            //Directly posting as Json
            //var httpResponseResult = client.PostAsJsonAsync(api, payload).Result;


            //Alternate way to read as string and then deserialize as dynamic type
            //var json = httpResponseResult.Content.ReadAsStringAsync().Result;
            //dynamic results = JsonConvert.DeserializeObject<ExpandoObject>(json);

            var xx = httpResponseResult.RequestMessage.Content.ReadAsStringAsync().Result;

            var payloadResponseData = httpResponseResult.Content.ReadAsAsync<ExpandoObject>().Result;


            //Converting the payload anonymous object to dictionary
            var type = payload.GetType();
            var props = type.GetProperties();
            var dictionaryPayload = props.ToDictionary(x => x.Name, x => x.GetValue(payload, null));
            
            //Converting the api response to dictionary
            var dictionaryResponse = (IDictionary<string, object>)payloadResponseData;

            //Merging payload and response into single dictionary
            foreach (var key in dictionaryResponse)
            {
                dictionaryPayload.Add(key.Key, key.Value);
                var k = key.Key;
                var v = key.Value;
            }
            

            if (httpResponseResult.IsSuccessStatusCode)//If Authentication successful
            {

                foreach (var data in dictionaryPayload)
                {
                    Console.WriteLine(data.Key+":"+data.Value+"  ");
                }
                Console.WriteLine("\n");



                //Final object to return
               // object resultFinal = dictionaryPayload;
                return dictionaryPayload;
               
            }
            else//If authentication fails
            {
                foreach (var data in dictionaryPayload)
                {
                    if (data.Key == "Error")//Error property contains an object
                    {
                        //Extracting error object in case error has occured 
                        var dictionary3 = (IDictionary<string, object>)data.Value;

                        foreach (var error in dictionary3)
                        {
                            Console.WriteLine(error.Key+":"+error.Value);
                        }

                    }
                    else
                    {
                        Console.WriteLine(data.Key + ":" + data.Value + "  ");
                    }
                }
                Console.WriteLine("\n");

                //Final object to return
               // object resultFinal = dictionaryPayload;

                return dictionaryPayload;
            }
        }
    }
}
