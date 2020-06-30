using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fisiomed.App
{
    public class AppManager : Singleton<AppManager>
    {
        public bool iAmFirst;
        [SerializeField] private float sceneChangeDelay = default;
        [SerializeField] private GameObject loaderPrefab = default;
        LoaderController loader;

        // Called first
        void Awake()
        {
            #region Don't Destroy OnLoad Singleton
            DontDestroyOnLoad(Instance);
            AppManager[] appManagers = FindObjectsOfType(typeof(AppManager)) as AppManager[];
            if (appManagers.Length > 1)
            {
                for (int i = 0; i < appManagers.Length; i++)
                {
                    if (!appManagers[i].iAmFirst)
                        DestroyImmediate(appManagers[i].gameObject);
                }
            }
            else
                iAmFirst = true;
            #endregion
        }
        // Called second
        void OnEnable()
        {
            // Debug.Log("OnEnable called");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        // Called third
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Log.Color("OnSceneLoaded: " + scene.name, this);
            if (!loader)
            {
                GameObject newLoader = Instantiate(loaderPrefab, this.transform);
                loader = newLoader.GetComponent<LoaderController>();
            }
            loader.SetLoader(false);
        }
        // Called when the game is terminated
        void OnDisable()
        {
            // Debug.Log("OnDisable");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private IEnumerator LoadAndChangeCoroutine(int scene, float time)
        {
            loader.SetLoader(true);
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(scene);
        }
        private IEnumerator LoadAndChangeCoroutine(string scene, float time)
        {
            loader.SetLoader(true);
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(scene);
        }
        public void ChangeSceneDelayBefore(string sceneName)
        {
            void callbackAction() => ChangeScene(sceneName);
            ShowLoaderDelayedCallback(true, sceneChangeDelay, callbackAction);
        }
        public void ChangeScene(int scene)
        {
            StartCoroutine(this.LoadAndChangeCoroutine(scene, 1.1f));
        }
        public void ChangeScene(string sceneName)
        {
            StartCoroutine(this.LoadAndChangeCoroutine(sceneName, 1.1f));
        }
        public void ShowLoader(bool show)
        {
            loader.SetLoader(show);
        }
        public void ShowLoaderDelay(bool show, float delay)
        {
            StartCoroutine(ShowLoaderCoroutine(show, delay));
        }
        public void ShowLoaderDelayedCallback(bool show, float delay, Action onCompleted)
        {
            StartCoroutine(ShowLoaderCoroutine(show, delay, onCompleted));
        }
        public IEnumerator ShowLoaderCoroutine(bool show, float delay)
        {
            yield return new WaitForSeconds(delay);
            loader.SetLoader(show);
        }
        public IEnumerator ShowLoaderCoroutine(bool show, float delay, Action onCompleted)
        {
            yield return new WaitForSeconds(delay);
            loader.SetLoader(show);
            onCompleted();
        }
    }
}
