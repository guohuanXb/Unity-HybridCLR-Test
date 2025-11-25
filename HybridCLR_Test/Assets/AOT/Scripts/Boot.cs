using Cysharp.Threading.Tasks;
using HybridCLR;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    private static string[] AOTMetaAssemblyNames =
    {
        "mscorlib.dll.bytes",
        "System.dll.bytes",
        "System.Core.dll.bytes",
        "Manager.dll.bytes",
        "UnityEngine.CoreModule.dll.bytes",
    };
    async void Start()
    {
        DontDestroyOnLoad(this);
        Assembly hotUpdateAss = null;
        try
        {



#if !UNITY_EDITOR
            //先加载AOT元数据，防止泛型报错
            await LoadMetadata();
            //加载热更新程序集
            var data = await DllLoad("HotUpdate.dll.bytes");
            await UniTask.SwitchToMainThread();
            hotUpdateAss = Assembly.Load(data);
#else
            hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotUpdate");
#endif
            Debug.Log("Dll加载成功");
        }
        catch(Exception e)
        {
            Debug.LogError($"发生错误：{e.Message}");
        }

        //注册所有工厂
        if (hotUpdateAss != null)
        {
            Type type = hotUpdateAss.GetType("HotUpdate.GameLoader");
            if (type != null)
            {
                type.GetMethod("Init")?.Invoke(null, null);
            }
            else
            {
                Debug.Log("type == null");
            }

        }
        else
        {
            Debug.Log("hotUpdateAss == null");
        }
            //加载场景
            await SceneManager.LoadSceneAsync("Scenes/SampleScene");
    }

    /// <summary>
    /// 异步读取dll
    /// </summary>
    /// <returns></returns>
    private async UniTask<byte[]> DllLoad(string dllName)
    {
        var data = await File.ReadAllBytesAsync($"{Application.streamingAssetsPath}/{dllName}");
        return data;
    }
    /// <summary>
    /// 异步加载元数据
    /// </summary>
    /// <returns></returns>
    private async UniTask LoadMetadata()
    {
        HomologousImageMode mode = HomologousImageMode.SuperSet;
        foreach (var aotDllName in AOTMetaAssemblyNames)
        {
            byte[] dllData = await DllLoad(aotDllName);
            LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllData, mode);
            Debug.Log($"加载元数据: {aotDllName}. Result: {err}");
        }
    }
}
