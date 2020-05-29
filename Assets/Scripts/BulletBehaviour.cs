using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public GameObject target;
    public float damage;
    void Update()
    {
        if (target!=null)
        {
            transform.LookAt(target.transform.position+Vector3.up);
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
            other.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
            Destroy(gameObject); 
        }
    }
}
