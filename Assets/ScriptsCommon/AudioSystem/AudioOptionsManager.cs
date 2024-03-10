using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class AudioOptionsManager : MonoBehaviour
{

    public static float musicVolume { get; private set; }
    public static float soundsEffectVolume { get; private set; }

    [SerializeField]
    private AudioMixerGroup musicMixerGroup;

    [SerializeField]
    private AudioMixerGroup soundEffectMixerGroup;

    [SerializeField]
    private TextMeshProUGUI musicSliderText;

    [SerializeField]
    private TextMeshProUGUI soundEffectSliderText;
    private void Awake()
    {
        if (PlayerPrefs.HasKey(Constants.preferenceAudioVolume))
        {
            musicSliderText.text = PlayerPrefs.GetString(Constants.preferenceAudioVolumeText);
        }

        if (PlayerPrefs.HasKey(Constants.preferenceSoundEffectsVolume))
        {
            soundEffectSliderText.text = PlayerPrefs.GetString(Constants.preferenceSoundEffectsVolumeText);
        }

    }

    public void OnMusicSliderValueChanged(float value)
    {
        string castedValue = ((int)(value * 100)).ToString();

        musicVolume = value;
        PlayerPrefs.SetFloat(Constants.preferenceAudioVolume, musicVolume);

        musicSliderText.text = castedValue;
        PlayerPrefs.SetString(Constants.preferenceAudioVolumeText, castedValue);

        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }

    public void OnEffectSliderValueChanged(float value)
    {
        string castedValue = ((int)(value * 100)).ToString();

        soundsEffectVolume = value;
        PlayerPrefs.SetFloat(Constants.preferenceSoundEffectsVolume, soundsEffectVolume);

        soundEffectSliderText.text = castedValue;
        PlayerPrefs.SetString(Constants.preferenceSoundEffectsVolumeText, castedValue);

        soundEffectMixerGroup.audioMixer.SetFloat("SoundsEffect", Mathf.Log10(soundsEffectVolume) * 20);
    }

    public void ResetOptions()
    {
        PlayerPrefs.DeleteKey(Constants.preferenceAudioVolume);
        OnMusicSliderValueChanged(1f);

        PlayerPrefs.DeleteKey(Constants.preferenceSoundEffectsVolume);
        OnEffectSliderValueChanged(1f);

        AudioManager.Instance.ResetSliders();
    }

}