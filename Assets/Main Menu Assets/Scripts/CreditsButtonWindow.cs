using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtonWindow : MonoBehaviour
{
    Animator buttonBack;
    bool setActive = true;
    public void Start()
    {
       // buttonBack = GetComponent<Animator>();

        transform.localScale = Vector2.zero;
    }

    private void Update()
    {
        if (gameObject == enabled)
        {
            if (setActive)
            {
                Open();
                setActive = false;
            }

        }
        else
        {
            if (!setActive)
            {
                setActive = true;
                Close();
            }

        }
    }
    // Update is called once per frame

    public void Open()
    {
        transform.LeanScale(Vector2.one, 0.8f);
    }

    public void Close()
    {
        transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
    }

}
