using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehavior : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;

    // Start is called before the first frame update
    void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        Boulder.OnBoulderEnd += UnsubscribeFromEvent;

        PlayerController2D.OnTrapActivated += SetCameraNoise;
        PlayerController2D.OnDamaged += SetCameraNoise;
        PlayerController2D.OnBoulderCollision += StopCameraNoise;
        PlayerController2D.OnRespawn += StopCameraNoise;
        Boulder.OnBoulderEnd += StopCameraNoise;
    }

    private void OnDestroy()
    {
        PlayerController2D.OnTrapActivated -= SetCameraNoise;
        PlayerController2D.OnDamaged -= SetCameraNoise;
        PlayerController2D.OnBoulderCollision -= StopCameraNoise;
        PlayerController2D.OnRespawn -= StopCameraNoise;
        Boulder.OnBoulderEnd -= StopCameraNoise;
    }

    private void SetCameraNoise()
    {
        CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        basicMultiChannelPerlin.m_AmplitudeGain = 1.0f;
        basicMultiChannelPerlin.m_FrequencyGain = 2.0f;
    }

    private void StopCameraNoise() {
        CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        basicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
        basicMultiChannelPerlin.m_FrequencyGain = 0.0f;
    }

    private void UnsubscribeFromEvent()
    {
        PlayerController2D.OnTrapActivated -= SetCameraNoise;
        PlayerController2D.OnDamaged -= SetCameraNoise;
        PlayerController2D.OnBoulderCollision -= StopCameraNoise;
        PlayerController2D.OnRespawn -= StopCameraNoise;
        Boulder.OnBoulderEnd -= StopCameraNoise;
    }
}
