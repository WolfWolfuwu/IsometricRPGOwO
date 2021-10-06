using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MouseManager : MonoBehaviour
{
    public NavMeshAgent Agent;
    public static MouseManager instance;
    private void Awake() {
        instance = this;
    }

    public void Start() {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    public void Move() {
        Agent.SetDestination(MousePosition());
        Debug.Log(MousePosition());
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Move();
        }
    }

    public Vector2 MousePosition() {
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return MousePosition;
    }
}
