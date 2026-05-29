using System.Collections;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    #region input
    [SerializeField] private AudioSource audioBG;
    [SerializeField] private AudioSource audioSFX;
    public float volumeBG = 1;
    public float volumeSFX = 1;
    private AudioSource currentMusicBG;
    #endregion
    [Header("SoundAsset")]
    public AudioClip bg_MainMenu;
    public AudioClip bg_Play;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        CreateSoundPool();
        PlayMusicBG(SoundManager.Instance.bg_MainMenu);
    }
    private void CreateSoundPool()
    {
        ObjectPooling.Instance.CreatePool(KeyPool.KEY_SOUND_MUSICBACKGROUND, this.audioBG.gameObject, 3, this.transform);
        ObjectPooling.Instance.CreatePool(KeyPool.KEY_SOUND_MUSICSFX, this.audioSFX.gameObject, 8, this.transform);
    }
    #region MusicBG
    public void PlayMusicBG(AudioClip clipBG)
    {
        StopPlayMusicBG();
        AudioSource audioBG = ObjectPooling.Instance.GetPool(KeyPool.KEY_SOUND_MUSICBACKGROUND, this.transform).GetComponent<AudioSource>();
        this.currentMusicBG = audioBG;
        this.currentMusicBG.clip = clipBG;
        this.currentMusicBG.volume = this.volumeBG;
        this.currentMusicBG.Play();
    }
    public void StopPlayMusicBG()
    {
        if (this.currentMusicBG != null)
        {
            this.currentMusicBG.Stop();
            ObjectPooling.Instance.ReturnToPool(KeyPool.KEY_SOUND_MUSICBACKGROUND, this.currentMusicBG.gameObject);
        }               
        this.currentMusicBG = null;
    }
    #endregion
    #region MusicSFX
    public void PlayMusicSFX(AudioClip clipSFX)
    {
        AudioSource audioSFX = ObjectPooling.Instance.GetPool(KeyPool.KEY_SOUND_MUSICSFX, this.transform).GetComponent<AudioSource>();
        audioSFX.clip = clipSFX;
        audioSFX.Play();
        StartCoroutine(ReturnSFX(audioSFX, clipSFX.length));
    } 
    IEnumerator ReturnSFX(AudioSource objSfx, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPooling.Instance.ReturnToPool(KeyPool.KEY_SOUND_MUSICSFX, objSfx.gameObject);
    }
    #endregion
}
