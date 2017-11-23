using EndUser.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EndUser.APIParser
{
    public static class HttpRequestParser
    {
        private async static Task<HttpResponseMessage> PerformHttpGetRequest(string path)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                    return response;
                else
                    return null;
            }
        }

        public async static Task<double> GetId(string measurementPath)
        {
            var url = string.Format("{0}/{1}/Id", APIValues.ApiAddress, measurementPath);
            double result = 0;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<double>(content);

                    
                }
            }

            return result;
        }

        public async static Task<double[]> GetMeasurement(string measurementPath)
        {
            var url = string.Format("{0}/{1}", APIValues.ApiAddress, measurementPath);
            double[] result = { };

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<double[]>(content);


                }
            }

            return result;
        }

        public async static Task<IList<double[]>> GetListedMeasurement(string measurementPath)
        {
            var url = string.Format("{0}/{1}", APIValues.ApiAddress, measurementPath);
            IList<double[]> result = new List<double[]>();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<IList<double[]>>(content);
                }
            }

            return result;
        }
    }
}
