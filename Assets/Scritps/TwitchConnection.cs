using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TwitchLib.Unity;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchBot.Commands;
using System;

public class TwitchConnection : MonoBehaviour
{
    // Start is called before the first frame update
    private TwitchClient client;
    private ConnectionCredentials credentials;
    public static event Action<string, string> OnPlayerJoined;

    public void Connect(bool isLogging)
    {
        credentials = new ConnectionCredentials(TwitchInfo.ChannelName, TwitchInfo.BotToken);
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
                var id = e.Command.ChatMessage.UserId;
                var name = e.Command.ChatMessage.Username;                 
                var args = e.Command.ArgumentsAsString;
                CommandParametersHandler.param = id;
                CommandParametersHandler.param2 = name;
                CommandParametersHandler.param3 = args;
                item.CallFunction();
                Debug.Log("Comando Chamado");
               


                client.SendMessage(TwitchInfo.ChannelName, item.GetMessage(id, name, args));
               
            }
        }
    }

    private void Client_OnMessageReceived(object? sender, OnMessageReceivedArgs e)
    {
        if (!PlayerManager.playerList.Exists(x => x.id == e.ChatMessage.UserId))
        {
            OnPlayerJoined?.Invoke(e.ChatMessage.UserId, e.ChatMessage.Username);
            Debug.Log("aaa");
        }
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
   

}
