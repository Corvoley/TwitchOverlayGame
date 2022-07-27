using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TwitchBot.Commands;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    public static event Action OnJobSet;
    public static event Action<int> OnExpGive;
    private Player player;  


    private void Awake()
    {
        CollectorJob.OnJobChanged += SetJob;
        TrainerJob.OnJobChanged += SetJob;
        playerManager.OnPlayerAdded += SetJob;

    }

    private void SetJob(string id, Player.Jobs job, Enum jobType)
    {
        if (PlayerManager.playerList.Exists(x => x.id == id))
        {
            player = PlayerManager.playerList.FirstOrDefault(x => x.id == id);            
            player.job = job;
            switch (job)
            {
                case Player.Jobs.Collector:
                    ClearCurrentTrainerJob();
                    SetCollectorJob(jobType == null ? player.collector.resourceType : jobType);

                    break;
                case Player.Jobs.Trainer:
                    ClearCurrentCollectorJob();
                    SetTrainerJob(jobType == null ? player.trainer.soldierType : jobType);

                    break;
                default:
                    break;
            }
        }
        Debug.Log("antes da call");
        OnJobSet?.Invoke();
        Debug.Log("despois da call");
        playerManager.SavePlayerData(); 

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


    public void SetTrainerJob(Enum jobType)
    {
        Trainer.SoldierType currentType = (Trainer.SoldierType)jobType;
        player.trainer.soldierType = currentType;   
        ClearCurrentTrainerJob();

        switch (jobType)
        {
            case Trainer.SoldierType.Warrior:
                TrainerManager.warriorList.Add(player);
                break;
            case Trainer.SoldierType.Archer:
                TrainerManager.archerList.Add(player);
                break;
            default:
                break;
        }

    }
    private void ClearCurrentTrainerJob()
    {
        Trainer.SoldierType currentType = player.trainer.soldierType;
        switch (currentType)
        {
            case Trainer.SoldierType.Warrior:
                TrainerManager.warriorList.Remove(player);
                break;
            case Trainer.SoldierType.Archer:
                TrainerManager.archerList.Remove(player);
                break;
            default:
                break;
        }

    }

    public void SetCollectorJob(Enum jobType)
    {
        Collector.ResourceType currentType = (Collector.ResourceType)jobType;
        ClearCurrentCollectorJob();        
        player.collector.resourceType = currentType;

        switch (currentType)
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

    private void ClearCurrentCollectorJob()
    {
        Collector.ResourceType currentType = player.collector.resourceType;
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

    }

}
