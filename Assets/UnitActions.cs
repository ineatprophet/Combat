using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActions {

public void BasicAttack(GameObject target)
    {
        int attackRoll = Random.Range(1, 20);
        if(attackRoll >= target.GetComponent<Unit>().myStats.defenseAC)
        {
            Debug.Log("Hey, we hit!");
            target.GetComponent<Unit>().myStats.hitpoints -= 1;
        }
        else
        {
            Debug.Log("Nope. Missed.");
        }
    }
}
