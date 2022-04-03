using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    private Animator cinemachineAnimator;

    private GameObject player;


    void Start()
    {
        cinemachineAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        SwitchCameraStates(player.transform.position.y);

    }

    private void SwitchCameraStates(float y)
    {
        // Debug.Log("HERE");
        cinemachineAnimator.Play((y > 0 && y < 7.35) ? "FirstLevelCamera" :
            ((y >= 7.35 && y < 21.9) ? "SecondLevelCamera" :
            ((y >= 21.9 && y < 37) ? "ThirdLevelCamera" : 
            "Player out of Camera reach")));

    }
}
