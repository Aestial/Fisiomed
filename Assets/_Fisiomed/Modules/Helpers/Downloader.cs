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
                    Debug.Log(uri + ": Error: " + webRequest.error);
                }
                else
                {
                    string text = webRequest.downloadHandler.text;
                    Debug.Log(uri + ":\nReceived: " + text);
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
                    Debug.Log(uri + ": Error: " + webRequest.error);
                }
                else
                {
                    Texture texture = ((DownloadHandlerTexture) webRequest.downloadHandler).texture;
                    Debug.Log(uri + ":\nReceived: " + texture);
                    // Callback function
                    onCompleted(texture);
                }
            }
        }

        public IEnumerator LoadAsset(string uri, Action<GameObject> onCompleted)
        {
            Debug.Log("Downloader - Loading Asset");
            using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                // Fail
                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log("Downloader - Error: " + webRequest.error + " from: " + uri);
                }
                // Success
                else
                {
                    // Load and retreive the AssetBundle
                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
                    Debug.Log("Downloader - Received: " + bundle + " from: " + uri);

                    // Load the object asynchronously
                    AssetBundleRequest request = bundle.LoadAssetAsync(bundle.GetAllAssetNames()[0], typeof(GameObject));
                    yield return request;

                    // Load the object synchronously
                    //GameObject prefab = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                    //yield return prefab;

                    GameObject prefab = request.asset as GameObject;

                    Debug.Log("Downloader - Loaded: " + prefab);

                    bundle.Unload(false);
                    // Callback function
                    onCompleted(prefab);
                   
                }
                webRequest.Dispose();
            }
        }

        public IEnumerator LoadVideo(string uri, string filename, Action<string> onCompleted)
        {
            Debug.Log("Downloader - Loading Video");
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log("Downloader - Error: " + webRequest.error + " from: " + uri);
                }
                else
                {
                    string filePath = Path.Combine(Application.persistentDataPath, filename);

                    File.WriteAllBytes(filePath, webRequest.downloadHandler.data);
                    Debug.Log("VideoDonload - Video saved: " + filePath);

                    // Callback function
                    onCompleted(filePath);

                }
                webRequest.Dispose();
            }
        }
    }
}
