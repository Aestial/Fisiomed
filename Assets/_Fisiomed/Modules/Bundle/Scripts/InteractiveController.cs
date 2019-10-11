using UnityEngine;
using Loader;

public class InteractiveController : Singleton<InteractiveController>
{
    [SerializeField] Transform parent;
    public void Play(string url)
    {
        StartCoroutine(Downloader.Instance.LoadAsset(url, OnDownloadCompleted));
    }
    void OnDownloadCompleted(GameObject prefab)
    {
        GameObject currentGO = Instantiate(prefab, parent);
    }
}
