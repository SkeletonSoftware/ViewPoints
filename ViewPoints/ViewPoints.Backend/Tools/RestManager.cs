using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ViewPoints.Backend.Tools
{
    class RestManager<T> where T : class
    {
        private const string ContentType = "application/json";
        private static HttpClient httpClient;

        /// <summary>
        /// Adresa webové služby
        /// </summary>
        public string RestServiceUrl { get; private set; }

        /// <summary>
        /// Konstruktor třídy umožňující pracovat s REST webovými službami. Odesílá data metodou POST. 
        /// </summary>
        /// <param name="restServiceUrl"></param>
        public RestManager(string restServiceUrl)
        {
            if (!string.IsNullOrEmpty(restServiceUrl))
            {
                if (restServiceUrl[restServiceUrl.Length - 1] != '/')
                    throw new ArgumentException("At the end of the address must be the character \'/\'");
            }
            RestServiceUrl = restServiceUrl;
            if (httpClient == null)
                httpClient = new HttpClient(new ModernHttpClient.NativeMessageHandler());
        }

        /// <summary>
        /// Odešle dotaz na zadnou webovou službu
        /// </summary>
        /// <param name="restServiceURL">Úplná adresa webové služby</param>
        /// <param name="methodUrl">Název funkce která má být zavolána</param>
        /// <param name="parameters">Data která mají být odeslána</param>
        /// <param name="token">Cancelation token pro případ že je potřeba síťovou komunikaci ukončit</param>
        /// <param name="withoutOutput">Určuje zda očekáváme od WS nějaký výstup</param>
        /// <returns></returns>
        public async Task<T> SendGetRequest(string methodUrl, string parameters, bool withoutOutput = false)
        {
            string url = $"{this.RestServiceUrl}{methodUrl}";
            string output = null;
            if (!string.IsNullOrEmpty(parameters))
                url += parameters;
            HttpResponseMessage response = null;
            try
            {
                response = await httpClient.GetAsync(url);
                output = await response.Content.ReadAsStringAsync();
            }
            finally
            {
                if (response != null)
                    response.Dispose();
            }
            return this.GetResult(output, withoutOutput);
        }

        /// <summary>
        /// Odešle dotaz na zadanou webovou službu
        /// </summary>
        /// <param name="methodUrl">Název funkce která má být zavolána</param>
        /// <param name="data">Data která mají být odeslána</param>
        /// <param name="withoutOutput">Určuje zda očekáváme od WS nějaký výstup</param>
        /// <returns></returns>
        public async Task<T> SendPostRequest(string methodUrl, object data, bool withoutOutput = false)
        {
            string textData = JsonConvert.SerializeObject(data);
            return await this.SendPostRequest(methodUrl, textData, withoutOutput);
        }

        /// <summary>
        /// Odešle dotaz na zadnou webovou službu
        /// </summary>
        /// <param name="restServiceURL">Úplná adresa webové služby</param>
        /// <param name="methodUrl">Název funkce která má být zavolána</param>
        /// <param name="data">Data která mají být odeslána</param>
        /// <param name="token">Cancelation token pro případ že je potřeba síťovou komunikaci ukončit</param>
        /// <param name="encoding">Kódování zadaných dat, pokud není uvedeno použíje se UTF-8</param>
        /// <param name="withoutOutput">Určuje zda očekáváme od WS nějaký výstup</param>
        /// <returns></returns>
        private async Task<T> SendPostRequest(string methodUrl, string data, bool withoutOutput = false)
        {
            string output = null;
            HttpContent content = new StringContent(data, Encoding.UTF8, ContentType);
            string url = $"{this.RestServiceUrl}{methodUrl}";
            HttpResponseMessage response = null;
            try
            {
                response = await httpClient.PostAsync(url, content);
                output = await response.Content.ReadAsStringAsync();
            }
            finally
            {
                if (response != null)
                    response.Dispose();
            }

            return this.GetResult(output, withoutOutput);
        }

        private T GetResult(string output, bool withoutOutput)
        {
            if (!withoutOutput)
            {
                var result = JsonConvert.DeserializeObject<T>(output);
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
