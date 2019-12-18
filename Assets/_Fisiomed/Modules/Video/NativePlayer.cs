using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NativePlayer : MonoBehaviour 
{
    [SerializeField] GameObject loader;
    string m_pathToFile;

      void OnApplicationPause (bool isPaused) 
    {
        if (!isPaused)
        {
            Screen.orientation = ScreenOrientation.Portrait;
            StartCoroutine(DisableSafeCanvasCoroutine());
        }
	}

    IEnumerator DisableSafeCanvasCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        AppManager.Instance.ShowLoader(false);
    }

    public void DownloadAndPlay(string url)
    {
        StartCoroutine(DownloadAndPlayCoroutine(url));
    }

    IEnumerator DownloadAndPlayCoroutine(string url)
    {
        AppManager.Instance.ShowLoader(true);

        string path = Path.Combine(Application.persistentDataPath, "Videos");
        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        string videoName = "DownloadedVideo.mp4";
        m_pathToFile = Path.Combine(path, videoName);


        Debug.Log("Downloading video: " + url);
        var www = new WWW(url);
        while (!www.isDone)
        {
            // Show loader
            Debug.Log("Download progress: " + www.progress);
            yield return null;
        }
        yield return www;
        Debug.Log("Video downloaded!");

        Debug.Log("Video save path: " + m_pathToFile);
        File.WriteAllBytes(m_pathToFile, www.bytes);
        Debug.Log("Video Saved!");

        www.Dispose();
        www = null;

        PlayVideoFullscreen();
    }

	public void PlayVideoFullscreen()
    {
        if (File.Exists(m_pathToFile))
        {
#if !UNITY_WEBGL
            Handheld.PlayFullScreenMovie(m_pathToFile, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFit);
#endif
        }
    }
}
