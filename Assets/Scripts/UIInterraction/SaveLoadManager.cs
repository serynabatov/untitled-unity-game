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

    void LoadOnClick(string fileName)
    {
        DataPersistenceManager.Instance.LoadGame(fileName);
    }

    void DeleteOnClick(string fileName)
    {
        DataPersistenceManager.Instance.DeleteGame(fileName);
    }
}