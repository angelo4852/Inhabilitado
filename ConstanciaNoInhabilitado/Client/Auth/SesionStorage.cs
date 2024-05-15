using Blazored.SessionStorage;
using ConstanciaNoInhabilitado.Shared.Entities;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ConstanciaNoInhabilitado.Client.Auth
{
    public class SesionStorage
    {
        public async Task GuardarSesionStorage<T>(ISessionStorageService sessionStorageService, string key, T item) where T:class 
        {
            var itemJson = JsonSerializer.Serialize(item); 
            await sessionStorageService.SetItemAsStringAsync(key, itemJson);
        }

        public async Task<T?> ObtenerSesionStorage<T>(ISessionStorageService sessionStorageService, string key) where T : class
        {
            var itemJson = await sessionStorageService.GetItemAsStringAsync(key);

            if (itemJson != null)
            {
                var item = JsonSerializer.Deserialize<T>(itemJson);
                return item;
            }
            else
            {
                return null; 
            }
        }
    }
}
