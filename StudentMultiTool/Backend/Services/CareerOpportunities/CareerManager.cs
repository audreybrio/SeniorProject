
using Newtonsoft.Json.Linq;
using StudentMultiTool.Backend.Models.CareerOpportunities;
using System.Net;
using System.Text.Json;

namespace StudentMultiTool.Backend.Services.CareerOpportunities
{
    public class CareerManager
    {
        public CareerManager() { }
        public async Task<Opportunities> getResults()
        {
            string jsonData = "";
            string URL = "https://data.usajobs.gov/api/search?Keyword=Software";
            #pragma warning disable SYSLIB0014 // Type or member is obsolete
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            #pragma warning restore SYSLIB0014 // Type or member is obsolete
            request.Method = "GET";
            request.UserAgent = "Albert.Toscano01@student.csulb.edu";
            string autrorizationKey = "EuaMSTkwGOXJalPclJ2Y0ery3a5+NSycD7Ov/6jcJCc=";
            request.Headers.Add("Authorization-Key", autrorizationKey);
            try
            {
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            jsonData = reader.ReadToEnd();
                            reader.Close();
                        }
                    }
                }
                var opportunity = Opportunities.FromJson(jsonData);
                return opportunity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var opportunity = Opportunities.FromJson(jsonData);
                return opportunity;
            }
        }
    }
}
