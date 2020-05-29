using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField]GameObject starParent;
    [SerializeField] Sprite yellowStar;
    public void SetStarLevel(float hp)
    {
        int starAmount = (int)hp / 20;
        for (int i = 0; i < starAmount; i++)
        {
            starParent.transform.GetChild(i).GetComponent<Image>().sprite=yellowStar;
        }


    }

}
