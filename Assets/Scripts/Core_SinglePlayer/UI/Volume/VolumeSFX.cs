using UnityEngine;

public class VolumeSFX : BaseSlider
{
    protected override void Start()
    {
        base.Start();
        this.slider.value = SoundManager.Instance.volumeBG;
    }
    protected override void LoadSlider(float value)
    {
        base.LoadSlider(value);
        SoundManager.Instance.volumeSFX = value;
        SoundManager.Instance.SetVolumeSFX(value);
    }
}
