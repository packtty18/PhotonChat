using System;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class UI_MessageItem : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _timeText;
    
    public void SetMessage(ChatMessage message)
    {
        if (_messageText != null && !message.Message.IsNullOrEmpty())
        {
            _messageText.text = message.Message;
        }
        if (_timeText !=null)
        {
            _timeText.text = message.Timestamp.ToString("tt hh:mm");
        }
    }
}
