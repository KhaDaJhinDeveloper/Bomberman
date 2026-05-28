using UnityEngine;

public abstract class SpawnBase : MonoBehaviour
{
    [SerializeField] protected PoolEntry[] poolArray;
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
       
    }
    protected virtual void CreatePool()
    {

        for (int i = 0; i < this.poolArray.Length; i++)
        {
            ObjectPooling.Instance.CreatePool(this.poolArray[i].key, 
                                                            this.poolArray[i].prefabs,
                                                            this.poolArray[i].poolSize);
        }
    }
}
