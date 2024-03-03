/* 
 * Author: Francis Vergel Altura Japson
 * 
 * This sample program shows basic rest request and response read 
 */

using Newtonsoft.Json;
using System.Security.Principal;

namespace CSharpRainStation
{
    public class StationInformation
    {
        public string? label { get; set; }
        public string? lat { get; set; }
        public string? stationReference { get; set; }
    }

    public class StationList
    {
        public StationInformation[]? items;

        public int Length { get; internal set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            TriggerRestCall();
        }

        public static void TransformResponse(string response)
        {
            StationList member = JsonConvert.DeserializeObject<StationList>(response);

            if (member == null)
            {
                Console.WriteLine("Problem with json response");
            }
            else
            {
                if (member.items.Length == 0)
                {
                    Console.WriteLine("No Stations available");
                }
                else
                {
                    // Print all stations with some info
                    Console.WriteLine("UNIT TEST - Start printing all stations with some info");
                    for (int i = 0; i < member.items.Length; i++)
                    {
                        Console.WriteLine("##");
                        Console.WriteLine(member.items[i].label);
                        Console.WriteLine(member.items[i].lat);
                        Console.WriteLine(member.items[i].stationReference);
                        Console.WriteLine("##");
                    }
                    Console.WriteLine("UNIT TEST - Stop printing all stations with some info");
                }
            }
        }

        private static void TriggerRestCall()
        {
            Console.WriteLine("UNIT TEST : Start");

            using (var client = new HttpClient())
            {
                try
                {
                    // Trigger the GET request to call an API endpoint
                    var response = client.GetAsync("https://environment.data.gov.uk/flood-monitoring/id/stations?parameter=rainfall&_limit=50").Result;

                    // Read the response as a stream
                    var responseStream = response.Content.ReadAsStreamAsync().Result;

                    // Convert the stream to string
                    StreamReader reader = new StreamReader(responseStream);

                    // Transformer
                    string text = reader.ReadToEnd();

                    // Print out the JSON Response
                    Console.WriteLine("Response: " + text);

                    // Create an object to carry the information and drool on it if neede by other apps
                    TransformResponse(text);
                }
                catch (Exception ex)
                {
                    // If got problem with the rest call show here
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Console.WriteLine("UNIT TEST : Stop");
        }
    }
}
