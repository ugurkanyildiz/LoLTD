using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Create Enemy Type")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public float hp;
    public float speed;
    public Color color;
    public float gold;
    public GameObject graphic;

}
