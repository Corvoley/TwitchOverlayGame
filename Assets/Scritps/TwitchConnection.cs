using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchBot.Commands;

public class TwitchConnection : MonoBehaviour
{
    // Start is called before the first frame update
    private TwitchClient client;
    private ConnectionCredentials credentials;
    void Awake()
    {
        credentials = new ConnectionCredentials(TwitchInfo.ChannelName, TwitchInfo.BotToken);        
 
    }
    public void Connect(bool isLogging)
    {
        client = new Client();
        client.Initialize(credentials, TwitchInfo.ChannelName);

        if (isLogging)
        {
            client.OnLog += Client_OnLog;
        }
        client.Connect();

        client.OnError += Client_OnError;
        client.OnMessageReceived += Client_OnMessageReceived;
        client.OnChatCommandReceived += Client_OnChatCommandReceived;


    }

    private void Client_OnChatCommandReceived(object? sender, OnChatCommandReceivedArgs e)
    {
        foreach (var item in CommandList.commands)
        {
            if (item.CommandName == e.Command.CommandText.ToLower())
            {
                var name = e.Command.ChatMessage.Username;
                var arg = e.Command.ArgumentsAsString;
                item.CallFunction(name, arg);
                client.SendMessage(TwitchInfo.ChannelName, item.GetMessage(name, arg));
                Debug.Log("debugado");

            }
        }
    }

    private void Client_OnMessageReceived(object? sender, OnMessageReceivedArgs e)
    {

    }

    private void Client_OnError(object? sender, TwitchLib.Communication.Events.OnErrorEventArgs e)
    {
        
    }

    private void Client_OnLog(object? sender, OnLogArgs e)
    {
        
    }

    internal void Disconnect()
    {
        client.Disconnect();
    }
   
    void Update()
    {
        
    }
}
