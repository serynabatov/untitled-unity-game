using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{

    public static void SavePosition(Vector3 position)
    {
        Debug.Log("PlayerPosition = " + position);
        Debug.Log($"Current scene = {SceneManager.GetActiveScene().name}");
        switch (SceneManager.GetActiveScene().name)
        {
            case "Gameplay":
                PlayerPrefs.SetFloat("PlayerPosXGameplay", position.x);
                PlayerPrefs.SetFloat("PlayerPosYGameplay", position.y);
                break;
            case "Crate Puzzle":
                PlayerPrefs.SetFloat("PlayerPosXCrate", position.x);
                PlayerPrefs.SetFloat("PlayerPosYCrate", position.y);
                break;
            default:
                Debug.LogWarning("Something wrong with scene name here to Save " + SceneManager.GetActiveScene().name);
                break;
        }
    }

    public static Vector3 LoadPosition()
    {
        Vector3 position = new Vector3();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Gameplay":
                position.x = PlayerPrefs.GetFloat("PlayerPosXGameplay", -87);
                position.y = PlayerPrefs.GetFloat("PlayerPosYGameplay", 12.5f);
                break;
            case "Crate Puzzle":
                position.x = PlayerPrefs.GetFloat("PlayerPosXCrate", 0);
                position.y = PlayerPrefs.GetFloat("PlayerPosYCrate", -5f); //-5f default (24.81f - это не дефолтный для прохождения 3 уровня)
                break;
            default:
                Debug.LogWarning("Something wrong with scene name here to Load " + SceneManager.GetActiveScene().name);
                break;
        }
        return position;
    }

}
