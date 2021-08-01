using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Prefabs.Resources.Levels.Base;
using Amsterdam.Managers;
using Cinemachine;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Transform rayObj;
    public LayerMask layermask;
    public float speed;
    public bool lastGrounded;
    public Transform leftBehindPos;
    private CinemachineStateDrivenCamera _stateDriven;
    private bool grounded;
    private Rigidbody _rbPlayer;
    public List<GameObject> picks = new List<GameObject>();

    private Transform parentPickup;
    private float timer = 0f;


    [SerializeField] private Transform stackPosition;


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
        if (LevelManager.Instance.CurrentLevel.state == Level.State.Started)
        {
            RunInfinite();
            CheckIfGround();

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

    private void CheckIfGround()
    {
        grounded = (Physics.Raycast(rayObj.transform.position, Vector3.down, 1000f, layermask));
        Vector3 forward = rayObj.transform.TransformDirection(Vector3.down) * 1;
        Debug.DrawRay(rayObj.transform.position, forward, Color.red);

        if (!grounded)

        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                LeftBehind();
                timer = 0f;
            }
        }

        if (grounded != lastGrounded)
        {
            if (grounded)
            {
                Debug.Log("Grounded one!!");
            }
            else
            {
                Debug.Log("not grounded one !!");
            }
        }

        lastGrounded = grounded;
    }

    private void LeftBehind()
    {
        // var timeElapsed = 0f;
        // var timeDur = 1f;

        // while (timeElapsed < timeDur)
        // {
        Debug.Log("in coroitine ");


        picks.ToList().Last().gameObject.transform.position = leftBehindPos.position;
        picks.ToList().Last().transform.parent = null;
        picks.Remove(picks.ToList().Last());
        Debug.Log("in for ");


        // }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            Transform otherTransform = other.transform.parent;
            Rigidbody otherRB = otherTransform.GetComponent<Rigidbody>();

            otherRB.isKinematic = true;
            other.enabled = false;
            if (parentPickup == null)
            {
                parentPickup = otherTransform;
                parentPickup.position = stackPosition.position;
                parentPickup.parent = stackPosition;

                picks.Add(otherTransform.gameObject);
            }
            else
            {
                parentPickup.position += Vector3.up * (otherTransform.localScale.y);
                otherTransform.position = stackPosition.position;
                otherTransform.parent = parentPickup;

                picks.Add(otherTransform.gameObject);
            }
        }
    }
}