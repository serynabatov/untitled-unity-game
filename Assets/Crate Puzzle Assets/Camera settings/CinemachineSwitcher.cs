using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    private Animator cinemachineAnimator;

    [SerializeField]
    private GameObject player;


    void Start()
    {
        cinemachineAnimator = GetComponent<Animator>();
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        SwitchCameraStates(player.transform.position.y);
    }

    private void SwitchCameraStates(float y)
    {
        // Debug.Log("HERE");
        cinemachineAnimator.Play((y > 0 && y < 7.35f) ? "FirstLevelCamera" :
            ((y >= 7.35f && y < 21.9f) ? "SecondLevelCamera" :
            ((y >= 21.9f && y < 37f) ? "ThirdLevelCamera" :
            "Player out of Camera reach")));

    }
}
