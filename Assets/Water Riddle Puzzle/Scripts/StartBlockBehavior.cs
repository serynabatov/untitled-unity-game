using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlockBehavior : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartAnimation()
    {
        _animator.Play("Start");
    }
}
