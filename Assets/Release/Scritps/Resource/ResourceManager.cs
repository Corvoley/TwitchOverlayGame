using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static List<Player> foodPlayerList = new List<Player>();
    public static List<Player> woodPlayerList = new List<Player>();

    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI woodText;

    public float Wood { get; set; }
    private float woodBaseProduction = 1f;
    public float Food { get; set; }
    private float foodBaseProduction = 1f;
 

    private void Update()
    {       
        foodText.text = ((int)Food).ToString();
        woodText.text = ((int)Wood).ToString();
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
