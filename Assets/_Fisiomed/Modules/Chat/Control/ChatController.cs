using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Loader;

namespace Fisiomed.Chat
{
	using App;

	public class ChatController : Singleton<ChatController> 
	{	
		[Header("Visual")]
		[SerializeField] Canvas canvas = default;
		
		[SerializeField] GameObject messageBubblePrefab = default;
		[SerializeField] GameObject questionBubblePrefab = default;
		[SerializeField] GameObject interactiveBubblePrefab = default;
		[SerializeField] GameObject videoBubblePrefab = default;
		[SerializeField] GameObject endBubblePrefab = default;

		[SerializeField] Transform containerPanel = default;
		[SerializeField] Button nextButton = default;
		public List<Sprite> characterSprites = new List<Sprite>();
        
		[SerializeField] int lenght = default;
		[SerializeField] int currentIndex = default;
		Chat chat;
		
		#region Public Methods
		public void Set(Chat chat)
		{
			this.chat = chat;
			lenght = chat.sequence.Length;
			Character[] characters = chat.characters;						
			// Download Character Images
			StartCoroutine(DownloadSprites());
		}
		public void NextBubble()
		{
            bool last = currentIndex <= lenght - 1;
            //canvas.enabled &= currentIndex <= lenght;
            canvas.enabled = last;
            nextButton.interactable = last;
			if (++currentIndex < lenght)
				ShowElement(currentIndex);
			else
            {
				ShowEnd();
				// NOTIFY: FINISHED CHAT
            }
		}
		#endregion		
		private void ShowElement(int index)
		{
			Element element = chat.sequence[index];
			Character character = chat.characters[element.character];
			Sprite sprite = characterSprites[element.character];
			switch ((ElementType)element.type)
			{
				case ElementType.Message:
					Message message = chat.messages[element.index];
					GameObject newMessage = Instantiate(messageBubblePrefab, containerPanel);
					MessageController messageC = newMessage.GetComponent<MessageController>();
					messageC.Set(message.text, character, sprite);
					nextButton.interactable = true;
				break;
				case ElementType.Question:
					Question question = chat.questions[element.index];
					GameObject newQuestion = Instantiate(questionBubblePrefab, containerPanel);
					QuestionController questionC = newQuestion.GetComponent<QuestionController>();
					questionC.Set(this, question, character, sprite);
					nextButton.interactable = false;
				break;
				case ElementType.Interactive:
                    Interactive interactive = chat.interactives[element.index];
                    GameObject newInteractive = Instantiate(interactiveBubblePrefab, containerPanel);
                    InteractiveController interactiveC = newInteractive.GetComponent<InteractiveController>();                    
                    interactiveC.Set(interactive, character, sprite);
                    nextButton.interactable = false;
				break;
                case ElementType.Video:
                    Video video = chat.videos[element.index];
                    GameObject newVideo = Instantiate(videoBubblePrefab, containerPanel);
                    VideoController videoC = newVideo.GetComponent<VideoController>();
                    videoC.Set(video, character, sprite);
                    nextButton.interactable = false;
                    break;
				default:
				break;
			}			
		}		
		private void ShowEnd()
		{
			GameObject newMessage = Instantiate(endBubblePrefab, containerPanel);
			
			// MessageController messageC = newMessage.GetComponent<MessageController>();
			// messageC.Set(message, character, sprite);
			// nextButton.interactable = true;
		}
		IEnumerator DownloadSprites()
		{
			for (int i = 0; i < chat.characters.Length; i++)
			{
				string url = chat.characters[i].imageUrl;
				yield return StartCoroutine(Downloader.Instance.LoadTexture(url, OnTextureLoaded));
			}
			yield return null;			
			ShowElement(currentIndex);	
			AppManager.Instance.ShowLoader(false);
		}
		void OnTextureLoaded(Texture texture)
		{
			Texture2D tex = (Texture2D) texture;
			Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
			characterSprites.Add(sprite);
		}	
	}
}
