using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PurseManager : MonoBehaviour
{
    private int coins = 0;
    [SerializeField] TextMeshProUGUI coinText; // text field for current coin amount

    private void Awake()
    {
        coinText.SetText("Coins: 0");
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
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                GameObject e = hit.transform.parent.gameObject;
                bool death = e.GetComponent<Enemy>().Damage(1);
                int value = e.GetComponent<Enemy>().value;
                if (death)
                    addToPurse(value);
            }
        }
    }
}
