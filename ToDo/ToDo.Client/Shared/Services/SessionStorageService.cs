using Microsoft.JSInterop;
using System.Text.Json;

namespace ToDo.Client.Shared.Services
{
    public class SessionStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public SessionStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string?> GetValueAsync(string key)
        {
            return await _jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", key);
        }

        public async Task<T?> GetValueAsync<T>(string key)
        {
            var value = await GetValueAsync(key);

            if (value is null)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetValueAsync(string key, string? value)
        {
            await _jsRuntime.InvokeAsync<string?>("sessionStorage.setItem", key, value);
        }

        public async Task SetValueAsync<T>(string key, T? value)
        {
            var json = JsonSerializer.Serialize(value);
            await SetValueAsync(key, json);
        }

        public async Task RemoveKeyAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", key);
        }
    }
}
