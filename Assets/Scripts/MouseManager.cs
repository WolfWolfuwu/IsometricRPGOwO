using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class MouseManager : MonoBehaviour
{
    public NavMeshAgent Player;
    public PartyManager PartyManager;
    public static MouseManager instance;
    private LineRenderer linerender;
    private void Awake() {
        instance = this;
    }

    public void Start() {
        linerender = GetComponent<LineRenderer>();
    }

    private void Update() {
        if (Player.velocity.magnitude <= 0.1f) {
            Vector3 Destination = MousePosition();
            DisplayPath(CalculatePath(MousePosition()));
            if (Input.GetMouseButtonDown(0)) {
                NavMeshPath path = CalculatePath(MousePosition());
                Player.SetPath(path);
                int i = 1;
                foreach (GameObject partymember in PartyManager.Party) {
                    CharacterCon partyagent = partymember.GetComponent<CharacterCon>();
                    if(Vector3.Distance(Player.transform.position,MousePosition()) > 4) {
                        partyagent.AddPoint(Player.transform.position);
                    }
                    partyagent.AddPoint(MousePosition());
                    partyagent.SetDistanceFromPlayer(i);
                    i++;
                }
            }
        }
        else {
            DisplayPath(Player.path);
        }
    }

    public Vector2 MousePosition() {
        Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return MousePosition;
    }

    public NavMeshPath CalculatePath(Vector2 Destination) {
        NavMeshPath path = new NavMeshPath();
        Vector3 AgentPosition = new Vector3(Player.transform.position.x, Player.transform.position.y,0);
        NavMesh.CalculatePath(AgentPosition, Destination,NavMesh.AllAreas, path);
        return path;
    }

    public Quaternion PointAtMouse(Vector3 position) {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
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

    Vector3 FindPointAlongPath(Vector3[] path, float distanceToTravel) {
        if (distanceToTravel < 0) {
            return path[0];
        }

        //Loop Through Each Corner in Path
        for (int i = 0; i < path.Length - 1; i++) {
            //If the distance between the next to points is less than the distance you have left to travel
            if (distanceToTravel <= Vector3.Distance(path[i], path[i + 1])) {
                //Calculate the point that is the correct distance between the two points and return it
                Vector3 directionToTravel = path[i + 1] - path[i];
                directionToTravel.Normalize();
                return (path[i] + (directionToTravel * distanceToTravel));
            }
            else {
                //otherwise subtract the distance between those 2 points from the distance left to travel
                distanceToTravel -= Vector3.Distance(path[i], path[i + 1]);
            }
        }

        //if the distance to travel is greater than the distance of the path, return the final point
        return path[path.Length - 1];
    }

    public Vector3 ReducePathByAmount(Vector3[] path, float offset) {
        for (int i = path.Length - 1; i > 0; i--) {
            float distance = Vector3.Distance(path[i],path[i-1]);
            if (distance < offset) {
                offset -= distance;
            }
            else {
                Vector3 directionToTravel = path[i] - path[i-1];
                Vector3 finalDirection = directionToTravel + directionToTravel.normalized * offset;
                return (path[i] - (finalDirection));
            }
        }

        return path[path.Length - 1];
    }
}
