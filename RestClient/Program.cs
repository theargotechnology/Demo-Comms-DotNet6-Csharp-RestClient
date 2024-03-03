/* This sample program shows basic rest request and response read */

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
    }
    catch (Exception ex)
    {
        // If got problem with the rest call show here
        Console.WriteLine("Error: " + ex.Message);
    }
}

Console.WriteLine("UNIT TEST : Stop");