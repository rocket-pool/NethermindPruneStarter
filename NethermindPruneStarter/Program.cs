using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace NethermindPruneStarter
{
    /// <summary>
    /// This is a simple program that accesses Nethermind's `admin_prune` JSON-RPC route to trigger pruning.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid arguments. One argument is expected - the URL of Nethermind's admin JSON-RPC API.");
                return;
            }

            Uri uri = new(args[0]);
            using HttpClient client = new();

            int retryTime = 3;
            int retryCount = 10;
            for (int i = 0; i < retryCount; i++)
            {
                // Generate the request payload
                Request request = new(i + 1, "admin_prune", Array.Empty<object>());
                string serializedRequest = JsonConvert.SerializeObject(request);
                byte[] requestBytes = Encoding.UTF8.GetBytes(serializedRequest);
                ByteArrayContent content = new(requestBytes);
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                // Send the request
                HttpResponseMessage? responseMessage = null;
                try
                {
                    responseMessage = client.PostAsync(uri, content).Result;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error requesting prune: {ex.GetType().Name} - {ex.Message}");
                }

                // Process the response
                if (responseMessage != null && responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = responseMessage.Content.ReadAsStringAsync().Result;
                    try
                    {
                        // Try deserializing the response
                        Response? response = JsonConvert.DeserializeObject<Response>(responseBody);
                        if (response != null)
                        {
                            // Try again if it errored out
                            if (response.ErrorCode != null)
                            {
                                Console.WriteLine($"Error starting prune: code {response.ErrorCode}, data = {response.Data}");
                            }
                            else
                            {
                                // No error code, so this was a success!
                                Console.WriteLine($"Success: Pruning is now \"{response.Result}\"");
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Error deserializing response JSON: response resulted in a null value.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deserializing response JSON: {ex.GetType().Name} - {ex.Message}");
                    }
                }

                Console.WriteLine($"Trying again in {retryTime} seconds... ({i + 1}/{retryCount}");
                Thread.Sleep(retryTime * 1000);
            }

            Console.WriteLine($"Failed starting prune after {retryCount} attempts. Please try again later.");
        }
    }

}
