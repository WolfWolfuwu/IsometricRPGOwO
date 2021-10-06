using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterCon : MonoBehaviour
{
    private Stats stats;
    private NavMeshAgent Agent;
    public List<Vector3> points = new List<Vector3>();
    private float DistanceFromPlayer;
    private void OnEnable() {
        stats = GetComponent<Stats>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            stats.Actions[0].CallMethod(gameObject);
        }
        if(points.Count ==1) {
            Agent.stoppingDistance = DistanceFromPlayer;
            Agent.SetDestination(points[0]);
            points.Clear();
        }
        else {
            if(points.Count == 2) {
                Agent.stoppingDistance = 0;
                Agent.SetDestination(points[0]);
                if(Vector3.Distance(transform.position,points[0]) < 0.1f) {
                    points.Remove(points[0]);
                }
            }
            if(points.Count > 2) {
                points.Clear();
            }
        }
    }

    public void SetDestination(Vector3 destination) {
        Agent.SetDestination(destination);
    }

    public void SetPath(NavMeshPath path) {
        Agent.SetPath(path);
    }

    public void AddPoint(Vector3 point) {
        points.Add(point);
        Agent.SetDestination(points[0]);
    }

    public void SetDistanceFromPlayer(float distance) {
        DistanceFromPlayer = distance;
    }

}
