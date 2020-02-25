using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Loader
{
    public class Downloader : Singleton<Downloader>
    {
        public IEnumerator LoadJSON(string uri, Action<string> onCompleted)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Log.Color(uri + ": Error: " + webRequest.error, this);
                }
                else
                {
                    string text = webRequest.downloadHandler.text;
                    Log.Color(uri + ":\nReceived: " + text, this);
                    // Callback function
                    onCompleted(text);
                }
            }
        }

        public IEnumerator LoadTexture(string uri, Action<Texture> onCompleted)
        {
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Log.Color(uri + ": Error: " + webRequest.error, this);
                }
                else
                {
                    Texture texture = ((DownloadHandlerTexture) webRequest.downloadHandler).texture;
                    Log.Color(uri + ":\nReceived: " + texture, this);
                    // Callback function
                    onCompleted(texture);
                }
            }
        }

        public IEnumerator LoadAsset(string uri, Action<GameObject> onCompleted)
        {
            Log.Color("Downloader - Loading Asset", this);
            using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                // Fail
                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Log.Color("Downloader - Error: " + webRequest.error + " from: " + uri, this);
                }
                // Success
                else
                {
                    // Load and retreive the AssetBundle
                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
                    Log.Color("Downloader - Received: " + bundle + " from: " + uri, this);

                    // Load the object asynchronously
                    AssetBundleRequest request = bundle.LoadAssetAsync(bundle.GetAllAssetNames()[0], typeof(GameObject));
                    yield return request;

                    // Load the object synchronously
                    //GameObject prefab = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                    //yield return prefab;

                    GameObject prefab = request.asset as GameObject;

                    Log.Color("Downloader - Loaded: " + prefab, this);

                    bundle.Unload(false);
                    // Callback function
                    onCompleted(prefab);
                   
                }
                webRequest.Dispose();
            }
        }

        public IEnumerator LoadVideo(string uri, string filename, Action<string> onCompleted)
        {
            string path = Path.Combine(Application.persistentDataPath, "videos");
            if (!Directory.Exists(path))    Directory.CreateDirectory(path);
            string filePath = Path.Combine(path, filename);
            if (File.Exists(filePath))
            {
                Log.Color("Downloader - Video exists in path: " + filePath, this);
                // Callback function
                onCompleted(filePath);
                yield return null;
            }
            else
            {
                Log.Color("Downloader - Loading Video", this);
                using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
                {
                    // Request and wait for the desired page.
                    yield return webRequest.SendWebRequest();
                    if (webRequest.isNetworkError || webRequest.isHttpError)
                    {
                        Log.ColorError("Downloader - Error: " + webRequest.error + " from: " + uri, this);                        
                    }
                    else
                    {                        
                        File.WriteAllBytes(filePath, webRequest.downloadHandler.data);
                        Log.Color("Video Download - Video saved: " + filePath, this);                        
                        // Callback function
                        onCompleted(filePath);
                    }
                    webRequest.Dispose();
                }
            }
            
        }
    }
}
