using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        void CleanUp();
        void Initialize();
        Task<GameObject>Instantiate(string path);
        Task<GameObject> Instantiate(string path, Vector3 at);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string address) where T : class;
    }
}