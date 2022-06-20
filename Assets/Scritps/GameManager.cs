using System.Collections;
using System.Collections.Generic;
using TwitchBot.Commands;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private TwitchConnection bot;
    void Awake()
    {
        var buy = new BuyItem("buyitem");
        CommandList.commands.Add(buy);
        bot = GetComponent<TwitchConnection>(); 
        
        bot.Connect(true);
    }


}
