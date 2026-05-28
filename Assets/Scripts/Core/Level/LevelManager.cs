using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int currentLevel = 0;
    public LayoutLevel[] levelArray;
    protected override void Awake()
    {
        base.Awake();
    }
    public LayoutLevel GetLevel()
    {
        if(this.currentLevel > levelArray.Length)
            this.currentLevel = 0;   
        return levelArray[currentLevel]; 
    }
    public void ResetLevel()
    {
        this.currentLevel = 0;
    }
    public LayoutLevel NextLevel()
    {
        this.currentLevel++;
        if (this.currentLevel >= levelArray.Length)
            this.currentLevel = 0;
        return levelArray[currentLevel];
    }    
}
