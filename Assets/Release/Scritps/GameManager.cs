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
        var buy = new BuyItem("buyitem");
        CommandList.commands.Add(buy);
        var join = new Join("join");
        CommandList.commands.Add(join);        

        
        gameSaver = GetComponent<GameSaver>();
        bot = GetComponent<TwitchConnection>();


        gameSaver.LoadGame();
        
        bot.Connect(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           SaveGame();
        }
    }
    public void SaveGame()
    {
        gameSaver.SaveGame(new SavePlayerData { PlayerList = PlayerManager.playerList});
    }


}
