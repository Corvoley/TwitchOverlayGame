public class AutoDisablePoolableObject : PoolableObject
{
    public float AutoDisableTime = 5f;
    private const string DisableMethodName = "Disable";
    public virtual void OnEnable()
    {
        CancelInvoke(DisableMethodName);
        Invoke(DisableMethodName, AutoDisableTime);
    }
    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }

}
