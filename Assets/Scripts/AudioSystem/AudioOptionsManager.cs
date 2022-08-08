using UnityEngine;
using TMPro;

public class AudioOptionsManager : MonoBehaviour
{

    public static float musicVolume { get; private set; }
    public static float soundsEffectVolume { get; private set; }

    [SerializeField]
    private TextMeshProUGUI musicSliderText;

    [SerializeField]
    private TextMeshProUGUI soundEffectSliderText;
    private void Awake()
    {
        if (PlayerPrefs.HasKey(Constants.preferenceAudioVolumeText))
        {
            musicSliderText.text = PlayerPrefs.GetString(Constants.preferenceAudioVolumeText);
        }

        if (PlayerPrefs.HasKey(Constants.preferenceSoundEffectsVolume))
        {
            soundEffectSliderText.text = PlayerPrefs.GetString(Constants.preferenceSoundEffectsVolumeText);
        }

    }

    /*
        public static void Initialize()
        {
            if (PlayerPrefs.HasKey(Constants.preferenceAudioVolumeText))
            {
                musicVolume = PlayerPrefs.GetFloat(Constants.preferenceAudioVolume);
            }

            if (PlayerPrefs.HasKey(Constants.preferenceSoundEffectsVolume))
            {
                soundsEffectVolume = PlayerPrefs.GetFloat(Constants.preferenceSoundEffectsVolume);
            }
        } */

    public void OnMusicSliderValueChanged(float value)
    {
        // Initialize();

        string castedValue = ((int)(value * 100)).ToString();

        musicVolume = value;
        PlayerPrefs.SetFloat(Constants.preferenceAudioVolume, musicVolume);

        musicSliderText.text = castedValue;
        PlayerPrefs.SetString(Constants.preferenceAudioVolumeText, castedValue);

        AudioManager.Instance.UpdateMusicMixerVolume();
    }

    public void OnEffectSliderValueChanged(float value)
    {
        // Initialize();

        string castedValue = ((int)(value * 100)).ToString();

        soundsEffectVolume = value;
        PlayerPrefs.SetFloat(Constants.preferenceSoundEffectsVolume, soundsEffectVolume);

        soundEffectSliderText.text = castedValue;
        PlayerPrefs.SetString(Constants.preferenceSoundEffectsVolumeText, castedValue);

        AudioManager.Instance.UpdateSoundsEffectMixerVolume();
    }

}