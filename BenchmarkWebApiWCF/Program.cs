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
    public class BenchmarkPost
    {
        private static HttpClient _httpClient;
        private readonly static string UrlBaseWcf = "http://localhost:58270/";
        private readonly static string UrlBaseWebApi = "http://localhost:5000/";
        
        public int CodigoWCFPost = 1;
        public int CodigoWebApiPost  = 1;

        public List<Object> listaObjetos = new List<object>();

        public BenchmarkPost()
        {
            _httpClient = new HttpClient();
        }


        [GlobalSetup]
        public void PreencherListas()
        {
            for (int i = 0; i < 9999; i++)
            {
                listaObjetos.Add(CriarObjProduto(i));
            }
        }

        [Benchmark]
        public string WCF_Post_Sem_Parametro_vazio()
        {
            var contentString = new StringContent("{}", Encoding.UTF8, "application/json");
            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/CriarVazio?", contentString).Result;
            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            return retorno;
        }

        [Benchmark]
        public string WebApi_Post_Sem_Parametro_Vazio()
        {
            var contentString = new StringContent("{}", Encoding.UTF8, "application/json");
            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/CriarVazio/", contentString).Result;
            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            return retorno;
        }


        [Benchmark]
        public string WCF_Post_Sem_Parametro()
        {
            var contentString = new StringContent("{}", Encoding.UTF8, "application/json");
            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/Adicionar?", contentString).Result;
            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            return retorno;
        }

        [Benchmark]
        public string WebApi_Post_Sem_Parametro()
        {
            var contentString = new StringContent("{}", Encoding.UTF8, "application/json");
            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/Adicionar/", contentString).Result;
            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            return retorno;
        }


        [Benchmark]
        public string WCF_Post_Parametro()
        {
            var codigo = CodigoWCFPost;
            var parametro = new { produto = CriarObjProduto(codigo) };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/Criar?", contentString).Result;

            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWCFPost++;
            return retorno;
        }

        [Benchmark]
        public string WebApi_Post_Parametro()
        {
            var codigo = CodigoWebApiPost;
            var parametro = CriarObjProduto(codigo);
            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/Criar/", contentString).Result;

            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWebApiPost++;
            return retorno;
        }

        [Benchmark]
        public string WCF_Post_Lista()
        {
            var codigo = CodigoWCFPost;
            var parametro = new { produtos = listaObjetos };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/CriarVarios?", contentString).Result;

            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWCFPost++;
            return retorno;
        }

        [Benchmark]
        public string WebApi_Post_Lista()
        {
            var codigo = CodigoWebApiPost;
            var parametro = listaObjetos;
            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/CriarVarios/", contentString).Result;

            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWebApiPost++;
            return retorno;
        }

        private object CriarObjProduto(int codigo)
        {
            return new
            {
                CodigoProduto = codigo,
                NomeProduto = "Produto N" + codigo,
                DescricaoProduto = "",
                PrecoProduto = codigo * Math.PI,

                CodigoCategoria = codigo % 9,
                NomeCategoria = "Categoria N" + (codigo % 9),
                DescricaoCategoria = "Categoria N" + (codigo % 9) + " Descrição",

                CodigoDepartamento = codigo % 10,
                NomeDepartamento = "Departamento N" + (codigo % 9),
                DescricaoDepartamento = "Departamento N" + (codigo % 9) + " Descrição",

                ImpostoUniao = 0.34 * codigo,
                ImpostoEstado = 0.09 * codigo,
                ImpostoMuniciopio = 0.009 * codigo
            };
        }

    }


    [MemoryDiagnoser]
    public class BenchmarkPostGet
    {
        private static HttpClient _httpClient;
        private readonly static string UrlBaseWcf = "http://localhost:58270/";
        private readonly static string UrlBaseWebApi = "http://localhost:5000/";

        public int CodigoWCFGet { get; set; } = 1;
        public int CodigoWebApiGet { get; set; } = 1;

        public BenchmarkPostGet()
        {
            _httpClient = new HttpClient();
        }

        [GlobalSetup]
        public void PreencherListas()
        {

            var parametro = new
            {
                Count = 9999
            };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage wcf = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/AdicionarVarios?", contentString).Result;
            HttpResponseMessage core = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/AdicionarVarios/", contentString).Result;
        }

        [Benchmark]
        public string WcfRest_Get_Produto()
        {
            HttpResponseMessage r2 = _httpClient.GetAsync(UrlBaseWcf + "Produto.svc/produto/Buscar?codigo=" + CodigoWCFGet).Result;
            HttpContent stream2 = r2.Content;
            var retorno = stream2.ReadAsStringAsync().Result;
            CodigoWCFGet++;
            return retorno;
        }

        [Benchmark]
        public string WebApi_Get_Produto()
        {
            HttpResponseMessage r2 = _httpClient.GetAsync(UrlBaseWebApi + "api/Produto/Buscar/" + CodigoWebApiGet).Result;
            HttpContent stream = r2.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWebApiGet++;
            return retorno;
        }
        [Benchmark]
        public string WcfRest_Get_Produto_All()
        {
            HttpResponseMessage r2 = _httpClient.GetAsync(UrlBaseWcf + "Produto.svc/produto/BuscarTodos").Result;
            HttpContent stream2 = r2.Content;
            return stream2.ReadAsStringAsync().Result;
        }

        [Benchmark]
        public string WebApi_Get_Produto_All()
        {
            HttpResponseMessage r2 = _httpClient.GetAsync(UrlBaseWebApi + "api/Produto/BuscarTodos/").Result;
            HttpContent stream2 = r2.Content;
            return stream2.ReadAsStringAsync().Result;
        }
    }

    [MemoryDiagnoser]
    public class BenchmarkAtualizar
    {
        private static HttpClient _httpClient;
        private readonly static string UrlBaseWcf = "http://localhost:58270/";
        private readonly static string UrlBaseWebApi = "http://localhost:5000/";
        
        public int CodigoWCFAtualizar { get; set; } = 1;
        public int CodigoWebAtualizar { get; set; } = 1;
        public int CodigoWCFAtualizarTodos { get; set; } = 1;
        public int CodigoWebApiAtualizarTodos { get; set; } = 1;

        public BenchmarkAtualizar()
        {
            _httpClient = new HttpClient();
        }

        [GlobalSetup]
        public void PreencherListas()
        {

            var parametro = new
            {
                Count = 99999
            };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage wcf = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/AdicionarVarios?", contentString).Result;
            HttpResponseMessage core = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/AdicionarVarios/", contentString).Result;
        }

        [Benchmark]
        public string WCF_Post_Atualizar()
        {
            var codigo = CodigoWCFAtualizar;
            var parametro = new
            {
                produto = new
                {
                    CodigoProduto = codigo,
                    NomeProduto = "Produto N" + codigo,
                    DescricaoProduto = "Modificado",
                    PrecoProduto = codigo * Math.PI * 2,

                    CodigoCategoria = codigo % 9,
                    NomeCategoria = "Categoria N" + (codigo % 9) + "Modificado",
                    DescricaoCategoria = "Categoria N" + (codigo % 9) + " Descrição Modificado",

                    CodigoDepartamento = codigo % 10,
                    NomeDepartamento = "Departamento N" + (codigo % 9),
                    DescricaoDepartamento = "Departamento N" + (codigo % 9) + " Descrição Modificado",

                    ImpostoUniao = 0.34 * codigo * 2,
                    ImpostoEstado = 0.09 * codigo * 2,
                    ImpostoMuniciopio = 0.009 * codigo * 2
                }
            };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/Atualizar?", contentString).Result;

            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            Console.WriteLine(retorno);
            CodigoWCFAtualizar++;
            return retorno;
        }

        [Benchmark]
        public string WebApi_Post_Atualizar()
        {
            var codigo = CodigoWebAtualizar;
            var parametro = new
            {
                CodigoProduto = codigo,
                NomeProduto = "Produto N" + codigo + "Modificado",
                DescricaoProduto = "Modificado",
                PrecoProduto = codigo * Math.PI * 2,

                CodigoCategoria = codigo % 9,
                NomeCategoria = "Categoria N" + (codigo % 9) + "Modificado",
                DescricaoCategoria = "Categoria N" + (codigo % 9) + " Descrição  Modificado",

                CodigoDepartamento = codigo % 10,
                NomeDepartamento = "Departamento N" + (codigo % 9) + "Modificado",
                DescricaoDepartamento = "Departamento N" + (codigo % 9) + " Descrição  Modificado",

                ImpostoUniao = 0.34 * codigo * 2,
                ImpostoEstado = 0.09 * codigo * 2,
                ImpostoMuniciopio = 0.009 * codigo * 2

            };
            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/Atualizar/", contentString).Result;

            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            Console.WriteLine(retorno);
            CodigoWebAtualizar++;
            return retorno;
        }
        [Benchmark]
        public string WCF_Post_Atualizar_Todos()
        {
            var codigo = CodigoWCFAtualizarTodos;
            var parametro = new
            {
                produto = new
                {
                    CodigoProduto = codigo,
                    NomeProduto = "Produto N" + codigo,
                    DescricaoProduto = "Modificado",
                    PrecoProduto = codigo * Math.PI * 2,

                    CodigoCategoria = codigo % 9,
                    NomeCategoria = "Categoria N" + (codigo % 9) + "Modificado",
                    DescricaoCategoria = "Categoria N" + (codigo % 9) + " Descrição Modificado",

                    CodigoDepartamento = codigo % 10,
                    NomeDepartamento = "Departamento N" + (codigo % 9),
                    DescricaoDepartamento = "Departamento N" + (codigo % 9) + " Descrição Modificado",

                    ImpostoUniao = 0.34 * codigo * 2,
                    ImpostoEstado = 0.09 * codigo * 2,
                    ImpostoMuniciopio = 0.009 * codigo * 2
                }
            };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/AtualizarTodos?", contentString).Result;

            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWCFAtualizarTodos++;
            return retorno;
        }

        [Benchmark]
        public string WebApi_Post_Atualizar_Todos()
        {
            var codigo = CodigoWebApiAtualizarTodos;
            var parametro = new
            {
                CodigoProduto = codigo,
                NomeProduto = "Produto N" + codigo + "Modificado",
                DescricaoProduto = "Modificado",
                PrecoProduto = codigo * Math.PI * 2,

                CodigoCategoria = codigo % 9,
                NomeCategoria = "Categoria N" + (codigo % 9) + "Modificado",
                DescricaoCategoria = "Categoria N" + (codigo % 9) + " Descrição  Modificado",

                CodigoDepartamento = codigo % 10,
                NomeDepartamento = "Departamento N" + (codigo % 9) + "Modificado",
                DescricaoDepartamento = "Departamento N" + (codigo % 9) + " Descrição  Modificado",

                ImpostoUniao = 0.34 * codigo * 2,
                ImpostoEstado = 0.09 * codigo * 2,
                ImpostoMuniciopio = 0.009 * codigo * 2

            };
            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/AtualizarTodos/", contentString).Result;

            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWebApiAtualizarTodos++;
            return retorno;
        }

    }



    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("1 : BenchmarkPost");
                Console.WriteLine("2 : BenchmarkPostGet");
                Console.WriteLine("3 : BenchmarkAtualizar");
                int opcao = int.Parse(Console.ReadLine());
                if (opcao == 1)
                {
                    var summary = BenchmarkRunner.Run<BenchmarkPost>();
                }
                else if(opcao == 2)
                {
                    var summary = BenchmarkRunner.Run<BenchmarkPostGet>();
                }
                else if (opcao == 3)
                {
                    var summary = BenchmarkRunner.Run<BenchmarkAtualizar>();
                } 
            }
            finally
            {
                Console.ReadKey();
            }

        }



    }

}

