using System;
using UnityEngine;

public class UI_ChatMessages : MonoBehaviour
{
    [SerializeField] private GameObject _otherChatItemPrefab;
    [SerializeField] private GameObject _myChatItemPrefab;
    [SerializeField] private GameObject _systemChatItemPrefab;

    [SerializeField] private Transform _contentRoot;
    
    private UI_ChatItem _lastChatItem;
    
    private void Start()
    {
        ChatManager.OnNewMessage += OnNewMessage;
    }

    private void OnNewMessage(ChatMessage message)
    {
        bool isSameSender = _lastChatItem.IsSameSender(message.Sender);
        //todo
        //같은 샌더라면? lastSender에서 메시지 생성 및 할당
        
        //다른 샌더라면 새로운 Chat Item 생성
 
        switch (message.Type)
        {
            case ChatType.System:
                
                break;
            case ChatType.Mine :
                
                break;
            case ChatType.Other :
                
                break;
        }

        
    }
}
