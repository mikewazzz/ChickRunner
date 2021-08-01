using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Prefabs.Resources.Levels.Base;
using Amsterdam.Managers;
using UnityEngine;
using UnityEngine.AI;

public class AIbase : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform finishTarget;
    public LayerMask _layerMask;
    public bool lastGrounded;
    public bool onRoad;
    private Transform parentPickup;
    [SerializeField] private Transform stackPosition;
    public Transform leftBehindPos;

    public List<GameObject> picks = new List<GameObject>();
    private float timer = 0f;



    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (LevelManager.Instance.CurrentLevel.state == Level.State.Started)
        {
            _agent.SetDestination(finishTarget.position);
            CheckIfCround();
        }
    }

    private void CheckIfCround()
    {
        var grounded = (Physics.Raycast(gameObject.transform.position, Vector3.down, 1000f, _layerMask));
        Vector3 forward = gameObject.transform.TransformDirection(Vector3.down) * 1;

        if (!grounded)
        {
            onRoad = false;
            
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                LeftBehind();
                timer = 0f;
            }
        }
        else
        {
            onRoad = true;
        }

        {
            if (grounded != lastGrounded)
            {
                if (grounded)
                {
                }
                else
                {
                }
            }


            lastGrounded = grounded;
        }
    }

    private void LeftBehind()
    {
        picks.ToList().Last().gameObject.transform.position = leftBehindPos.position;
        picks.ToList().Last().transform.parent = null;
        picks.Remove(picks.ToList().Last());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            _agent.SetDestination(finishTarget.transform.position);
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