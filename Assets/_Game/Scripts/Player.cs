using System.Collections;
using System.Collections.Generic;
using _Game.Prefabs.Resources.Levels.Base;
using Amsterdam.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    
    void Start()
    {
        InputManager.Instance._player = this;
    }
    
    void Update()
    {
        if (LevelManager.Instance.CurrentLevel.state  == Level.State.Started)
        {
          
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("first  touch");
    }

    public void OnPointerDrag(PointerEventData eventData)
    {
        Debug.Log("first  drag");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("last  touch");
    }
}
