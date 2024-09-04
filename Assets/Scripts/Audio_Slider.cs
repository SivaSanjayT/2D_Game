
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Slider : MonoBehaviour
{
    public Slider Volumeslider;
    public AudioSource SoundSource;

    float PreviousVolume;

    private void Start()
    {
        Volumeslider.value = SoundSource.volume;
        Volumeslider.onValueChanged.AddListener(OnVolumeChange);
    }

    public void OnVolumeChange(float Volume)
    {
        SoundSource.volume = Volume;
    }
}
