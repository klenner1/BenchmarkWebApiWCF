using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Net.Http;

namespace BenchmarkWebApiWCF
{
    [MemoryDiagnoser]
    public class Processar
    {
        private static HttpClient _httpClient;
        private readonly static string UrlBaseWcf = "http://localhost:58270/";
        private readonly static string UrlBaseWebApi = "http://localhost:5000/";

        public Processar()
        {
            _httpClient = new HttpClient();
        }

        [Benchmark]
        public void WebApi_Get_SemParametro()
        {
            var result = _httpClient.GetAsync(UrlBaseWebApi + "Service");
        }

        [Benchmark]
        public void WcfRest_Get_SemParametro()
        {
            var result = _httpClient.GetAsync(UrlBaseWcf + "Service");
        }

        [Benchmark]
        public void WcfRest_Get_Produto()
        {
            var result = _httpClient.GetAsync(UrlBaseWcf + "Produto/Buscar/?Codigo=1");
        }

        //[Benchmark]
        //public void WebApi_Post_ParametroComplexo()
        //{
        //}

        //[Benchmark]
        //public void WCF_Post_ParametroComplexo()
        //{
        //}


    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var summary = BenchmarkRunner.Run<Processar>();
            }
            finally
            {
                System.Console.ReadKey();
            }

        }
    }

}
