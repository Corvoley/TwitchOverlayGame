public class Collector
{
    public string id;
    public int experience = 0;
    public float bonusRate = 1f; 
    public ResourceType resourceType;
    public enum ResourceType
    {
        Food ,Wood, Gold, Stone
    }
    public Collector(string id)
    {
        this.id = id;
    }



}
