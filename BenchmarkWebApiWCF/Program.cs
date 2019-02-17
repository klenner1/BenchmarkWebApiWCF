using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BenchmarkWebApiWCF
{   
    [MemoryDiagnoser]
    public class Processar
    {
        private static HttpClient _httpClient;
        private readonly static string UrlBaseWcf = "http://localhost:58270/";
        private readonly static string UrlBaseWebApi = "http://localhost:5000/";

        [Params(1, 5, 10, 100, 500, 1000)]
        public int Codigo { get; set; } = 1;

        public Processar()
        {
            _httpClient = new HttpClient();
        }

        [Benchmark]
        public void WcfRest_Get_Produto_task()
        {
            _httpClient.GetAsync(UrlBaseWcf + "Produto.svc/produto/Buscar?codigo=" + Codigo);
        }
        [Benchmark]
        public void WebApi_Get_Produto_task()
        {
            _httpClient.GetAsync(UrlBaseWebApi + "api/Produto/1");
        }
        [Benchmark]
        public string WcfRest_Get_Produto_Completo()
        {
            HttpResponseMessage r2 = _httpClient.GetAsync(UrlBaseWcf + "Produto.svc/produto/Buscar?codigo="+Codigo).Result;
            HttpContent stream2 = r2.Content;
            return stream2.ReadAsStringAsync().Result;
        }

        [Benchmark]
        public string WebApi_Get_Produto_Completo()
        {
            
            HttpResponseMessage r2 = _httpClient.GetAsync(UrlBaseWebApi + "api/Produto/Buscar/"+ Codigo).Result;
            HttpContent stream2 = r2.Content;
            return stream2.ReadAsStringAsync().Result;
        }
        [Benchmark]
        public string WebApi_Post_ParametroComplexo()
        {
            var parametro = new
            {
                CodigoProduto = "1",
                NomeProduto = "Produto N" + 1,
                DescricaoProduto = "",
                PrecoProduto = 1 * Math.PI,

                CodigoCategoria = (1 % 9).ToString(),
                NomeCategoria = "Categoria N" + (1 % 9),
                DescricaoCategoria = "Categoria N" + (1 % 9) + " Descrição",

                CodigoDepartamento = (1 % 10).ToString(),
                NomeDepartamento = "Departamento N" + (1 % 9),
                DescricaoDepartamento = "Departamento N" + (1 % 9) + " Descrição",

                ImpostoUniao = (0.34 * 1).ToString(),
                ImpostoEstado = (0.09 * 1).ToString(),
                ImpostoMuniciopio = (0.009 * 1).ToString()
            };



            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/Criar/", contentString).Result;


            HttpContent stream = r1.Content;
            return stream.ReadAsStringAsync().Result;
        }

        [Benchmark]
        public string WCF_Post_ParametroComplexo()
        {
            var parametro = new
            {
                CodigoProduto = "1",
                NomeProduto = "Produto N" + 1,
                DescricaoProduto = "",
                PrecoProduto = 1 * Math.PI,

                CodigoCategoria = (1 % 9).ToString(),
                NomeCategoria = "Categoria N" + (1 % 9),
                DescricaoCategoria = "Categoria N" + (1 % 9) + " Descrição",

                CodigoDepartamento = (1 % 10).ToString(),
                NomeDepartamento = "Departamento N" + (1 % 9),
                DescricaoDepartamento = "Departamento N" + (1 % 9) + " Descrição",

                ImpostoUniao = (0.34 * 1).ToString(),
                ImpostoEstado = (0.09 * 1).ToString(),
                ImpostoMuniciopio = (0.009 * 1).ToString()
            };



            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWcf + "api/produto/Criar/", contentString).Result;


            HttpContent stream = r1.Content;
            return  stream.ReadAsStringAsync().Result;
        }


    }

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //var summary = BenchmarkRunner.Run<Processar>();
                Console.WriteLine("Inicio");
                Executar();

                //Console.WriteLine(new Processar().WCF_Post_ParametroComplexo());
                Console.WriteLine("Fim");
            }
            finally
            {
                Console.ReadKey();
            }

        }

        private readonly static string UrlBaseWcf = "http://localhost:58270/";
        private readonly static string UrlBaseWebApi = "http://localhost:5000/";

        public static void Executar()
        {
            HttpClient httpClient = new HttpClient();


            var parametro = new
            {
                CodigoProduto = "1" ,
                NomeProduto = "Produto N" + 1,
                DescricaoProduto = "",
                PrecoProduto = 1 * Math.PI,

                CodigoCategoria = (1 % 9).ToString(),
                NomeCategoria = "Categoria N" + (1 % 9),
                DescricaoCategoria="Categoria N" + (1 % 9) + " Descrição",

                CodigoDepartamento= (1 % 10).ToString(),
                NomeDepartamento= "Departamento N" + (1 % 9),
                DescricaoDepartamento= "Departamento N" + (1 % 9) + " Descrição",

                ImpostoUniao= (0.34 * 1).ToString(),
                ImpostoEstado=  (0.09 * 1).ToString(),
                ImpostoMuniciopio=  (0.009 * 1).ToString()
            };

            

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //HttpResponseMessage r1 = httpClient.PostAsync(UrlBaseWebApi + "api/produto/Criar/", contentString).Result;
            HttpResponseMessage r1 = httpClient.PostAsync(UrlBaseWcf + "produto.svc/Criar/", contentString).Result;


            HttpContent stream = r1.Content;
            var data = stream.ReadAsStringAsync();
            Console.WriteLine(data.Result);

            Console.WriteLine("_________________________________");
            Console.WriteLine("_________________________________");
            Console.WriteLine("_________________________________");

            //HttpResponseMessage g1 = httpClient.GetAsync(UrlBaseWebApi + "api/Produto/Buscar/1").Result;
            HttpResponseMessage g1 = httpClient.GetAsync(UrlBaseWcf + "Produto.svc/produto/CalcularImposto?codigo=1").Result;
            HttpContent streamg1 = g1.Content;
            var datag1 = stream.ReadAsStringAsync();
            Console.WriteLine(datag1.Result);
            //HttpResponseMessage g2 = httpClient.GetAsync(UrlBaseWcf + "Produto.svc/produto/CriarSP").Result;
            //HttpContent streamg2 = g2.Content;
            //var datag2 = stream.ReadAsStringAsync();
            //System.Console.WriteLine(datag2.Result);
            //HttpResponseMessage g3 = httpClient.GetAsync(UrlBaseWcf + "Produto.svc/produto/CriarSP").Result;
            //System.Console.WriteLine(data.Result);
            //HttpContent streamg3 = g3.Content;
            //var datag3 = stream.ReadAsStringAsync();
            //System.Console.WriteLine(datag3.Result);
            /*
            System.Console.WriteLine(r1);
            System.Console.WriteLine(r1.RequestMessage);
            System.Console.WriteLine();
            System.Console.WriteLine(r1.Content);*/
            //System.Console.WriteLine("///////////////////////");


            //HttpResponseMessage r2 = httpClient.GetAsync(UrlBaseWcf + "Produto.svc/produto/Buscar?codigo=200").Result;
            //HttpContent stream2 = r2.Content;
            //var data2 = stream2.ReadAsStringAsync();


            //System.Console.WriteLine(r2);
            //System.Console.WriteLine(r2.RequestMessage);
            //System.Console.WriteLine();
            //System.Console.WriteLine(r2.Content);
            //System.Console.WriteLine(r2.IsSuccessStatusCode);
            //System.Console.WriteLine(data2.Result);
            Console.WriteLine("Fim");
            System.Console.ReadKey();
        }
       

    }

}

