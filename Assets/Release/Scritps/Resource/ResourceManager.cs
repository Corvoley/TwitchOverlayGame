using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static List<Player> foodPlayerList = new List<Player>();
    public static List<Player> woodPlayerList = new List<Player>();
    public static List<Player> goldPlayerList = new List<Player>();
    public static List<Player> stonePlayerList = new List<Player>();

    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI stoneText;
    public float Wood { get; set; }
    private float woodBaseProduction = 0.1f;
    public float Food { get; set; }
    private float foodBaseProduction = 0.1f;
    public float Gold { get; set; }
    private float goldBaseProduction = 0.1f;
    public float Stone { get; set; }
    private float stoneBaseProduction = 0.1f;

    private void Update()
    {
        Food += ResourceGenerator(foodPlayerList, foodBaseProduction);
        Wood += ResourceGenerator(woodPlayerList, woodBaseProduction);
        Gold += ResourceGenerator(goldPlayerList, goldBaseProduction);
        Stone += ResourceGenerator(stonePlayerList, stoneBaseProduction);
        foodText.text = ((int)Food).ToString();
        woodText.text = ((int)Wood).ToString();
        goldText.text = ((int)Gold).ToString();
        stoneText.text = ((int)Stone).ToString();
    }

    private float ResourceGenerator(List<Player> resourceList, float baseValue)
    {
        if (resourceList.Count > 0)
        {
            return baseValue * resourceList.Count * Time.deltaTime;  
        }
        return 0;
    }
    


}
