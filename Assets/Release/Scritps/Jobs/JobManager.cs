using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchBot.Commands;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    public static List<Collector> collectorsList = new List<Collector>();
    public static event Action OnJobSet;
    public static event Action<int> OnExpGive;
    private Player player;


    private void Awake()
    {
        CollectorJob.OnJobChanged += SetJob;
        playerManager.OnPlayerAdded += SetJob;

    }

    private void SetJob(string id, Player.Jobs job, Enum jobType)
    {
        if (PlayerManager.playerList.Exists(x => x.id == id))
        {
            player = PlayerManager.playerList.FirstOrDefault(x => x.id == id);
            var currentJob = player.job;
            switch (currentJob)
            {
                case Player.Jobs.Collector:
                    SetCollectorJob(id, player.job, player.collector.resourceType);

                    break;
                case Player.Jobs.Trainer:


                    break;
                default:
                    break;
            }
        }
        playerManager.SavePlayerData(); 
        OnJobSet?.Invoke();

    }

    private void Start()
    {
        StartCoroutine(ExpController());
    }

    private IEnumerator ExpController()
    {
        while (true)
        {
            if (PlayerManager.playerList.Count > 0)
            {
                yield return new WaitForSeconds(5);
                OnExpGive?.Invoke(5);
                playerManager.SavePlayerData();
            }
            else
            {
                yield return null;
            }

        }
    }


    public void SetCollectorJob(string id, Player.Jobs job, Enum jobType)
    {      
        
        Collector.ResourceType currentType = (Collector.ResourceType)jobType;
        
        player.collector.resourceType = (Collector.ResourceType)jobType;
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
        switch (jobType)
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
             

    }

}
