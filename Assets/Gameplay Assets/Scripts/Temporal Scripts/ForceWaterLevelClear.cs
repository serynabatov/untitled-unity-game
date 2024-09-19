using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceWaterLevelClear : MonoBehaviour
{
    public void ForceWaterLevelVar (bool isCleared)
    {
        int isClearedInt;
        if (isCleared )
        {
            isClearedInt = 1;
        }
        else
        {
            isClearedInt = 0;
        }

        PlayerPrefs.SetInt(GameManager.LastWaterSceneStatus, isClearedInt);
        print(PlayerPrefs.GetInt(GameManager.WaterLevelStatus));
        print(PlayerPrefs.GetInt(GameManager.LastWaterSceneStatus));
    }
}
