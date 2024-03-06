using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using idojaras;


namespace idojaras
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            List<Ido> ido = new List<Ido>();
            Console.WriteLine("Add meg a város nevét!");
            string varos = Console.ReadLine();
            ido = await IdoAdat(varos);
            Console.WriteLine($"A város: {ido[0].Data.City} \n Az idő: {ido[0].Data.Temp}°C \n A szél: {ido[0].Data.Wind} \n uv index: {ido[0].Data.UvIndex} \n páratartalom: {ido[0].Data.Humidity}\n utoljára frissitve: {ido[0].Data.LastUpdate}");
            Console.ReadLine();

        }

        private static async Task<List<Ido>> IdoAdat(string varos)
        {
            List<Ido> ido = new List<Ido>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://the-weather-api.p.rapidapi.com/api/weather/{varos}"),
                Headers =
    {
        { "X-RapidAPI-Key", "2aafff4238msh42a8819a4d67bb9p1129c3jsne6571485665a" },
        { "X-RapidAPI-Host", "the-weather-api.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    ido.Add(Ido.FromJson(jsonString));
                }
            }

                return ido;
        }
    }
}
