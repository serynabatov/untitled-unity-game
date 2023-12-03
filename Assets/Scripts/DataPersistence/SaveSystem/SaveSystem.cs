using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{

    private static SaveSystem instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one SaveSystem in the scene");
        }
        instance = this;
    }

    public static SaveSystem GetInstance()
    {
        return instance;
    }
    public void SavePosition(Vector3 position)
    {
        Debug.Log("PlayerPosition = " + position);
        PlayerPrefs.SetFloat("PlayerPosX", position.x);
        PlayerPrefs.SetFloat("PlayerPosY", position.y);
    }

    public Vector3 LoadPosition()
    {
        Vector3 position = new Vector3();
        position.x = PlayerPrefs.GetFloat("PlayerPosX", -87);
        position.y = PlayerPrefs.GetFloat("PlayerPosY", 12.5f);
        return position;
    }

}
