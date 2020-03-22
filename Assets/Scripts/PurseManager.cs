using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurseManager : MonoBehaviour
{
    int coins = 0;

    public void addToPurse(int amt)
    {
        coins += amt;
    }

    public void removeFromPurse(int amt)
    {
        coins -= amt;
    }
}
