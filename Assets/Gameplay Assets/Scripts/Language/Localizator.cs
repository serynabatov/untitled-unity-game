using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Localizator : MonoBehaviour
{
    public string iD;
    private Regex reg;
    private List<string[]> rows = new List<string[]>();

    void Awake()
    {
        reg = new Regex(";");
        TextAsset txtData = (TextAsset)Resources.Load("localization");
        string localTxt = txtData.text;
        string[] rows = localTxt.Split('\n');
        for (int i = 1; i < rows.Length; i++)
        {
            string[] highlightedRow = reg.Split(rows[i]);
            this.rows.Add(highlightedRow);
        }
    }
    void Update()
    {
        if (PlayerPrefs.HasKey("GameLanguage"))
        {
            string gameLanguage = PlayerPrefs.GetString("GameLanguage");
            ChangeText(LocalizedText(iD, gameLanguage));
        }
        else
        {
            ChangeText(LocalizedText(iD, "EN"));
        }
    }

    private void ChangeText(string newText)
    {
        GetComponent<TMPro.TMP_Text>().text = newText;
    }

    private string LocalizedText(string iD, string language)
    {
        for (int i = 0; i < this.rows.Count; i++)
        {
            string[] highlightedRow = this.rows[i];
            if (iD == highlightedRow[0])
            {
                if (language == "EN")
                {
                    return highlightedRow[1];
                }
                else if (language == "RU")
                {
                    return highlightedRow[2];
                }
                break;
            }
        }
        return "No translation";
    }
}
