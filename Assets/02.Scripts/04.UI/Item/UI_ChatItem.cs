using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UI_ChatItem : MonoBehaviour
{
    [SerializeField] private GameObject _messageItemPrefab;

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Transform _messageRoot;
    
    public void AddMessage(ChatMessage message)
    {
        if (_nameText != null 
            && !string.IsNullOrEmpty(message.Sender)
            && !string.Equals(message.Sender, _nameText.text))
        {
            _nameText.text = message.Sender;
        }
        
        UI_MessageItem messageItem = Instantiate(_messageItemPrefab, _messageRoot).GetComponent<UI_MessageItem>();
        messageItem.SetMessage(message);
    }
}
