using UnityEngine;
[System.Serializable]
public class BoardLayoutData 
{
    public int with;
    public int height;
    public int[] wallTypesData;
    public BoardLayoutData() { }
    public BoardLayoutData(WallType[,] wallTypesData, int with, int height)
    {
        this.with = with;   
        this.height = height;
        this.wallTypesData = new int[with * height];
        for (int i = 0; i < with; i++)
        {
            for(int j = 0; j < height; j++)
            {
                this.wallTypesData[i * height + j] = (int)wallTypesData[i, j];     
            }
        }
    }
}
