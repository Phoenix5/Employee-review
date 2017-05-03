using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApi
{
   

    class User
    {
        public User(string phoneNo, string password, string lan1, string lan2, string lat1, string lat2)
        {
            PhoneNo = phoneNo;
            Password = password;
            Lan1 = lan1;
            Lan2 = lan2;
            Lat1 = lat1;
            Lat2 = lat2;


        }

        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string Lan1 { get; set; }
        public string Lan2 { get; set; }
        public string Lat1 { get; set; }
        public string Lat2 { get; set; }
    }

    public class Response
    {
        public string AccessToken { get; set; }
        public string Status { get; set; }
        public string RequestId { get; set; }
        //public string PhoneNo { get; set; }
        //public string Password { get; set; }
        public Error Error { get; set; }

    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }

    class Program
    {
        public delegate Task<HttpResponseMessage> PostUserData<T>(T payload, string url, string api) where T : class;
        public static PostUserData<User> method;
        public static Task<HttpResponseMessage> task1;

        static void Main(string[] args)
        {
            var url = "http://auth.bungii.ccigoa:8189/";
            var api = "api/customer/login";

            int userCount = 1;
            method = new PostUserData<User>(PostData);
            IEnumerable<User> User = GetUser();
            Console.WriteLine("User signing in...");

            foreach (var user in User)
            {
                method.BeginInvoke(user,url,api,Result, null);

                if (userCount == 5)
                {
                    Thread.Sleep(10000);
                    userCount = 0;
                }

                userCount += 1;
            }

            Console.ReadKey();

        }

        private static AsyncCallback Result = new AsyncCallback((ar) =>
        {
            task1 = method.EndInvoke(ar);
            var httpResponseResult = task1.Result;

            var request = httpResponseResult.RequestMessage.Content.ReadAsAsync<User>().Result;
            var response = httpResponseResult.Content.ReadAsAsync<Response>().Result;

            if (httpResponseResult.IsSuccessStatusCode)
            {
                
               Console.WriteLine("Username: " + request.PhoneNo + " Password: " + request.Password + " || Status Code:" +
               httpResponseResult.StatusCode + " || Message : " + httpResponseResult.ReasonPhrase +"|| Status:  " + response.Status);
                
            }
            else
            {
                Console.WriteLine("Username: " + request.PhoneNo + " || Status Code:" +
                httpResponseResult.StatusCode + " || Message : " + httpResponseResult.ReasonPhrase + " || Status: " + response.Status + "|| Error Code: " + response.Error.Code + "|| Error Message: " + response.Error.Message+"");

            }
        });
      
        private static IEnumerable<User> GetUser()
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
                      }).Select(x => new User(x.PhoneNo, x.password, x.lan1, x.lan2, x.lat1, x.lat2))
                         .ToList();
        }


        private static Task<HttpResponseMessage> PostData<Tpayload>(Tpayload payload, string url, string api) where Tpayload:class
        {
           // Console.WriteLine(payload.PhoneNo + " signing in...");
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic Zjg5ZTY2ZDQtZjhmMi00NDhhLWFmYTUtYjdjMDk0OTNmMTk2OmRjZGFmNDFiLWFmMzctNGRhYy05Y2ZlLThkNTUyMzZmYmY1Zg==");

            var httpResponseMessage =client.PostAsJsonAsync(api, payload);
            

            return httpResponseMessage;
            
        }
    }
}