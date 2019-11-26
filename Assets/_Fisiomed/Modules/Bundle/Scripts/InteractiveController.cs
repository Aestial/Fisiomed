using UnityEngine;
using Loader;

public class InteractiveController : Singleton<InteractiveController>
{
    [SerializeField] Transform parent;
    public void Play(string url)
    {
        AppManager.Instance.ShowLoader(true);
        StartCoroutine(Downloader.Instance.LoadAsset(url, OnDownloadCompleted));
    }
    void OnDownloadCompleted(GameObject prefab)
    {
        GameObject currentGO = Instantiate(prefab, parent);
        AppManager.Instance.ShowLoader(false);
    }
}
