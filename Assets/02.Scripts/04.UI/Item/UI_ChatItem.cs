using UnityEngine;

public class UI_ChatItem : MonoBehaviour
{
    [SerializeField] private GameObject MessageItemPrefab;
    [SerializeField] private Transform MessageRoot;
    
    private string _sender;

    public bool IsSameSender(string sender)
    {
        return string.Equals(_sender, sender);
    }
    
    public void SetSender(string sender)
    {
        _sender = sender;
    }
    
    public UI_MessageItem InstantiateMessageItem()
    {
        GameObject messageItem = Instantiate(MessageItemPrefab, MessageRoot);
        return messageItem.GetComponent<UI_MessageItem>();
    }
    
    
}
