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
    private Point woodPoint, foodPoint, goldPoint, stonePoint;
    public Player player { get; set; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        SetPointsOnAwake();
        JobManager.OnCollectorJobSet += UpdateCollectionPoint;
    }


    private void Start()
    {
        nameText.text = player.name;
        StartCoroutine(StartWalk());
    }

    private void SetPointsOnAwake()
    {
        pointToFollow = GameObject.Find("Point").GetComponent<Point>();
        woodPoint = GameObject.Find("PointWood").GetComponent<Point>();
        foodPoint = GameObject.Find("PointFood").GetComponent<Point>();
        goldPoint = GameObject.Find("PointGold").GetComponent<Point>();
        stonePoint = GameObject.Find("PointStone").GetComponent<Point>();
    }
    private void UpdateCollectionPoint()
    {
        if (player.collector != null)
        {          
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
