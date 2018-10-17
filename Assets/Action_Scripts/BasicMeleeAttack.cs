using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BasicMeleeAttack : AttackAction {

    public BasicMeleeAttack()
    {
        isBasicAttack = true;
        actionName = "Basic Melee Attack";
        actionEconomy = AEconomy.Standard;
    }

    public override void Attack(GameObject attacker, GameObject target)
    {
        //for now, assume the attack is valid, in range, etc.
        int attackBonus = attacker.GetComponent<Unit>().myStats.attackBonus;

        if(Random.Range(1,20) + attackBonus >= target.GetComponent<Unit>().myStats.defenseAC)
        {
            Debug.Log("Attack Hits");
            target.GetComponent<Unit>().myStats.hitpoints -= attacker.GetComponent<Unit>().equippedWeapon.damageDice.Roll();
        }
    }

    public override void MenuClick(Unit unit)
    {
        //base.MenuClick();
        Debug.Log("The attack button was clicked by " + unit.name);

        //time to select a target
        WaitForTargetSelect();
    }

    public IEnumerator WaitForTargetSelect() 
    {
        Debug.Log("Waiting");

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.gameObject.tag == "AllUnits")
                    {
                        //testTransform = hit.transform;
                        //ExitState();
                        Debug.Log("I hit you");

                        //currentState = State.Walking;
                        //EnterState();
                        yield break;
                    }
                }
            }
            yield return null;
        }
        
    }

}
