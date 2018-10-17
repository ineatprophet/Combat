using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// root class for action heirarchy
/// </summary>
/// 

[System.Serializable]
public class BaseAction {

    public enum AEconomy { Standard, Move, Minor, Free, ImmediateInterrupt, ImmediateResponse, None, Uninitialized }; // Types of action economy (i.e. time required for the action to take place)
    public AEconomy actionEconomy = AEconomy.Uninitialized; // initialize to no economy type 
    public string actionName = "No Name Yet";


    public virtual void MenuMouseOver()
    {
        //Display things like attack range, tooltip, etc.
    }

    public virtual void MenuClick(Unit unit)
    {
        //Begin the process of taking the action.
    }
}
