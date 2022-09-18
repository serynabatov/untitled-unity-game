using UnityEngine;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    public Button loadButton, deleteButton;

    void Start()
    {
        loadButton.onClick.AddListener(() => LoadOnClick());
        deleteButton.onClick.AddListener(() => DeleteOnClick());
    }

    void LoadOnClick(string fileName)
    {
        DataPersistenceManager.Instance.LoadGame(fileName);
    }

    void DeleteOnClick(string fileName)
    {
        // TODO: make the deletion
        DataPersistenceManager.Instance.DeleteGame(fileName);
    }
}