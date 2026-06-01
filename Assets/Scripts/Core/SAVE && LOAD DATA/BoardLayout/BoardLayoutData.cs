using UnityEngine;
[System.Serializable]
public class BoardLayoutData 
{
    public WallType[,] wallTypesData;
    public BoardLayoutData(WallType[,] wallTypesData)
    {
        this.wallTypesData = wallTypesData;
    }
}
