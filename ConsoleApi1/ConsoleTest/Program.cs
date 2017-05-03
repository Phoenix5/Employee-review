namespace ConsoleTest
{
        using System;
        using System.Net.Http;

        class Program
        {
            private static readonly int _connections = 10;
            private static readonly HttpClient _httpClient = new HttpClient();

            private static void Main()
            {
                TestHttpClientWithStaticInstance();
                TestHttpClientWithUsing();
            Console.ReadLine();
            }

            private static void TestHttpClientWithUsing()
            {
                try
                {
                    for (var i = 0; i < _connections; i++)
                    {
                        using (var httpClient = new HttpClient())
                        {
                            var result = httpClient.GetAsync(new Uri("http://bing.com")).Result;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }

            private static void TestHttpClientWithStaticInstance()
            {
                try
                {
                    for (var i = 0; i < _connections; i++)
                    {
                        var result = _httpClient.GetAsync(new Uri("http://bing.com")).Result;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
        }
    }

