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
    public static List<Player> inactivePlayerList = new List<Player>();
    public static List<PlayerController> playerPrefabList = new List<PlayerController>();
    public GameSaver gameSaver;
    [SerializeField] private PlayerController playerPrefab;
    
    void Awake()
    {
        TwitchConnection.OnPlayerJoined += AddPlayerToGame;
        GameSaver.OnGameStart += LoadPlayers;
        BuyItem.OnItemBuy += UpdatePlayerInfo;
        InstantiatePlayerPool(20);
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
        if (inactivePlayerList.Count > 0)
        {
            foreach (var item in inactivePlayerList)
            {
                activePlayerList.Add(item);
                GetPlayerPrefabFromPool(item);
            }
            inactivePlayerList.Clear();
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
            inactivePlayerList.Add(temp);
        }
        else
        {
            CreatePlayer(id, name);
            Player temp = playerList.Find(x => x.id == id);            
            inactivePlayerList.Add(temp);
        }

    }

    private void GetPlayerPrefabFromPool(Player player)
    {
        if (playerPrefabList.Count > 0)
        {
            PlayerController playerPrefab = playerPrefabList[playerPrefabList.Count - 1];            
            playerPrefab.player = player;
            playerPrefabList.RemoveAt(playerPrefabList.Count - 1);
            playerPrefab.gameObject.SetActive(true);

        }
        
    }
    private void InstantiatePlayerPool(int numberOfPlayers)
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            PlayerController spawn = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, transform);            
            playerPrefabList.Add(spawn);
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
