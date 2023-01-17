
using UnityEngine;

/// <summary>
/// This interface <c>IDataPersistence</c> will be implemented by the class we would like to operate on
/// </summary>
public interface IDataPersistence
{
    /// <summary>
    /// This method helps to acquire the user the data from the loaded system through this interface
    /// </summary>
    /// <param name="data">Data.</param>
    void LoadData(PlayerData data);

    /// <summary>
    /// This method allows the user to save the system changing the data that is passed through the reference
    /// </summary>
    /// <param name="data">Data.</param>
    void SaveData(ref PlayerData data);
}
