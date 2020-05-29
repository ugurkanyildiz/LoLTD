using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    TowerType selectedtower = null;
    [SerializeField] GameObject[] towers;
    [SerializeField] LayerMask groundMask;
    static public Controller Instance;
    public GameObject Panel;
    TowerSlot selectedSlot;
    private void Start()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
       // Debug.Log("Level Loaded" + LevelController.instance.level);
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {
                if (selectedtower != null)
                {
                    TowerSlot ts = hit.transform.gameObject.GetComponent<TowerSlot>();
                    if (ts)
                    {
                        ts.SetTower(selectedtower);
                        selectedSlot = ts;
                    }

                    selectedtower = null;
                }
                else
                {
                    TowerSlot ts = hit.transform.gameObject.GetComponent<TowerSlot>();
                    if (ts)
                    {
                        ts.activateUpgradePanel();
                        selectedSlot = ts;
                    }

                }

            }

        }
    }
    public void SelectTower(TowerType tower)
    {
        selectedtower = tower;
    }
    public void UpgradeTower()
    {
        selectedSlot.UpgradeTower();
    }
}
