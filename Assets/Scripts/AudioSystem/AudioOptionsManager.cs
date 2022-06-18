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


    public void OnMusicSliderValueChanged(float value)
    {
        string castedValue = ((int)(value * 100)).ToString();

        musicVolume = value;
        musicSliderText.text = castedValue;

        AudioManager.Instance.UpdateMixerVolume();
    }

    public void OnEffectSliderValueChanged(float value)
    {
        string castedValue = ((int)(value * 100)).ToString();

        soundsEffectVolume = value;
        soundEffectSliderText.text = castedValue;

        AudioManager.Instance.UpdateMixerVolume();
    }

}