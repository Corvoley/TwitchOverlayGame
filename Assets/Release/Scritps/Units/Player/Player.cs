using System;
public class Player
{
    public string id;
    public string name;
    public int itens;
   
    public Jobs job;
    public Collector collector;
    public Trainer trainer;
    public Enum jobType;
    public enum Jobs
    {
        Collector = 1,
        Trainer = 2,

    }
    public Player(string id, string name)
    {
        this.id = id;
        this.name = name;
        
    }

}
