using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon {


    public Dagger()
    {
        proficiencyBonus = 3;
        damageDice = new DiceExpression(1, 4); //daggers do 1d4 damage
    }
}
