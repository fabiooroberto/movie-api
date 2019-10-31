using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{

    public class Rootobject
    {
        public List<Upcoming> Results { get; set; }
        public int Page { get; set; }
        public int Total_results { get; set; }
        public Dates Dates { get; set; }
        public int Total_pages { get; set; }
        public List<string> Errors { get; set; }
    }

    public class GetErros
    {
        public string Erro { get; set; }
        public GetErros()
        {

        }
        public GetErros(List<string> erros)
        {
            if (erros != null)
            {

                foreach (var item in erros)
                {
                    Erro += string.Concat(item, " # ");
                }
            }
        }

    }

    public class Dates
    {
        public string Maximum { get; set; }
        public string Minimum { get; set; }
    }

    public class Upcoming
    {
        public float Popularity { get; set; }
        public int Id { get; set; }
        public bool Video { get; set; }
        public int Vote_count { get; set; }
        public float Vote_average { get; set; }
        public string Title { get; set; }
        public string Release_date { get; set; }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public List<int> Genre_ids { get; set; }
        public string Backdrop_path { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string Poster_path { get; set; }
    }

    public class MovieUpComing
    {
        public int Pagina { get; set; }
        public int Total_Resultados { get; set; }
        public Dates Datas { get; set; }
        public int Total_Paginas { get; set; }
        public List<ProximosLancamentos> Lista { get; set; }
    }

    public class ProximosLancamentos
    {
        public string Nome { get; set; }
        public string NomeOriginal { get; set; }
        public List<string> Genero { get; set; }
        public string DataLancamento { get; set; }
        public string Thumb { get; set; }
        public string Poster { get; set; }
        public ProximosLancamentos()
        {

        }

        public ProximosLancamentos(Upcoming upcoming, string path, List<Genre> generos)
        {
            Nome = upcoming.Title;
            NomeOriginal = upcoming.Original_title;
            DataLancamento = upcoming.Release_date;
            Thumb = string.IsNullOrEmpty(upcoming.Backdrop_path) ? "" : string.Concat(path, upcoming.Backdrop_path);
            Poster = string.IsNullOrEmpty(upcoming.Poster_path) ? "" : string.Concat(path, upcoming.Poster_path);
            Genero = generos.Where(x => upcoming.Genre_ids.Contains(x.Id)).Select(x => x.Name).ToList();
        }

        public static List<ProximosLancamentos> ToList(List<Upcoming> upcomings, string path, List<Genre> generos)
        {
            var lista = new List<ProximosLancamentos>();

            if (upcomings != null)
            {
                foreach (var item in upcomings)
                {
                    lista.Add(new ProximosLancamentos(item, path, generos));
                }
            }

            return lista;
        }
    }


    public class Genreobject
    {
        public List<Genre> Genres { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
