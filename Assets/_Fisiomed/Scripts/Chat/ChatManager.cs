using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;
using TMPro;
using Fisiomed.Loader;

namespace Fisiomed.Chat
{
	public class ChatManager : MonoBehaviour {

		// [SerializeField] Sprite[] characterSprites;
		[SerializeField] GameObject bubblePrefab;
		[SerializeField] Transform containerPanel;

		[SerializeField] string url;

		public List<Sprite> characterSprites = new List<Sprite>();
		int index = 0;
		Chat chat;

		void Start()
		{
			AppManager.Instance.ShowLoader(true);
			StartCoroutine(Downloader.Instance.LoadJSON(url, OnMetadataLoaded));
		}

		void OnMetadataLoaded(string json)
		{
			chat = JsonUtility.FromJson<Chat>(json);
			Character[] characters = chat.characters;
			
			// Download Character Images
			StartCoroutine(DownloadSprites());

			// // Change to OnCharactersLoaded
			// ShowBubble(chat, index);
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

		private void ShowBubble(Chat chat, int index)
		{
			Dialog dialog = chat.dialogs[index];
			Character character = chat.characters[dialog.character];

			GameObject newBubble = Instantiate(bubblePrefab, containerPanel);
			BubbleController newBC = newBubble.GetComponent<BubbleController>();
			newBC.content = dialog.message;
			newBC.characterSprite = characterSprites[dialog.character];
			BubbleSide side = character.side == "left" ? BubbleSide.Left:BubbleSide.Right;
			newBC.side = side;
			Color faceBColor = new Color();
			Color texsBColor = new Color();
			ColorUtility.TryParseHtmlString (character.faceBColor, out faceBColor);
			ColorUtility.TryParseHtmlString (character.textBColor, out texsBColor);
			newBC.charBColor = faceBColor;
			newBC.textBColor = texsBColor;
			newBC.radius = 60;
			newBC.SetAll();
		}

		public void NextBubble()
		{
			ShowBubble(chat, ++index);
		}
	}
}
