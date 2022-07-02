using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace MainUi.Shared
{
    public interface IEncryptProvider
    {
        Task<string> TextEncrypt(string input);
        Task<string> TextDecrypt(string input);

        Task<string> Encrypt<T>(T input);
        Task<T> Decrypt<T>(string input);
    }
}
