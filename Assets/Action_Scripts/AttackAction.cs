using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction {

    public bool isBasicAttack = false;

    

   public virtual void Attack(GameObject attacker, GameObject target)
    {
        Debug.Log("Generic Attack function");
    }

    public AttackAction()
    {
        actionName = "Generic Attack Name";
    }
}
