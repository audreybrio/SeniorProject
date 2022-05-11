
using Newtonsoft.Json.Linq;
using StudentMultiTool.Backend.Models.CareerOpportunities;
using System.Net;
using System.Text.Json;

namespace StudentMultiTool.Backend.Services.CareerOpportunities
{
    public class CareerManager
    {
        public CareerManager() { }

        // getResults method creates a GET request to USAJobs and 
        // returns an Opportunities object
        public async Task<Opportunities> getResults(string keywords)
        {
            string jsonData = "";
            keywords = fixString(keywords);
            string URL = "https://data.usajobs.gov/api/search?Keyword=" + keywords;
            // to make a Http web request to USAJobs
            #pragma warning disable SYSLIB0014 // Type or member is obsolete
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            #pragma warning restore SYSLIB0014 // Type or member is obsolete
            request.Method = "GET";
            // Credentials to validate Http web request
            request.UserAgent = "Albert.Toscano01@student.csulb.edu";
            string autrorizationKey = "EuaMSTkwGOXJalPclJ2Y0ery3a5+NSycD7Ov/6jcJCc=";
            request.Headers.Add("Authorization-Key", autrorizationKey);
            try
            {
                // awaits until it gets a response from USAJobs
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        // read data until the USAJobs API finishes sending the response
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            jsonData = reader.ReadToEnd();
                            reader.Close();
                        }
                    }
                }
                // It serializes the jsonData string to Json format and place it in an Opportinities object
                var opportunity = Opportunities.FromJson(jsonData);
                // it returns the Opportinities object
                return opportunity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // In case of an exception, it returns an Opportunities object with no data
                var opportunity = Opportunities.FromJson(jsonData);
                return opportunity;
            }
        }

        // The fixString method replaces the space character with %20
        // to concatenate the keywords string with the URL
        public string fixString(string keywords)
        {
            return keywords.Replace(" ", "%20");
        }
    }
}
