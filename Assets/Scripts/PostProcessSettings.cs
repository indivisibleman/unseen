using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessSettings : MonoBehaviour
{
    PostProcessVolume volume;
    bool isActive = true;

    void Awake() {
        volume = GetComponentInParent<PostProcessVolume>();
    }
    
    public void Change() {
        AutostereogramNoise autostereogramNoise;
        volume.profile.TryGetSettings(out autostereogramNoise);
        autostereogramNoise.hue.value = Random.Range(0f, 1f);
        autostereogramNoise.saturation.value = Random.Range(0.3f, 0.9f);
    }

    internal void Reveal()
    {
        AutostereogramNoise autostereogramNoise;
        volume.profile.TryGetSettings(out autostereogramNoise);
        isActive = !isActive;
        autostereogramNoise.active = isActive;
    }
}
