using UnityEngine;

public class VolumeBG : BaseSlider
{
    protected override void Start()
    {
        base.Start();
        this.slider.value = SoundManager.Instance.volumeBG;
    }
    protected override void LoadSlider(float value)
    {
        base.LoadSlider(value);
        SoundManager.Instance.volumeBG = value;
        SoundManager.Instance.SetVolumeBG(value);
    }
}
