using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerMain : MonoBehaviour
{
    [SerializeField] private AudioMixer _audio;

    public void SetVolumen(float SliderValue)
    {
        _audio.SetFloat("MasterVolumen", Mathf.Log10(SliderValue) * 20);
    }
}
