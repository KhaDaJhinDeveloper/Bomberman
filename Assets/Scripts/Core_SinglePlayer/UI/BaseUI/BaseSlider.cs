using UnityEngine;
using UnityEngine.UI;

public abstract class BaseSlider : MonoBehaviour
{

    protected Slider slider;
    protected virtual void Start()
    {
        LoadComponents();
        AddOnSliderValueChange();
    }
    protected virtual void LoadComponents()
    {
        this.slider = GetComponent<Slider>();
    }
    protected virtual void AddOnSliderValueChange()
    {
        this.slider.onValueChanged.AddListener(LoadSlider);
    }
    protected virtual void LoadSlider(float value)
    { 
    }
}
