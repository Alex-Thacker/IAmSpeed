using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{
    public class TestClass
    {
        //public string url { get; set;  }
        public static async Task<ListGameBase> LoadData (string url)
        {
            //string[] splitGameName = gameName.Split(" ");
            //var percent = ""; 
            //foreach(var sgn in splitGameName)
            //{
            //    percent += sgn + "%20"; 
            //}
            //string url = "";

            //url = $"https://www.speedrun.com/api/v1/games?name={gameName}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ListGameBase rootobject = await response.Content.ReadAsAsync<ListGameBase>();

                    return rootobject; 
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<SingleGameBase> LoadSingleData(string url)
        {
            //string[] splitGameName = gameName.Split(" ");
            //var percent = ""; 
            //foreach(var sgn in splitGameName)
            //{
            //    percent += sgn + "%20"; 
            //}
            //string url = "";

            //url = $"https://www.speedrun.com/api/v1/games?name={gameName}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    SingleGameBase rootobject = await response.Content.ReadAsAsync<SingleGameBase>();

                    return rootobject;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
