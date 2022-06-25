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
    public Player player { get; set; }    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        pointToFollow = GameObject.Find("Point").GetComponent<Point>();
    }
    private void Start()
    {
        nameText.text = player.name;
        StartCoroutine(StartWalk());
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
