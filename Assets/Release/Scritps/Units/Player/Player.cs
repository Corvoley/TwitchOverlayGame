using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string id;
    public string name;
    public int itens;
   
    public Jobs job;
    public Collector collector;
    public enum Jobs
    {
        Collector
    }
    public Player(string id, string name)
    {
        this.id = id;
        this.name = name;        
    }

}
