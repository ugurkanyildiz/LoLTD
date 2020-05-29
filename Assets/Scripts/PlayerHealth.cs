using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health=100;
    [SerializeField] Text hpText;
    [SerializeField] GameObject deathPanel;
    private void Start()
    {
        hpText.text = "" + health;
    }
    private void OnTriggerEnter(Collider other)
    {
        health -= 5;
        hpText.text = "" + health;
        if (health<=0)
        {
            Lose();
        }
        Destroy(other.gameObject);
    }
    void Lose()
    {
        deathPanel.SetActive(true);
    }

}
