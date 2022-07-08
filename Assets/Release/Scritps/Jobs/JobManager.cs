using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchBot.Commands;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    [SerializeField]private GameSaver gameSaver;
    public static List<Collector> collectorsList = new List<Collector>();

    private void Awake()
    {
        CollectorJob.OnJobChanged += SetJob;
        GameSaver.OnGameStart += LoadPlayers;
    }

    private void LoadPlayers()
    {
        collectorsList = GameSaver.CurrentCollectorSave.CollectorList;
    }

    public void SetJob(string id,Player.Jobs job ,Collector.ResourceType resourceType)
    {
        
        if (PlayerManager.playerList.Exists(x => x.id == id))
        {

            Player player = PlayerManager.playerList.FirstOrDefault(x => x.id == id);
            if (collectorsList.Exists(x => x.id == id))
            {
                Collector collector = collectorsList.FirstOrDefault(x => x.id == id);             
                
                player.collector = collector;
            }
            else
            {
                CreateCollectorJob(id , player);                
            }
            Collector.ResourceType currentType = resourceType;
            player.job = job;
            player.collector.resourceType = resourceType;
            switch (currentType)
            {
                case Collector.ResourceType.Wood:
                    ResourceManager.woodPlayerList.Remove(player);
                    break;
                case Collector.ResourceType.Food:
                    ResourceManager.foodPlayerList.Remove(player);
                    break;
                case Collector.ResourceType.Gold:
                    ResourceManager.goldPlayerList.Remove(player);
                    break;
                case Collector.ResourceType.Stone:
                    ResourceManager.stonePlayerList.Remove(player);
                    break;
                default:

                    break;
            }
            switch (resourceType)
            {
                case Collector.ResourceType.Wood:
                    ResourceManager.woodPlayerList.Add(player);
                    break;
                case Collector.ResourceType.Food:
                    ResourceManager.foodPlayerList.Add(player);
                    break;
                case Collector.ResourceType.Gold:
                    ResourceManager.goldPlayerList.Add(player);
                    break;
                case Collector.ResourceType.Stone:
                    ResourceManager.stonePlayerList.Add(player);
                    break;
                default:

                    break;
            }

            SaveCollectorData();
        }

    }

    private void CreateCollectorJob(string id, Player player)
    {
        if (!collectorsList.Exists(x => x.id == id))
        {
            Collector collector = new Collector(id);
            collectorsList.Add(collector);
            player.collector = collector;
        }
        
    }

    private void SaveCollectorData()
    {
        gameSaver.SaveCollectorData(new CollectorData { CollectorList = collectorsList });
    }
}
