using UnityEngine;
using UnityEngine.UI;

public class BossHpslider : MonoBehaviour
{
    public Slider sourceSlider;
    public Slider targetSlider;

    private void Start()
    {
        if (sourceSlider != null && targetSlider != null)
        {
            
            SyncSliders();
        }
    }

    
    public void SyncSliders()
    {
        if (sourceSlider != null && targetSlider != null)
        {
            targetSlider.maxValue = sourceSlider.maxValue;
            targetSlider.value = sourceSlider.value;
        }
    }
}
