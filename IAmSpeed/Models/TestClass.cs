using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{
    public class TestClass
    {
        public static async Task<Rootobject> LoadData (string gameName)
        {
            //string[] splitGameName = gameName.Split(" ");
            //var percent = ""; 
            //foreach(var sgn in splitGameName)
            //{
            //    percent += sgn + "%20"; 
            //}
            string url = "";
             url = $"https://www.speedrun.com/api/v1/games?name={gameName}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Rootobject rootobject = await response.Content.ReadAsAsync<Rootobject>();

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
