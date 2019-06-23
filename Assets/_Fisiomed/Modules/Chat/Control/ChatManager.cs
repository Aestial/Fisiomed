using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using TMPro;
using Loader;

namespace Fisiomed.Chat
{
	public class ChatManager : MonoBehaviour 
	{	
		[Header("Data")]
		[SerializeField] string defaultUrl;
		[Header("Visual")]
		[SerializeField] GameObject bubblePrefab;
		[SerializeField] Transform containerPanel;
		[SerializeField] Button nextButton;

		List<Sprite> characterSprites = new List<Sprite>();
		int lenght = 0;
		int index = 0;
		Chat chat;

		void Start()
		{
			AppManager.Instance.ShowLoader(true);
			string url = PlayerPrefs.GetString("url", defaultUrl);
			StartCoroutine(Downloader.Instance.LoadJSON(url, OnMetadataLoaded));
		}

		public void NextBubble()
		{
			if (++index < lenght)
				ShowBubble(chat, index);
			if (index >= lenght - 1)
				nextButton.interactable = false;
		}

		private void ShowBubble(Chat chat, int index)
		{
			Dialog dialog = chat.dialogs[index];
			Character character = chat.characters[dialog.character];

			GameObject bubbleGO = Instantiate(bubblePrefab, containerPanel);
			BubbleController bubble = bubbleGO.GetComponent<BubbleController>();
			bubble.SetDialog(dialog, character);
			bubble.SetSprite(characterSprites[dialog.character]);
		}

		void OnMetadataLoaded(string json)
		{
			chat = JsonUtility.FromJson<Chat>(json);
			lenght = chat.dialogs.Length;
			Character[] characters = chat.characters;

			// Start Chat
			// ShowBubble(chat, index);
		
			// Download Character Images
			StartCoroutine(DownloadSprites());
		}

		IEnumerator DownloadSprites()
		{
			for (int i = 0; i < chat.characters.Length; i++)
			{
				string url = chat.characters[i].imageUrl;
				yield return StartCoroutine(Downloader.Instance.LoadTexture(url, OnTextureLoaded));
			}
			yield return null;
			AppManager.Instance.ShowLoader(false);
			ShowBubble(chat, index);			
		}

		void OnTextureLoaded(Texture texture)
		{
			Texture2D tex = (Texture2D) texture;
			Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
			characterSprites.Add(sprite);
		}

		
	}
}
