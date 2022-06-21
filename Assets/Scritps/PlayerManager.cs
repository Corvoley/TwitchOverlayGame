using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TwitchBot.Commands;
using System.Threading.Tasks;

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
    private static event Action OnItemUpdate;
    public static List<Player> playerList = new List<Player>();
    public GameSaver gameSaver;
    
    
    void Awake()
    {
        TwitchConnection.OnPlayerJoined += CreatePlayer;
        GameSaver.OnGameStart += LoadPlayers;
        BuyItem.OnItemBuy += UpdatePlayerInfo;
        OnItemUpdate += SavePlayerData;

    }


    private void LoadPlayers()
    {
        playerList = GameSaver.CurrentPlayerSave.PlayerList;        
    }

    public void CreatePlayer(string id, string name)
    {       

        Player player = new Player(id, name);
        if (!playerList.Exists(x => x.id == id))
        {
            playerList.Add(player);  
        }            

    }

    public void UpdatePlayerInfo(string id, int value)
    {
        if (playerList.Exists(x => x.id == id))
        {
            playerList.FirstOrDefault(x=> x.id == id).itens += value;
        }       
        
        //Função é chamada mas o SaveGame nao acontece.
        //SavePlayerData();
        
    }
    private void SavePlayerData()
    {        
        gameSaver.SaveGame(new SavePlayerData { PlayerList = playerList });
    }

}
