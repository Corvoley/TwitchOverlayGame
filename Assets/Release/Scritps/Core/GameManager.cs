using System;
using System.Collections;
using System.Collections.Generic;
using TwitchBot.Commands;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private GameSaver gameSaver;
    private TwitchConnection bot;   

    void Awake()
    {        
        Application.runInBackground = true; 
        var buy = new BuyItem("buyitem");
        CommandList.commands.Add(buy);
        var collector = new Collector("collector");
        CommandList.commands.Add(collector);        

        
        gameSaver = GetComponent<GameSaver>();
        bot = GetComponent<TwitchConnection>();


        gameSaver.LoadGame();

        //bot.Connect(true);
    }

    private void Update()
    {
        
    }
    public void SaveGame()
    {
        gameSaver.SaveGame(new SavePlayerData { PlayerList = PlayerManager.playerList});
    }

    private void OnDestroy()
    {
        bot.Disconnect();
    }


}
