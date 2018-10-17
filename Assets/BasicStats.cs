using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicStats {

    public int defenseAC;
    public int defenseReflex;
    public int defenseFortitude;
    public int defenseWill;

    public int hitpoints;

    public int initiativeBonus;
    public int currentInitiative;

    public int attackBonus = 7; //temporary int to test attack functionality

    // Used to track whether a unit has actions available
    public bool standardAction;
    public bool moveAction;
    public bool minorAction;
    public bool immediateAction;
}
