using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Entidades;
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
        private readonly string UrlBaseWcf;
        private readonly string UrlBaseWebApi;

        public int CodigoWCFPost = 1;
        public int CodigoWebApiPost = 1;
        [Params("http://localhost/WCF", "http://localhost:58270/")]
        public string UrlBaseWCF;

        public List<EProduto> listaObjetos = new List<EProduto>();
        private EProduto ProdutoCore;
        private object ProdutoWCF;

        public BenchmarkPost()
        {
            _httpClient = new HttpClient();
            UrlBaseWcf = Utils.UrlBaseWcf;
            UrlBaseWebApi = Utils.UrlBaseWebApi;
        }

        [GlobalSetup]
        public void PreencherListas()
        {
            for (int i = 0; i < 9999; i++)
            {
                listaObjetos.Add(Utils.CriarObjProduto(i));
            }
            var codigoCore = CodigoWCFPost;
            var codigoWCF = CodigoWebApiPost;

            ProdutoCore = Utils.CriarObjProduto(codigoCore);
            ProdutoWCF = new { produto = Utils.CriarObjProduto(codigoWCF) };
        }

        [Benchmark]
        public string WCF_Post_Sem_Parametro_vazio()
        {
            var contentString = new StringContent("{}", Encoding.UTF8, "application/json");
            HttpResponseMessage r1 = _httpClient.PostAsync(/*UrlBaseWcf*/ UrlBaseWCF + "Produto.svc/produto/CriarVazio?", contentString).Result;
            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            return retorno;
        }

        ///[Benchmark]
        public string WebApi_Post_Sem_Parametro_Vazio()
        {
            var contentString = new StringContent("{}", Encoding.UTF8, "application/json");
            return _httpClient.PostAsync(UrlBaseWebApi + "api/produto/CriarVazio/", contentString).Result.Content.ReadAsStringAsync().Result;
        }


        [Benchmark]
        public string WCF_Post_Sem_Parametro()
        {
            var contentString = new StringContent("{}", Encoding.UTF8, "application/json");
            return _httpClient.PostAsync(/*UrlBaseWcf*/UrlBaseWCF + "Produto.svc/produto/Adicionar?", contentString).Result.Content.ReadAsStringAsync().Result;
        }

        // [Benchmark]
        public string WebApi_Post_Sem_Parametro()
        {
            var contentString = new StringContent("{}", Encoding.UTF8, "application/json");
            return _httpClient.PostAsync(UrlBaseWebApi + "api/produto/Adicionar/", contentString).Result.Content.ReadAsStringAsync().Result;
        }

        [Benchmark]
        public string WCF_Post_Parametro()
        {
            var jsonContent = JsonConvert.SerializeObject(ProdutoWCF);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return _httpClient.PostAsync(/*UrlBaseWcf*/UrlBaseWCF + "Produto.svc/produto/Criar?", contentString).Result.Content.ReadAsStringAsync().Result;
        }

        //[Benchmark]
        public string WebApi_Post_Parametro()
        {
            var jsonContent = JsonConvert.SerializeObject(ProdutoCore);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return _httpClient.PostAsync(UrlBaseWebApi + "api/produto/Criar/", contentString).Result.Content.ReadAsStringAsync().Result;
        }

        [Benchmark]
        public string WCF_Post_Lista()
        {
            var parametro = new { produtos = listaObjetos };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return _httpClient.PostAsync(/*UrlBaseWcf*/UrlBaseWCF + "Produto.svc/produto/CriarVarios?", contentString).Result.Content.ReadAsStringAsync().Result;

        }

       // [Benchmark]
        public string WebApi_Post_Lista()
        {
            var parametro = listaObjetos;
            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return _httpClient.PostAsync(UrlBaseWebApi + "api/produto/CriarVarios/", contentString).Result.Content.ReadAsStringAsync().Result;
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            Utils.LimparListas(_httpClient);
        }
    }


    [MemoryDiagnoser]
    public class BenchmarkGet
    {
        private static HttpClient _httpClient;
        private readonly string UrlBaseWcf;
        private readonly string UrlBaseWebApi;

        public int CodigoWCFGet { get; set; } = 1;
        public int CodigoWebApiGet { get; set; } = 1;
        public int RegistrosIniciais { get; set; } = 9999;

        public BenchmarkGet()
        {
            _httpClient = new HttpClient();
            UrlBaseWcf = Utils.UrlBaseWcf;
            UrlBaseWebApi = Utils.UrlBaseWebApi;
        }

        [GlobalSetup]
        public void PreencherListas()
        {

            var parametro = new
            {
                Count = RegistrosIniciais
            };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage wcf = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/AdicionarVarios?", contentString).Result;

            jsonContent = JsonConvert.SerializeObject(RegistrosIniciais);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
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

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            Utils.LimparListas(_httpClient);
        }
    }

    [MemoryDiagnoser]
    public class BenchmarkAtualizar
    {
        private readonly HttpClient _httpClient;
        private readonly string UrlBaseWcf;
        private readonly string UrlBaseWebApi;

        public int CodigoWCFAtualizar { get; set; } = 1;
        public int CodigoWebAtualizar { get; set; } = 1;
        public int CodigoWCFAtualizarTodos { get; set; } = 1;
        public int CodigoWebApiAtualizarTodos { get; set; } = 1;
        [Params(1, 100, 1000, 10000, 100000, 500000, 1000000, 5000000)]
        public int RegistrosIniciais { get; set; }
        private EProduto ProdutoCore;
        private object ProdutoWCF;

        public BenchmarkAtualizar()
        {
            _httpClient = new HttpClient();
            UrlBaseWcf = Utils.UrlBaseWcf;
            UrlBaseWebApi = Utils.UrlBaseWebApi;
        }

        [GlobalSetup]
        public void PreencherListas()
        {
            Console.WriteLine(RegistrosIniciais);
            var parametro = new
            {
                count = RegistrosIniciais
            };

            var jsonContent = JsonConvert.SerializeObject(parametro);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage wcf = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/AdicionarVarios?", contentString).Result;

            jsonContent = JsonConvert.SerializeObject(RegistrosIniciais);
            contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage core = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/AdicionarVarios/", contentString).Result;
        }

        [IterationSetup]
        public void CriarProduto()
        {
            var codigoCore = CodigoWebAtualizar;
            var codigoWCF = CodigoWCFAtualizar;

            ProdutoCore = Utils.CriarObjProduto(codigoCore);
            ProdutoWCF = new { produto = Utils.CriarObjProduto(codigoWCF) };
        }

        [Benchmark]
        public string WCF_Post_Atualizar()
        {
            var jsonContent = JsonConvert.SerializeObject(ProdutoWCF);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/Atualizar?", contentString).Result;
            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWCFAtualizar++;
            return retorno;
        }

        [Benchmark]
        public string WebApi_Post_Atualizar()
        {
            var jsonContent = JsonConvert.SerializeObject(ProdutoCore);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/Atualizar/", contentString).Result;
            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWebAtualizar++;
            return retorno;
        }
        [Benchmark]
        public string WCF_Post_Atualizar_Todos()
        {
            var jsonContent = JsonConvert.SerializeObject(ProdutoWCF);
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
            var jsonContent = JsonConvert.SerializeObject(ProdutoCore);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage r1 = _httpClient.PostAsync(UrlBaseWebApi + "api/produto/AtualizarTodos/", contentString).Result;
            HttpContent stream = r1.Content;
            string retorno = stream.ReadAsStringAsync().Result;
            CodigoWebApiAtualizarTodos++;
            return retorno;
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            Utils.LimparListas(_httpClient);
        }

    }

    public static class Utils
    {

        public readonly static string UrlBaseWcf = "http://localhost:58270/";
        public readonly static string UrlBaseWebApi = "http://localhost:5000/";

        public static void LimparListas(HttpClient httpClient)
        {
            HttpResponseMessage core = httpClient.PostAsync(UrlBaseWebApi + "api/produto/RemoverTodos/", null).Result;
            HttpResponseMessage wcf = httpClient.PostAsync(UrlBaseWcf + "Produto.svc/produto/RemoverTodos?", null).Result;
        }
        public static EProduto CriarObjProduto(int codigo)
        {
            return new EProduto
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

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Escolher();
            }
            finally
            {
                Console.ReadKey();
            }

        }

        private static void Escolher()
        {
            Console.WriteLine("1 : BenchmarkPost");
            Console.WriteLine("2 : BenchmarkGet");
            Console.WriteLine("3 : BenchmarkAtualizar");
            int opcao = int.Parse(Console.ReadLine());
            if (opcao == 1)
            {
                var summary = BenchmarkRunner.Run<BenchmarkPost>();
            }
            else if (opcao == 2)
            {
                var summary = BenchmarkRunner.Run<BenchmarkGet>();
            }
            else if (opcao == 3)
            {
                var summary = BenchmarkRunner.Run<BenchmarkAtualizar>();
            }
            else
            {
                //outras opção para testes
                Console.WriteLine(new BenchmarkPost().WebApi_Post_Sem_Parametro_Vazio());
            }
            if (opcao != 0)
            {
                Escolher();
            }
        }

    }

}

