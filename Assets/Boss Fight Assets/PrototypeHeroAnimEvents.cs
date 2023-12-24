using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeHeroAnimEvents : MonoBehaviour
{
    // References to effect prefabs. These are set in the inspector
    [Header("Effects")]
    public GameObject m_RunStopDust;
    public GameObject m_JumpDust;
    public GameObject m_LandingDust;
    public GameObject m_DodgeDust;
    public GameObject m_WallSlideDust;
    public GameObject m_WallJumpDust;
    public GameObject m_AirSlamDust;
    public GameObject m_ParryEffect;

    private PlayerController2D m_player;
    private AudioManager_PrototypeHero m_audioManager;
    MessageBrokerImpl broker = MessageBrokerImpl.Instance;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponentInParent<PlayerController2D>();
        //m_audioManager = AudioManager_PrototypeHero.instance;
    }

    // Animation Events
    // These functions are called inside the animation files
    void AE_runStop()
    {
        broker.Publish<int>((int)AudioClipName.RunStop);
        //m_audioManager.PlaySound("RunStop");
        float dustXOffset = 0.6f;
        float dustYOffset = 0.078125f;
        m_player.SpawnDustEffect(m_RunStopDust, dustXOffset, dustYOffset);
    }

    void AE_footstep()
    {
        //Debug.Log("AE_footstep");
        broker.Publish<int>((int)AudioClipName.FootstepEffect);
    }

    void AE_Jump()
    {
        broker.Publish<int>((int)AudioClipName.Jump);
        // m_audioManager.PlaySound("Jump");
        //Debug.Log("AE_Jump");
        float dustYOffset = 0.078125f;
        m_player.SpawnDustEffect(m_JumpDust, 0.0f, dustYOffset);
    }

    void AE_Landing()
    {
        broker.Publish<int>((int)AudioClipName.Landing);
        //Debug.Log("AE_Landing");
        //m_audioManager.PlaySound("Landing");
        float dustYOffset = 0.078125f;
        m_player.SpawnDustEffect(m_LandingDust, 0.0f, dustYOffset);
    }
    void AE_Hurt()
    {
        broker.Publish<int>((int)AudioClipName.Hurt);
        //m_audioManager.PlaySound("Hurt");
    }

    void AE_Death()
    {
        broker.Publish<int>((int)AudioClipName.Death);
        //m_audioManager.PlaySound("Death");
    }
}
