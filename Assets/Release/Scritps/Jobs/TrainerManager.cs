using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainerManager : MonoBehaviour
{

    [SerializeField] public static List<Player> warriorList = new List<Player>();
    [SerializeField] public static List<Player> archerList = new List<Player>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (var player in warriorList)
            {
                Debug.Log($"Players com warrior: {player.name}");
            }
            foreach (var player in archerList)
            {
                Debug.Log($"Players com archer: {player.name}");
            }
        }
    }
    private void SetupSoldier()
    {
        foreach(var player in warriorList)
        {
            
        }  


    }

    private float SoldierGenerator(List<Player> playerList, float baseValue)
    {
        if (playerList.Count > 0)
        {
            return baseValue * playerList.Count * Time.deltaTime;
        }
        return 0;
    }

}
