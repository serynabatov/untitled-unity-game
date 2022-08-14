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

    private PrototypeHero m_player;
    private AudioManager_PrototypeHero m_audioManager;
    MessageBrokerImpl broker = MessageBrokerImpl.Instance;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponentInParent<PrototypeHero>();
        //m_audioManager = AudioManager_PrototypeHero.instance;
    }

    // Animation Events
    // These functions are called inside the animation files
    void AE_resetDodge()
    {
        m_player.ResetDodging();
        float dustXOffset = 0.6f;
        float dustYOffset = 0.078125f;
        m_player.SpawnDustEffect(m_RunStopDust, dustXOffset, dustYOffset);
    }

    void AE_setPositionToClimbPosition()
    {
        m_player.SetPositionToClimbPosition();
    }

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
        broker.Publish<int>((int)AudioClipName.FootstepEffect);
    }

    void AE_Jump()
    {
        broker.Publish<int>((int)AudioClipName.Jump);
        // m_audioManager.PlaySound("Jump");

        if (!m_player.IsWallSliding())
        {
            float dustYOffset = 0.078125f;
            m_player.SpawnDustEffect(m_JumpDust, 0.0f, dustYOffset);
        }
        else
        {
            m_player.SpawnDustEffect(m_WallJumpDust);
        }
    }

    void AE_Landing()
    {
        broker.Publish<int>((int)AudioClipName.Landing);
        //m_audioManager.PlaySound("Landing");
        float dustYOffset = 0.078125f;
        m_player.SpawnDustEffect(m_LandingDust, 0.0f, dustYOffset);
    }

    void AE_Parry()
    {
        broker.Publish<int>((int)AudioClipName.Parry);
        //m_audioManager.PlaySound("Parry");
        float xOffset = 0.1875f;
        float yOffset = 0.25f;
        m_player.SpawnDustEffect(m_ParryEffect, xOffset, yOffset);
        m_player.DisableMovement(0.5f);
    }

    void AE_ParryStance()
    {
        broker.Publish<int>((int)AudioClipName.DrawSword);
        // m_audioManager.PlaySound("DrawSword");
    }

    void AE_AttackAirSlam()
    {
        broker.Publish<int>((int)AudioClipName.DrawSword);
        //m_audioManager.PlaySound("DrawSword");
    }

    void AE_AttackAirLanding()
    {
        broker.Publish<int>((int)AudioClipName.AirSlamLanding);
        //m_audioManager.PlaySound("AirSlamLanding");
        float dustYOffset = 0.078125f;
        m_player.SpawnDustEffect(m_AirSlamDust, 0.0f, dustYOffset);
        m_player.DisableMovement(0.5f);
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

    void AE_SwordAttack()
    {
        broker.Publish<int>((int)AudioClipName.SwordAttack);
        //m_audioManager.PlaySound("SwordAttack");
    }

    void AE_SheathSword()
    {
        broker.Publish<int>((int)AudioClipName.SheathSword);
        //m_audioManager.PlaySound("SheathSword");
    }

    void AE_Dodge()
    {
        broker.Publish<int>((int)AudioClipName.DodgeEffect);
        float dustYOffset = 0.078125f;
        m_player.SpawnDustEffect(m_DodgeDust, 0.0f, dustYOffset);
    }

    void AE_WallSlide()
    {
        //m_audioManager.GetComponent<AudioSource>().loop = true;
        if (!m_audioManager.IsPlaying("WallSlide"))
        {
            m_audioManager.PlaySound("WallSlide");
        }
        float dustXOffset = 0.25f;
        float dustYOffset = 0.25f;
        m_player.SpawnDustEffect(m_WallSlideDust, dustXOffset, dustYOffset);
    }

    void AE_LedgeGrab()
    {
        broker.Publish<int>((int)AudioClipName.LedgeGrab);
        //m_audioManager.PlaySound("LedgeGrab");
    }

    void AE_LedgeClimb()
    {
        broker.Publish<int>((int)AudioClipName.RunStop);
        //m_audioManager.PlaySound("RunStop");
    }
}
