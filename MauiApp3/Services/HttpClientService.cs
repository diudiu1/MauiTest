﻿using MauiApp3.Services.AccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp3.Services
{
    public class HttpClientService
    {
        private readonly Lazy<HttpClient> _httpClient = new Lazy<HttpClient>(() =>
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        },
        LazyThreadSafetyMode.ExecutionAndPublication);
        private static JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

        };
        private HttpClient CreateHttpClient()
        {
            var httpClient = _httpClient.Value;

            if (IAccountService.CurrentAccount!=null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IAccountService.CurrentAccount.Token);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
            }

            return httpClient;
        }
        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var httpClient = CreateHttpClient();
            var resp = await httpClient.GetAsync(url);
            return await resp.Content.ReadFromJsonAsync<TResponse>();
        }
        public async Task<TResponse> GetAsync<TRequest, TResponse>(string url, TRequest request)
        {
            url = UrlParms<TRequest>(url, request);
            var httpClient = CreateHttpClient();
            var resp = await httpClient.GetAsync(url);
            return await FromJsonAsync<TResponse>(resp);
        }
        public async Task<TResponse> PostAsync<TRequest,TResponse>(string url,TRequest request)
        {
            var httpClient = CreateHttpClient();
            var model = new StringContent(JsonSerializer.Serialize(request));
            model.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var resp = await httpClient.PostAsync(url, model);
            return await FromJsonAsync<TResponse>(resp);
        }
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest request)
        {
            var httpClient = CreateHttpClient();
            var model = new StringContent(JsonSerializer.Serialize(request));
            model.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var resp = await httpClient.PutAsync(url, model);
            return await FromJsonAsync<TResponse>(resp);
        }

        private async Task<TResponse> FromJsonAsync<TResponse>(HttpResponseMessage responseMessage)
        {
            return await responseMessage.Content.ReadFromJsonAsync<TResponse>(options);
        }
        private string UrlParms<TRequest>(string url, TRequest request)
        { 
            Type type=typeof(TRequest);
            var props = type.GetProperties();
            if (props.Length>0 && url.IndexOf("?")==-1)
            {
                url += "?";
            }
            foreach (var item in props)
            {
                var value = item.GetValue(request);
                if (value!=null)
                {
                    url += item.Name + "=" + value + "&";
                }
            }
            return url.TrimEnd('&');
        }
    }
}