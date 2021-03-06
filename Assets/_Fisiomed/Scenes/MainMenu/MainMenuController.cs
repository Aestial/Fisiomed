﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loader;

namespace Fisiomed.Main
{
    using App;

    public class MainMenuController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] string defaultUrl = default;
        [Header("Visual")]
        [SerializeField] GameObject buttonPrefab = default;
        [SerializeField] Transform containerPanel = default;

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
                CCaseButtonController button = buttonGO.GetComponent<CCaseButtonController>();
                button.SetOption(option);
            }
        }
    }
}
