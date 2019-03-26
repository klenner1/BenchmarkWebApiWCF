using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Entidades;
using MessagePack;
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

        private EProduto ProdutoCore;
        private object ProdutoWCF;

        public BenchmarkPost()
        {
            _httpClient = new HttpClient();
            UrlBaseWcf = Utils.UrlBaseWcf;
            UrlBaseWebApi = Utils.UrlBaseWebApi;
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var codigoCore = CodigoWCFPost;
            var codigoWCF = CodigoWebApiPost;
            ProdutoCore = Utils.CriarObjProduto(codigoCore);
            ProdutoWCF = new { produto = Utils.CriarObjProduto(codigoWCF) };
        }

       // [Benchmark]
        public string WCF_Post_Sem_Parametro_vazio()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWcf + "Produto.svc/produto/CriarVazio?", null);
        }

        [Benchmark]
        public string WebApi_Post_Sem_Parametro_Vazio()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWebApi + "api/produto/CriarVazio/", null);
        }

        //[Benchmark]
        public string WCF_Post_Sem_Parametro()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWcf + "Produto.svc/produto/Adicionar?", null);
        }

        [Benchmark]
        public string WebApi_Post_Sem_Parametro()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWebApi + "api/produto/Adicionar/", null);
        }

        //[Benchmark]
        public string WCF_Post_Parametro()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWcf + "Produto.svc/produto/Criar?", ProdutoWCF);
        }

        [Benchmark]
        public string WebApi_Post_Parametro()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWebApi + "api/produto/Criar/", ProdutoCore);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            Utils.LimparListas(_httpClient);
        }
    }

    [MemoryDiagnoser]
    public class BenchmarkPostLista
    {
        private static HttpClient _httpClient;
        private readonly string UrlBaseWcf;
        private readonly string UrlBaseWebApi;

        public List<EProduto> listaObjetos = new List<EProduto>();

        [Params(1, 100, 1000, 10000)]
        public int TamanhoLista { get; set; }

        public BenchmarkPostLista()
        {
            _httpClient = new HttpClient();
            UrlBaseWcf = Utils.UrlBaseWcf;
            UrlBaseWebApi = Utils.UrlBaseWebApi;
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 0; i < TamanhoLista; i++)
            {
                listaObjetos.Add(Utils.CriarObjProduto(i));
            }
        }
        //[Benchmark]
        public string WCF_Post_Lista()
        {
            var parametro = new { produtos = listaObjetos };
            return Utils.RequisicaoPost(_httpClient, UrlBaseWcf + "Produto.svc/produto/CriarVarios?", parametro);
        }

        [Benchmark]
        public string WebApi_Post_Lista()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWebApi + "api/produto/CriarVarios/", listaObjetos);
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
        [Params(1, 100, 1000, 10000,100000)]
        public int RegistrosIniciais { get; set; }

        public BenchmarkGet()
        {
            _httpClient = new HttpClient();
            UrlBaseWcf = Utils.UrlBaseWcf;
            UrlBaseWebApi = Utils.UrlBaseWebApi;
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var parametro = new { count = RegistrosIniciais };
            Utils.RequisicaoPost(_httpClient, UrlBaseWcf + "Produto.svc/produto/AdicionarVarios?", parametro);
            Utils.RequisicaoPost(_httpClient, UrlBaseWebApi + "api/produto/AdicionarVarios/", RegistrosIniciais);
        }

        //[Benchmark]
        public string Wcf_Get_Produto()
        {
            return Utils.RequisicaoGet(_httpClient, UrlBaseWcf + "Produto.svc/produto/Buscar?codigo=" + CodigoWCFGet);
        }

        [Benchmark]
        public string WebApi_Get_Produto()
        {
            return Utils.RequisicaoGet(_httpClient, UrlBaseWebApi + "api/Produto/Buscar/" + CodigoWebApiGet);
        }
        //[Benchmark]
        public string Wcf_Get_Produto_All()
        {
            return Utils.RequisicaoGet(_httpClient, UrlBaseWcf + "Produto.svc/produto/BuscarTodos");
        }

        [Benchmark]
        public string WebApi_Get_Produto_All()
        {
            return Utils.RequisicaoGet(_httpClient, UrlBaseWebApi + "api/Produto/BuscarTodos/");
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
        public void GlobalSetup()
        {
            var parametro = new { count = RegistrosIniciais };
            Utils.RequisicaoPost(_httpClient, UrlBaseWcf + "Produto.svc/produto/AdicionarVarios?", parametro);
            Utils.RequisicaoPost(_httpClient, UrlBaseWebApi + "api/produto/AdicionarVarios/", RegistrosIniciais);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            var codigoCore = CodigoWebAtualizar++;
            var codigoWCF = CodigoWCFAtualizar++;

            ProdutoCore = Utils.CriarObjProduto(codigoCore);
            ProdutoWCF = new { produto = Utils.CriarObjProduto(codigoWCF) };
        }

        //[Benchmark]
        public string WCF_Post_Atualizar()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWcf + "Produto.svc/produto/Atualizar?", ProdutoWCF);
        }

        [Benchmark]
        public string WebApi_Post_Atualizar()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWebApi + "api/produto/Atualizar/", ProdutoCore);
        }
        //[Benchmark]
        public string WCF_Post_Atualizar_Todos()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWcf + "Produto.svc/produto/AtualizarTodos?", ProdutoWCF);
        }

        [Benchmark]
        public string WebApi_Post_Atualizar_Todos()
        {
            return Utils.RequisicaoPost(_httpClient, UrlBaseWebApi + "api/produto/AtualizarTodos/", ProdutoCore);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            Utils.LimparListas(_httpClient);
        }

    }

    public static class Utils
    {

        public readonly static string UrlBaseWcf = "http://localhost/WCF/";
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
        public static string MessagePackToJson(object obj)
        {
            var bytes = MessagePackSerializer.Serialize(obj);
            return MessagePackSerializer.ToJson(bytes);
        }

        public static string RequisicaoPost(HttpClient httpClient, string url, object obj)
        {
            //var jsonContent = JsonConvert.SerializeObject(obj);
            var jsonContent = MessagePackToJson(obj);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage r1 = httpClient.PostAsync(url, contentString).Result;
            HttpContent stream = r1.Content;
            return stream.ReadAsStringAsync().Result;
        }

        public static string RequisicaoGet(HttpClient httpClient, string url)
        {
            HttpResponseMessage r2 = httpClient.GetAsync(url).Result;
            HttpContent stream = r2.Content;
            return stream.ReadAsStringAsync().Result;
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
            Console.WriteLine("2 : BenchmarkPostLista");
            Console.WriteLine("3 : BenchmarkGet");
            Console.WriteLine("4 : BenchmarkAtualizar");
            int opcao = int.Parse(Console.ReadLine());
            if (opcao == 1)
            {
                var summary = BenchmarkRunner.Run<BenchmarkPost>();
            }
            if (opcao == 2)
            {
                var summary = BenchmarkRunner.Run<BenchmarkPostLista>();
            }
            else if (opcao == 3)
            {
                var summary = BenchmarkRunner.Run<BenchmarkGet>();
            }
            else if (opcao == 4)
            {
                var summary = BenchmarkRunner.Run<BenchmarkAtualizar>();
            }
            else
            {
                //outras opção para testes
                var b = new BenchmarkGet();
                b.RegistrosIniciais = 10;
                b.GlobalSetup();
                //Console.WriteLine(b.WCF_Post_Sem_Parametro_vazio());
                //Console.WriteLine(b.WCF_Post_Sem_Parametro());
                //Console.WriteLine(b.WCF_Post_Parametro());
                Console.WriteLine(b.Wcf_Get_Produto_All());
                Console.WriteLine(b.WebApi_Get_Produto());
            }
            if (opcao != 0)
            {
                Escolher();
            }
        }
    }
}

