using ElectronicShop_Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ElectronicShop_ApiIntegration
{
	public class BaseApiClient
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		// Provides access to the current HttpContext, if one is available.
		private readonly IHttpContextAccessor _httpContextAccessor;

		protected BaseApiClient(IHttpClientFactory httpClientFactory,
				   IHttpContextAccessor httpContextAccessor,
					IConfiguration configuration)
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<TResponse> GetAsync<TResponse>(string url, bool requiredLogin = false)
		{
			var token = _httpContextAccessor
				.HttpContext
				.Request
				.Cookies[Constants.TOKEN];

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

			if (requiredLogin)
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, token);
			}

			var response = await client.GetAsync(url);

			var body = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode)
			{
				TResponse myDeserializeObjList = (TResponse)JsonConvert.DeserializeObject(body, typeof(TResponse));
				return myDeserializeObjList;
			}
			return JsonConvert.DeserializeObject<TResponse>(body);
		}

		public async Task<List<T>> GetListAsync<T>(string url, bool requiredLogin = false)
		{
			var token = _httpContextAccessor
				.HttpContext
				.Request
				.Cookies[Constants.TOKEN];

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

			if (requiredLogin)
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, token);
			}

			var response = await client.GetAsync(url);
			var body = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode)
			{
				var data = (List<T>)JsonConvert.DeserializeObject(body, typeof(List<T>));
				return data;
			}
			throw new Exception(body);
		}

		// Cần đăng nhập mới dùng được nên bỏ tham số bool requiredLogin = false
		public async Task<bool> Delete(string url)
		{
			// Lấy ra token
			var token = _httpContextAccessor
				.HttpContext
				.Request
				.Cookies[Constants.TOKEN];

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, token);

			var response = await client.DeleteAsync(url);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Put(string url, HttpContent content)
		{
			var token = _httpContextAccessor
				.HttpContext
				.Request
				.Cookies[Constants.TOKEN];

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, token);

			var response = await client.PutAsync(url, content);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Post(string url, HttpContent content)
		{
			var token = _httpContextAccessor
				.HttpContext
				.Request
				.Cookies[Constants.TOKEN];

			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, token);

			var response = await client.PostAsync(url, content);

			return response.IsSuccessStatusCode;
		}
	}
}
