using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSlot : MonoBehaviour
{
    GameObject builtTower = null;
    TowerType slottedTowerType;
    int towerIndex = 0;
    public bool SetTower(TowerType tower)
    {


        if (builtTower == null)
        {
            slottedTowerType = tower;
            if (Spawner.instance.byteCoin < slottedTowerType.buildingPrice) return false;
            GameObject insTower = Instantiate(slottedTowerType.level1Graphic, transform);
            builtTower = insTower;
            Spawner.instance.EditCoin(-slottedTowerType.buildingPrice);
            towerIndex++;
            return true;
        }
        else
        {
            activateUpgradePanel();
        }
        return false;
    }
    public void activateUpgradePanel()
    {
        if (builtTower == null) return;
        GameObject panel = Controller.Instance.Panel;
        panel.SetActive(true);
        panel.transform.GetChild(0).GetComponent<Text>().text=slottedTowerType.towerName;
        panel.transform.GetChild(1).GetComponent<Text>().text = slottedTowerType.description;
        if(towerIndex == 1)
            panel.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = ""+ slottedTowerType.level2Price;
        if (towerIndex == 2)
            panel.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text =""+ slottedTowerType.level3Price;
    }
    public void UpgradeTower()
    {
        if (builtTower == null) return;

        if (towerIndex == 1)
        {
            if (Spawner.instance.byteCoin >= slottedTowerType.level2Price)
            {

                Spawner.instance.EditCoin(-slottedTowerType.level2Price);
                Destroy(builtTower);
                GameObject insTower = Instantiate(slottedTowerType.level2Graphic, transform);
                builtTower = insTower;
                towerIndex++;
            }
        }
        else if (towerIndex == 2)
        {
            if (Spawner.instance.byteCoin >= slottedTowerType.level3Price)
            {
                Spawner.instance.EditCoin(-slottedTowerType.level3Price);
                Destroy(builtTower);
                GameObject insTower = Instantiate(slottedTowerType.level3Graphic, transform);
                builtTower = insTower;
                towerIndex++;
            }
        }
    }

}
