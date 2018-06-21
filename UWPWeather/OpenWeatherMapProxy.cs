using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace UWPWeather
{
    public class OpenWeatherMapProxy
    {

        /// <summary>
        /// Since we are making a network request in the function we need the async keyword
        /// </summary>
        /// <param name="lat">The latitude parameter</param>
        /// <param name="lon">The longitude parameter</param>
        /// <returns>Using the task RootObject we are promising that when the task completes if it
        /// completes it will return a root object</returns>
        public async static Task<RootObject> GetWeather(double lat, double lon)
        {
            //Create a httpclient for networkManagement
            //Download it using NugetManager
            var http = new HttpClient();

            //this will wait for the async task and then store the response
            var response = await http.GetAsync("http://api.openweathermap.org/data/2.5/weather?zip=421301,in&units=metric&appid=4fac3a969505b222febe923d04917a88");

            //Now it will start reading the response
            var result = await response.Content.ReadAsStringAsync();

            //Then we will use a serializer to make C# object which is readily useable
            //from the JSON response
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            //We create a memory stream to manage the varying speed of getting data and 
            //serializing it
            var mMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result));

            //finally we serialize the data with the help of the memory stream and cast it
            // to a RootObject
            var data = (RootObject)serializer.ReadObject(mMemoryStream);


            return data;
        }
    }

    [DataContract]
    public class Coord
    {
        [DataMember]
        public double lon { get; set; }

        [DataMember]
        public double lat { get; set; }
    }


    [DataContract]
    public class Weather
    {

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string main { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string icon { get; set; }
    }


    [DataContract]
    public class Main
    {

        [DataMember]
        public double temp { get; set; }

        [DataMember]
        public double pressure { get; set; }

        [DataMember]
        public int humidity { get; set; }

        [DataMember]
        public double temp_min { get; set; }

        [DataMember]
        public double temp_max { get; set; }

        [DataMember]
        public double sea_level { get; set; }

        [DataMember]
        public double grnd_level { get; set; }
    }


    [DataContract]
    public class Wind
    {
        [DataMember]
        public double speed { get; set; }

        [DataMember]
        public int deg { get; set; }
    }


    [DataContract]
    public class Clouds
    {
        [DataMember]
        public int all { get; set; }
    }


    [DataContract]
    public class Sys
    {
        [DataMember]
        public double message { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public int sunrise { get; set; }

        [DataMember]
        public int sunset { get; set; }
    }


    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }

        [DataMember]
        public List<Weather> weather { get; set; }

        [DataMember]
        public string @base { get; set; }

        [DataMember]
        public Main main { get; set; }

        [DataMember]
        public Wind wind { get; set; }

        [DataMember]
        public Clouds clouds { get; set; }

        [DataMember]
        public int dt { get; set; }

        [DataMember]
        public Sys sys { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int cod { get; set; }
    }
}
