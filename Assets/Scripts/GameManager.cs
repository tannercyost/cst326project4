using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] public WeaponFSM defense;
    [SerializeField] TextMeshProUGUI coinText; // text field for current coin amount
    private List<WeaponFSM> defenses;
    private void Awake()
    {
        coins = 10;
        coinText.SetText("Coins: " + coins);
    }
    private void addToPurse(int amt)
    {
        coins += amt;
        coinText.SetText("Coins: " + coins);
    }

    private void removeFromPurse(int amt)
    {
        coins -= amt;
        coinText.SetText("Coins: " + coins);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject e = hit.transform.gameObject;
                if (e.CompareTag("Location"))
                {
                    if (coins >= 10)
                    {
                        Instantiate(defense, hit.transform.position, hit.transform.rotation);
                        removeFromPurse(10);
                    }
                    else
                    {
                        Debug.Log("Insufficient coins.");
                    }
                }
                //bool death = e.GetComponent<Enemy>().Damage(1);
                //int value = e.GetComponent<Enemy>().value;
                //if (death)
                //    addToPurse(value);
            }
        }
    }
}
