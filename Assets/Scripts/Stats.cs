using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public DefaultStats defaultstats;
    public List<ScriptableAction> Actions = new List<ScriptableAction>();
    int CurrentHealth;
    public void OnEnable() {
        CurrentHealth = defaultstats.MaxHealth;
        foreach (ScriptableAction action in defaultstats.Actions) {
            Actions.Add(Instantiate(action));
        }
    }
}
