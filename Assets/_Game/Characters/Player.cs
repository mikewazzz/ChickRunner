using System.Collections;
using System.Collections.Generic;
using Amsterdam.Managers;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelManager.Instance.CurrentLevel.StartCurrentLevel();
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LevelManager.Instance.CurrentLevel.SuccessCurrentLevel();
        }
        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            LevelManager.Instance.CurrentLevel.FailCurrentLevel();
        }
    }
}
