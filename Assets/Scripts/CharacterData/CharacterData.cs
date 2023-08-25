using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScripatbleObject
{
    public string NAME;
    public float HEALTH;
    public float SPEED;
    public float DAMAGE;
    public GameObject MeshBody;
}
