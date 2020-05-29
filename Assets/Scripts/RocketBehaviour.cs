using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    public GameObject target;
    [SerializeField] GameObject ExpFX;
    Transform targetPoint;
    public float damage;
    private void Start()
    {
        targetPoint = target.transform;
    }
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(targetPoint.transform.position + Vector3.up);
            transform.Translate(Vector3.forward);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Collider[] colls =Physics.OverlapSphere(transform.position, 5f);
            for (int i = 0; i < colls.Length; i++)
            {
                if (colls[i].tag=="Enemy")
                {
                    colls[i].gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
                }
            }
            Instantiate(ExpFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
