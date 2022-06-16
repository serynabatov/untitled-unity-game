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


    public void OnSliderValueChanged(float value, AudioType audioType)
    {
        string castedValue = ((int)(value * 100)).ToString();

        switch (audioType)
        {
            case AudioType.SoundEffect:
                soundsEffectVolume = value;
                soundEffectSliderText.value = castedValue;
                break;

            case AudioType.MainMusic:
                musicVolume = value;
                musicSliderText.value = castedValue;
                break;
        }

        AudioManager.Instance.UpdateMixerVolume();
    }
}