using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using NUnit.Framework;
using Photon.Chat;
using UnityEditor.VersionControl;
using UnityEngine;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    private static ChatManager _instance;
    public static ChatManager Instance => _instance;
    
    private const string CHANNEL_SKKU = "skku2";
    private const string CHANNEL_NOTICE = "notice";
    private const string NICKNAME = "성훈";
    private ChatClient _client;

    private List<ChatMessage> _messages = new List<ChatMessage>();
    public static event Action<ChatMessage> OnNewMessage; //새로운 메시지가 등록됨
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        _client = new ChatClient(this);
        _client.DebugOut = DebugLevel.ALL;
        _client.ChatRegion = "ASIA";
        var auth = new AuthenticationValues(NICKNAME);
        _client.Connect("8952ee8f-6b2e-45c9-9cdb-b60a2f906585","1.0",auth);
    }

    private void Update()
    {
        //중요@@@
        //chatClient는 Mono가 아니기에 매 프레임 서비스 펌프를 호출해줘야한다.
        _client.Service();
    }

    // 01. 연결상태 관련
    public void OnConnected()
    {
        Debug.Log($"[Photon Chat] : 서버와 연결됨");

        _client.Subscribe(new String[]{CHANNEL_SKKU, CHANNEL_NOTICE});
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log($"[Photon Chat] : 연결상태 변경 ▶ {state}");
    }
    
    public void OnDisconnected()
    {
        Debug.Log($"[Photon Chat] : 서버와 연결 해제됨");
    }

    // 02. 채널 접속 관련
    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            string result = results[i] ? "성공" : "실패";

            Debug.Log($"[Photon Chat] : 채널 {channels[i]} 구독 {result}");
        }

        foreach (var channel in _client.PublicChannels)
        {
            //여기서 내가 구독중인 채널 목록 확인 가능
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            Debug.Log($"[Photon Chat] : 채널 {channels[i]} 구독 해지");
        }
    }
    
    // 03. 다른 유저들의 온라인 상태
    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log($"[Photon Chat] : {channel}에 {user} 입장");
        OnNewMessage?.Invoke(ChatMessage.CreateSystem("system",$"{user}님이 입장하셨습니다."));
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log($"[Photon Chat] : {channel}에 {user} 퇴장");
        OnNewMessage?.Invoke(ChatMessage.CreateSystem("system",$"{user}님이 퇴장하셨습니다."));
    }
    
    //친구 혹은 팔로우 리스트 등 특정 유저의 상태 변경시
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log($"{nameof(OnStatusUpdate)} 실행");
    }

    
    //04. 메시지 수신
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        //한 프레임에 여러개의 메시지를 받으면 배열로 묶어서 한번에 전달함(최적화)
        for (int i = 0; i < messages.Length; i++)
        {
            Debug.Log($"[Photon Chat] : [{channelName}] {senders[i]}: {messages[i]}");
            if (senders[i] == NICKNAME)
            {
                OnNewMessage?.Invoke(ChatMessage.CreateMine(senders[i], messages[i].ToString()));
            }
            else
            {
                OnNewMessage?.Invoke(ChatMessage.CreateOther(senders[i],messages[i].ToString()));
            }
        }
        
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string[] messages = message.ToString().Split(' ');
        for (int i = 0; i < messages.Length; i++)
        {
            Debug.Log($"[Photon Chat] : [{channelName}] {sender}: {messages[i]}");
        }
    }
    
    
    //포톤챗 내부에서 디버그 로그가 발생할 경우 호출됨
    public void DebugReturn(DebugLevel level, string message)
    {
        switch (level)
        {
         case DebugLevel.ERROR :  
             Debug.LogError($"[Photon Error] : {message}");
             break;
         case DebugLevel.WARNING :  
             Debug.LogWarning($"[Photon Warning] : {message}");
             break;
         default:
             Debug.Log($"[Photon Log] : [{level}] {message}");
             break;
        }
        
    }

    public void SendChatMessage(string message)
    {
        if (_client == null) return;
        if (_client.CanChat == false) return;

        _client.PublishMessage(CHANNEL_SKKU,message);
    }
    
}
