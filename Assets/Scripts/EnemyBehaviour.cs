using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    //NavMeshAgent nvAgent;
    [SerializeField] Image hpBar;
    Transform destination;
    float health = 100;
    float maxhp = 100;
    float speed = 15;
    int pointIndex = 1;
    Transform[] checkPoints;
    public Enemy myType;
    public Text myName;
    Transform target;
    Quaternion desiredAngle;
    public float mult;
    void Start()
    {
        checkPoints = GameObject.Find("CheckPoints").GetComponentsInChildren<Transform>();
        Instantiate(myType.graphic, transform);
        // nvAgent = GetComponent<NavMeshAgent>(); NvAgent Çektik
        destination = GameObject.Find("Destination").transform; //Destination isimli objeyi çektik
        // nvAgent.SetDestination(destination.position); NvAgent Gideceği Noktayı verdik
        maxhp = myType.hp* mult;
        health = maxhp;
        transform.localScale *= mult;
        myName.text = myType.enemyName;
        //nvAgent.speed = myType.speed;
        GetComponentInChildren<Renderer>().material.color = myType.color;
        target = checkPoints[pointIndex];
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
       // transform.rotation = Quaternion.Lerp(transform.rotation,desiredAngle,0.3f);
        if (CalculateDist() < CalculateDir().magnitude)
        {
            transform.position = target.position;
            if (pointIndex < checkPoints.Length - 1)
            {
                pointIndex++;
                target = checkPoints[pointIndex];
            }
            else
            {
                target = destination;
            }
        }
        else
        {
            transform.position += CalculateDir();
            transform.LookAt(transform.position+CalculateDir());
        }
    }
    Vector3 CalculateDir()
    {
        Vector3 dir = target.position - transform.position;
        return dir.normalized * speed * 0.01f;
    }
    float CalculateDist()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        return dist;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHp();
        if (health <= 0)
        {
            Die();
        }
    }
    void UpdateHp()
    {
        hpBar.fillAmount = health / maxhp;
    }
    void Die()
    {
        Spawner.instance.EditCoin(myType.gold);
        Destroy(gameObject);
    }
}
