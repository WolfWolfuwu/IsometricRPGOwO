using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class DefaultStats : ScriptableObject
{
    public int MaxHealth;
    public List<ScriptableAction> Actions;
}
