using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSave : MonoBehaviour
{
    public void FullReset()
    {
        PlayerPrefs.DeleteAll();
        SceneSystem.GetInstance().LoadThisLevel("MainMenu");
    }
}
