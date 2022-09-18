using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLoadManager : MonoBehaviour
{
    public Button loadButton, deleteButton;
    public TextMeshProUGUI timeText;

    void Start()
    {
        loadButton.onClick.AddListener(() => LoadOnClick(timeText.text));
        deleteButton.onClick.AddListener(() => DeleteOnClick(timeText.text));
    }

    void LoadOnClick(string timestamp)
    {
        DataPersistenceManager.Instance.LoadGame(timestamp);
    }

    void DeleteOnClick(string timestamp)
    {
        DataPersistenceManager.Instance.DeleteGame(timestamp);
    }
}