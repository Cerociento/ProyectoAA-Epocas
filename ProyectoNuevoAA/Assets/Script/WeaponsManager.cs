using UnityEngine;
using System.Collections.Generic;

public class WeaponsManager : MonoBehaviour {

    [SerializeField]
    GameObject[] weaponsActive = new GameObject[3];
    [SerializeField]
    List<GameObject> weapons;
    int weaponsBackpack = 1;
    float scroll;

    void Update ()
    {
        SelectWeapond();
    }

    void SelectWeapond()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            weaponsBackpack++;
        }
        else if (scroll < 0)
        {
            weaponsBackpack--;
        }

        switch (weaponsBackpack)
        {
            case 1:
                weaponsActive[0].SetActive(true);
                weaponsActive[1].SetActive(false);
                weaponsActive[2].SetActive(false);
                break;

            case 2:
                weaponsActive[1].SetActive(true);
                weaponsActive[2].SetActive(false);
                weaponsActive[0].SetActive(false);
                break;

            case 3:
                weaponsActive[2].SetActive(true);
                weaponsActive[0].SetActive(false);
                weaponsActive[1].SetActive(false);
                break;
        }

        if (weaponsBackpack <= 0)
            weaponsBackpack = 1;
        else if (weaponsBackpack >= 4)
            weaponsBackpack = 3;
    }
}
