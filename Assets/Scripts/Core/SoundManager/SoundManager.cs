using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource audioBG;
    [SerializeField] private AudioSource audioSFX;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        CreateSoundPool();
    }
    private void CreateSoundPool()
    {
        ObjectPooling.Instance.CreatePool(KeyPool.KEY_SOUND_MUSICBACKGROUND, this.audioBG.gameObject, 3, this.transform);
        ObjectPooling.Instance.CreatePool(KeyPool.KEY_SOUND_MUSICSFX, this.audioSFX.gameObject, 8, this.transform);
    }    
}
