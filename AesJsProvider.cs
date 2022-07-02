using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MainUi.Shared.EncryptProviders
{
    public class AesJsProvider : IEncryptProvider
    {
        IJSRuntime GetJSRuntime;
        public static int[] HiddenKey = new int[] { 0, 45, 6, 3, 8, 5, 8, 7, 89, 7, 10, 21, 12, 34, 12, 1 };
        public static string JsEncryptMethod { get; set; } = "encryptText";
        public static string JsDecryptMethod { get; set; } = "decryptText";

        public AesJsProvider(IJSRuntime jSRuntime)
        {
            GetJSRuntime = jSRuntime;
        }

        public async Task<string> TextDecrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            try
            {
                return await GetJSRuntime.InvokeAsync<string>(JsEncryptMethod, input, HiddenKey);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> TextEncrypt(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            try
            {
                return await GetJSRuntime.InvokeAsync<string>(JsDecryptMethod, input, HiddenKey);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> Encrypt<T>(T input)
        {
            var str = JsonSerializer.Serialize(input);
            if (input == null)
                return string.Empty;
            try
            {
                return await GetJSRuntime.InvokeAsync<string>(JsEncryptMethod, input, HiddenKey);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<T> Decrypt<T>(string input)
        {
            var str = await TextDecrypt(input);
            if (string.IsNullOrEmpty(input))
                return default(T);
            try
            {
                return await GetJSRuntime.InvokeAsync<T>(JsDecryptMethod, input, HiddenKey);
            }
            catch (Exception)
            {
                return default;

            }
        }
    }

}
