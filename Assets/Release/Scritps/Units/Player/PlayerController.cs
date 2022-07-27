using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Point pointToFollow;
    [SerializeField] private float timeToMove = 2f;
    [SerializeField] private TextMeshProUGUI nameText;
    private Point spawnPoint, woodPoint, foodPoint, goldPoint, stonePoint, barrackPoint;
    public Player player { get; set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        SetPointsOnAwake();
        JobManager.OnJobSet += UpdateCollectionPoint;
        JobManager.OnExpGive += GiveExp;
    }


    private void Start()
    {
        nameText.text = player.name;
        StartCoroutine(StartWalk());
    }
    private void GiveExp(int exp)
    {
        switch (player.job)
        {
            case Player.Jobs.Collector:
                player.collector.experience += exp;
                break;
            case Player.Jobs.Trainer:
                player.trainer.experience += exp;
                break;
            default:
                break;
        }
    }

    private void SetPointsOnAwake()
    {
        spawnPoint = GameObject.Find("Point").GetComponent<Point>();
        woodPoint = GameObject.Find("PointWood").GetComponent<Point>();
        foodPoint = GameObject.Find("PointFood").GetComponent<Point>();
        goldPoint = GameObject.Find("PointGold").GetComponent<Point>();
        stonePoint = GameObject.Find("PointStone").GetComponent<Point>();
        barrackPoint = GameObject.Find("PointBarrack").GetComponent<Point>();
        pointToFollow = spawnPoint;
    }
    private void UpdateCollectionPoint()
    {
        Debug.Log("PointUpdated");
        if (player != null)
        {
            switch (player.job)
            {
                case Player.Jobs.Collector:
                    switch (player.collector.resourceType)
                    {
                        case Collector.ResourceType.Wood:
                            pointToFollow = woodPoint;
                            break;
                        case Collector.ResourceType.Food:
                            pointToFollow = foodPoint;
                            break;
                        case Collector.ResourceType.Gold:
                            pointToFollow = goldPoint;
                            break;
                        case Collector.ResourceType.Stone:
                            pointToFollow = stonePoint;
                            break;
                        default:
                            break;
                    }
                    break;
                case Player.Jobs.Trainer:
                    pointToFollow = barrackPoint;
                    break;
                default:
                    pointToFollow = spawnPoint;
                    break;
            }


            
        }
    }
    private IEnumerator StartWalk()
    {
        while (true)
        {
            agent.SetDestination(pointToFollow.RandomPointOnCircleEdge());

            yield return new WaitForSeconds(timeToMove);
        }
    }

}
