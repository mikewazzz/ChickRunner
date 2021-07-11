using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Prefabs.Resources.Levels.Base;
using Amsterdam.Managers;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Rigidbody _rbPlayer;
    public float speed;
    private CinemachineStateDrivenCamera _stateDriven;
    private void Awake()
    {
       
    }

    void Start()
    {
        _stateDriven = FindObjectOfType<CinemachineStateDrivenCamera>();
        _stateDriven.Follow = this.transform;
        _stateDriven.LookAt = this.transform;
        InputManager.Instance._player = this;
        _rbPlayer = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (LevelManager.Instance.CurrentLevel.state  == Level.State.Started)
        {
            
            RunInfinite();
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                var level = LevelManager.Instance.CurrentLevel;
            }
            
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

    private void RunInfinite()
    {
        _rbPlayer.velocity = transform.forward * speed; // move local forward

    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("first  touch");
        // Debug.Log(eventData.delta.x);
    }

    public void OnPointerDrag(PointerEventData eventData)
    {
        if (eventData.delta.x <= 0)
        {
            transform.Rotate(0, 12 * Time.deltaTime * eventData.delta.x, 0, Space.Self);
            Debug.Log("first  drag");
        }
        else
        {
            transform.Rotate(0, 12 * Time.deltaTime * eventData.delta.x, 0, Space.Self);

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("last  touch");
    }
}
