using System;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class UI_MessageItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _timeText;

    public void SetMessage(string _name, string _message, DateTime _time)
    {
        if (_nameText != null || !_name.IsNullOrEmpty())
        {
            _nameText.text = _name;
        }
        if (_messageText != null || !_message.IsNullOrEmpty())
        {
            _nameText.text = _message;
        }
        if (_timeText !=null)
        {
            _nameText.text = _time.ToString("tt hh:mm");
        }
    }
}
