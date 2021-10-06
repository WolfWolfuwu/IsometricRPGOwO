using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class MouseManager : MonoBehaviour
{
    public NavMeshAgent Agent;
    public static MouseManager instance;
    private LineRenderer linerender;
    private void Awake() {
        instance = this;
    }

    public void Start() {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        linerender = GetComponent<LineRenderer>();
    }

    public void Move() {
        Agent.SetDestination(MousePosition());
        Debug.Log(MousePosition());
    }

    private void Update() {
        if (Agent.velocity.magnitude == 0) {
            DisplayPath(CalculatePath(MousePosition()));
            if (Input.GetMouseButtonDown(0)) {
                Move();
            }
        }
        else {
            DisplayPath(Agent.path);
        }
    }

    public Vector2 MousePosition() {
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return MousePosition;
    }

    public NavMeshPath CalculatePath(Vector2 Destination) {
        NavMeshPath path = new NavMeshPath();
        Vector3 AgentPosition = new Vector3(Agent.transform.position.x, Agent.transform.position.y,0);
        NavMesh.CalculatePath(AgentPosition, Destination,NavMesh.AllAreas, path);
        return path;
    }

    private void DisplayPath(NavMeshPath path) {
        int i = 1;
        linerender.positionCount = path.corners.Length;
        while (i < path.corners.Length) {
            List<Vector3> point = path.corners.ToList();
            for (int j = 0; j < point.Count; j++) {
                linerender.SetPosition(j, point[j]);
            }
            i++;
        }
    }
}
