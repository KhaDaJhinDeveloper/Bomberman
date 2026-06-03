using UnityEngine;
[System.Serializable]
public class BuffITemData 
{
    public DataBuffDetail[] buffData;
    public BuffITemData () { }
    public BuffITemData(BuffPickUp[] data)
    {
        this.buffData = new DataBuffDetail[data.Length];
        for (int i = 0; i < buffData.Length; i++)
        {
            this.buffData[i] = new DataBuffDetail((int)data[i].buffData.buffKey, data[i].transform.position);
        }            
    }
}
[System.Serializable]
public class DataBuffDetail
{
    public int BuffKey;
    public Vector3 position;
    public DataBuffDetail() { }
    public DataBuffDetail(int buffKey, Vector3 position) 
    { 
        this.BuffKey = buffKey; 
        this.position = position; 
    }
}
