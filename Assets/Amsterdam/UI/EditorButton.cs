using System.Collections;
using System.Collections.Generic;
using Amsterdam.Managers;
using Amsterdam.Managers.Extentions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EditorButton : MonoBehaviour
{
    private Dropdown _dropdown;

    #region Life Cycle

    void Start()
    {
        _dropdown = GetComponent<Dropdown>();
        var totalLevelCount = GameSettings.Current.totalLevelCount;
        PopulateDropdown (_dropdown, totalLevelCount);
    }

    #endregion

    #region Select Level

    public void SelectLevel()
    {
        LevelManager.Instance.LoadLevel(_dropdown.value);
    }

    #endregion
    
    
    #region Fill Dropdown

    private void PopulateDropdown(Dropdown dropdown, int totalLevelCount)
    {
        List<string> options = new List<string>();
        for (int i = 0; i < totalLevelCount; i++)
        {
            options.Add("Level "+ i);
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    #endregion
}