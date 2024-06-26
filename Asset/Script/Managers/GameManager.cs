using TMPro;
using UnityEngine;
using UnityEngine.WSA;

public class GameManager : Singleton<GameManager>
{
    private int gold = 1000;

    public int GOLD
    {
        get { return gold; }
        set { gold = value; }
    }

    public Player Player { get; set; }

    public ObjectPool ObjectPool { get; private set; }

    private int stageIndex = 0;
    public int STAGE
    {
        get { return stageIndex; }
        set { stageIndex = value; }
    }

    protected override void Awake()
    {
        base.Awake();

        ObjectPool = GetComponent<ObjectPool>();
    }
     
    public void UseGold(int amount)
    {
        gold -= amount;
    }
}
