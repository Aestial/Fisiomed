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

		[SerializeField] Sprite[] characterSprites;
		[SerializeField] GameObject bubblePrefab;
		[SerializeField] Transform containerPanel;

		[SerializeField] string url;

		Chat chat;
		int index = 0;

		void Start()
		{
			StartCoroutine(Downloader.Instance.LoadJSON(url, OnDownloaded));
		}

		void OnDownloaded(string json)
		{
			chat = JsonUtility.FromJson<Chat>(json);
			ShowBubble(chat, index);
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
