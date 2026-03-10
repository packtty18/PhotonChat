using System;
using System.Xml.Schema;

public class ChatMessage
{
    public readonly ChatType Type;
    public readonly string Sender;
    public readonly string Message;
    public readonly DateTime Timestamp;

    private ChatMessage(ChatType type,string sender, string message)
    {
        Type = type;
        Sender = sender;
        Message = message;
        Timestamp = DateTime.Now;
    }
    
    
    //팩토리 메서드 패턴
    public static ChatMessage CreateMine(string sender, string message)
    {
        return new ChatMessage(ChatType.Mine,sender, message);    
    }
    
    public static ChatMessage CreateOther(string sender, string message)
    {
        return new ChatMessage(ChatType.Other,sender, message);    
    }
    
    public static ChatMessage CreateSystem(string sender ,string message)
    {
        return new ChatMessage(ChatType.System,sender,  message);    
    }
}