using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagementService
{
    public class DoorManagementService
    {
        private readonly string _url = "https://localhost:12443/api/door";

        #region Get

        public async Task<HttpResponseMessage> GetAsync()
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync($"{_url}").ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string doorId)
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync($"{_url}/{doorId}").ConfigureAwait(false);
            }
        }

        #endregion

        #region Post

        public async Task<HttpResponseMessage> PostAsync(string body)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                return await client.PostAsync(_url, content).ConfigureAwait(false);
            }
        }

        #endregion

        #region Put

        public async Task<HttpResponseMessage> PutAsync(string body)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                return await client.PutAsync(_url, content).ConfigureAwait(false);
            }
        }

        #endregion

        #region Delete

        public async Task<HttpResponseMessage> DeleteAsync(string doorId)
        {
            using (var client = new HttpClient())
            {
                return await client.DeleteAsync($"{_url}/{doorId}").ConfigureAwait(false);
            }
        }

        #endregion
    }
}
