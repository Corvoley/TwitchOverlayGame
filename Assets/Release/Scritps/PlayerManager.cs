using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TwitchBot.Commands;

public class Player
{
    public string id;
    public string name;
    public string description;
    public int itens; 

    public Player(string id, string name)
    {
        this.id = id;
        this.name = name;
    }

}

public class PlayerManager : MonoBehaviour
{
    public static List<Player> playerList = new List<Player>();
    public static List<Player> activePlayerList = new List<Player>();
    public GameSaver gameSaver;   
    
    void Awake()
    {
        TwitchConnection.OnPlayerJoined += AddPlayerToGame;
        GameSaver.OnGameStart += LoadPlayers;
        BuyItem.OnItemBuy += UpdatePlayerInfo;        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Jogadores Salvos");
            foreach (var item in playerList)
            {
                Debug.Log(item.name);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Jogadores em Jogo");
            foreach (var item in activePlayerList)
            {
                Debug.Log(item.name);
            }
        }
    }
    private void LoadPlayers()
    {
        playerList = GameSaver.CurrentPlayerSave.PlayerList;        
    }

    public void CreatePlayer(string id, string name)
    {               
        if (!playerList.Exists(x => x.id == id))
        {
            Player player = new Player(id, name);
            playerList.Add(player);
            SavePlayerData();
        }      
       
    }
    public void AddPlayerToGame(string id, string name)
    {
        if (playerList.Exists(x => x.id == id))
        {
            Player temp = playerList.Find(x => x.id == id);
            activePlayerList.Add(temp);
        }
        else
        {
            CreatePlayer(id, name);
            Player temp = playerList.Find(x => x.id == id);
            activePlayerList.Add(temp);
        }
    }
    public void UpdatePlayerInfo(string id, int value)
    {
        if (playerList.Exists(x => x.id == id))
        {
            playerList.FirstOrDefault(x=> x.id == id).itens += value;
            SavePlayerData();
        }
        
    }
    private void SavePlayerData()
    {       
        gameSaver.SaveGame(new SavePlayerData { PlayerList = playerList });
    }

}
