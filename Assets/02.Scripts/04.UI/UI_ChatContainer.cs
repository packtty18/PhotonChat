using System;
using UnityEngine;

//받은 메시지들에 대하여 ChatItem을 생성하고 담고 있는 UI
public class UI_ChatContainer : MonoBehaviour
{
    [SerializeField] private GameObject _otherChatItemPrefab;
    [SerializeField] private GameObject _myChatItemPrefab;
    [SerializeField] private GameObject _systemChatItemPrefab;

    [SerializeField] private Transform _contentRoot;

    private string _lastSender;
    private UI_ChatItem _lastChatItem;
    
    private void OnEnable()
    {
        ChatManager.OnNewMessage += OnNewMessage;
    }

    private void OnDisable()
    {
        ChatManager.OnNewMessage -= OnNewMessage;
    }

    private void OnNewMessage(ChatMessage message)
    {
        //todo
        //같은 샌더라면? lastSender에서 메시지 생성 및 할당
        if (string.Equals(_lastSender, message.Sender))
        {
            _lastChatItem.AddMessage(message);
            return;
        }
        //다른 샌더라면 새로운 Chat Item 생성
        
        switch (message.Type)
        {
            case ChatType.System:
                InstantChatItem(_systemChatItemPrefab,message);
                break;
            case ChatType.Mine :
                InstantChatItem(_myChatItemPrefab,message);
                break;
            case ChatType.Other :
                InstantChatItem(_otherChatItemPrefab,message);
                break;
        }
        
        _lastSender = message.Sender;
    }

    private void InstantChatItem(GameObject target, ChatMessage message)
    {
        UI_ChatItem item = Instantiate(target, _contentRoot).GetComponent<UI_ChatItem>();
        item.AddMessage(message);
        _lastChatItem = item;
    }
}
