using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//사용자의 입력의 뷰
public class UI_ChatInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _chatInput;
    [SerializeField] private Button _sendButton;

    private void Start()
    {
        _sendButton.onClick.AddListener(SendMessage);
    }

    public void SendMessage()
    {
        string message = _chatInput.text;
        if (string.IsNullOrEmpty(message)) return;
        
        ChatManager.Instance.SendChatMessage(message);
        
        _chatInput.text = "";
    }
}
