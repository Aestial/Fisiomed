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
		[SerializeField] GameObject questionPrefab;
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
				ShowElement(index);
			if (index >= lenght - 1)
				nextButton.interactable = false;
		}

		private void ShowElement(int index)
		{
			// Dialog dialog = chat.dialogs[index];
			Element element = chat.dialogue[index];
			Character character = chat.characters[element.character];
			Sprite sprite = characterSprites[element.character];
			switch (element.type)
			{
				case ElementType.Message:
					Message message = chat.messages[element.message];
					GameObject newBubble = Instantiate(bubblePrefab, containerPanel);
					BubbleController bubbleC = newBubble.GetComponent<BubbleController>();
					bubbleC.Set(message, character, sprite);
				break;
				case ElementType.Question:
					Question question = chat.questions[element.question];
					GameObject newQuestion = Instantiate(bubblePrefab, containerPanel);
					QuestionController questionC = newQuestion.GetComponent<QuestionController>();
					questionC.Set(question, character, sprite);
				break;
				default:
				break;
			}
			
		}

		void OnMetadataLoaded(string json)
		{
			chat = JsonUtility.FromJson<Chat>(json);
			lenght = chat.dialogue.Length;
			Character[] characters = chat.characters;						
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
			ShowElement(index);			
		}

		void OnTextureLoaded(Texture texture)
		{
			Texture2D tex = (Texture2D) texture;
			Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
			characterSprites.Add(sprite);
		}

		
	}
}
