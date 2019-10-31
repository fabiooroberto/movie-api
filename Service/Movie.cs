using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class Movie
    {
        private string _ApiMovieUrl = Util.Constantes.API_MOVIE_URL;
        private string _ApiMoveiKey = Util.Constantes.API_MOVIE_KEY;
        private string _ApiMoveiPath = Util.Constantes.API_MOVIE_PATHIMAGE;
        private string _language = "pt-BR";

        public async Task<Models.MovieUpComing> ProximosLancamentos(int page)
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                var url = string.Concat(_ApiMovieUrl, "movie/upcoming?api_key=", _ApiMoveiKey, "&language=", _language, "&page=", page);
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);

                var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var retorno = new JsonSerializer().Deserialize<Models.Rootobject>(response);
                    var generos = await GetGenresAsync();
                    var dados = new Models.MovieUpComing()
                    {
                        Datas = retorno.Dates,
                        Lista = Models.ProximosLancamentos.ToList(retorno.Results, _ApiMoveiPath, generos),
                        Pagina = retorno.Page,
                        Total_Paginas = retorno.Total_pages,
                        Total_Resultados = retorno.Total_results
                    };

                    return dados;
                }
                else
                {
                    var retorno = new JsonSerializer().Deserialize<Models.Rootobject>(response);

                    var dados = new Models.GetErros(retorno.Errors);

                    throw new Exception(string.Concat("Falha ao recuperar os dados. Erros: ", dados.Erro));
                }
            }
        }

        public async Task<List<Models.Genre>> GetGenresAsync()
        {
            using (var cancellationTokenSource = new CancellationTokenSource())
            {
                var url = string.Concat(_ApiMovieUrl, "genre/movie/list?api_key=", _ApiMoveiKey, "&language=", _language);
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);

                var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var retorno = new JsonSerializer().Deserialize<Models.Genreobject>(response);
                    return retorno.Genres;
                }
                else
                {
                    var retorno = new JsonSerializer().Deserialize<Models.Rootobject>(response);

                    var dados = new Models.GetErros(retorno.Errors);

                    throw new Exception(string.Concat("Falha ao recuperar os dados. Erros: ", dados.Erro));
                }
            }
        }
    }
}