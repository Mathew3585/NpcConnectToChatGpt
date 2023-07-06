using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.Events;

public class ChatGptManager : MonoBehaviour
{

    public OnReponseEvent onReponse;

    [System.Serializable]
    public class OnReponseEvent : UnityEvent<string> { }

    private OpenAIApi openAi = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage>();
    public async void AskChatGtp(string newText)
    {
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user"; 

        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";

        var reponse = await openAi.CreateChatCompletion(request);

        if(reponse.Choices != null && reponse.Choices.Count > 0)
        {
            var chatReponse = reponse.Choices[0].Message;
            messages.Add(chatReponse);

            Debug.Log(chatReponse);

            onReponse.Invoke(chatReponse.Content);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
