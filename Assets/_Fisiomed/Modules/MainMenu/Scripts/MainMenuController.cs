using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loader;

namespace Fisiomed.Main
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] string defaultUrl;
        [Header("Visual")]
        [SerializeField] GameObject buttonPrefab;
        [SerializeField] Transform containerPanel;

        List<Sprite> optionSprites = new List<Sprite>();

        void Start()
        {
            AppManager.Instance.ShowLoader(true);
            StartCoroutine(Downloader.Instance.LoadJSON(defaultUrl, OnMetadataLoaded));
        }
        void OnMetadataLoaded(string json)
        {
            Menu menu = JsonUtility.FromJson<Menu>(json);
			DrawButtons(menu);
            AppManager.Instance.ShowLoaderDelay(false, 1.2f);
        }
        void DrawButtons(Menu menu)
        {
            for (int i = 0; i < menu.options.Length; i++)
            {
                Option option = menu.options[i];
                GameObject buttonGO = Instantiate(buttonPrefab, containerPanel);
                ButtonController button = buttonGO.GetComponent<ButtonController>();
                button.SetOption(option);
            }
        }
    }
}
