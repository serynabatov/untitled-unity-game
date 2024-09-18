using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Riddles
{
    Water = 0,
    Crate = 1,
    LastRiddle = 2
}

public class DebugScript : MonoBehaviour
{

    public static event Action OnSceneReset;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Vector2 _upperPosition;
    [SerializeField]
    private Vector2 _middlePosition;
    [SerializeField]
    private Vector2 _lowerPosition;

    [SerializeField]
    private Locations location;
    [SerializeField]
    private Riddles riddle;

    public void TeleportUp()
    {
        location = Locations.Upper;
    }

    public void TeleportMid()
    {
        location = Locations.Middle;
    }

    public void TeleportDown()
    {
        location = Locations.Lower;
    }

    public void ChangePositionOfPlayer()
    {
        switch (location)
        {
            case Locations.Upper:
                _player.position = _upperPosition;
                break;
            case Locations.Middle:
                _player.position = _middlePosition;
                break;
            case Locations.Lower:
                _player.position = _lowerPosition;
                break;
        }
    }

    public void SkipWaterPuzzle()
    {
        riddle = Riddles.Water;
    }

    public void SkipCratePuzzle()
    {
        riddle = Riddles.Crate;
    }

    public void SkipLastPuzzle()
    {
        riddle = Riddles.LastRiddle;
    }

    public void ForceRiddlesClear()
    {
        switch (riddle)
        {
            case Riddles.Water:
                PlayerPrefs.SetInt("Water level status", 1);
   
                break;
            case Riddles.Crate:
                PlayerPrefs.SetInt("Crate level status", 1);

                break;
            case Riddles.LastRiddle:
                PlayerPrefs.SetInt("mainVarBossFinished", 1);
                break;
        }
    }

    public void SceneReset()
    {
        OnSceneReset?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
