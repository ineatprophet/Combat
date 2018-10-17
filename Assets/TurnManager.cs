using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class TurnManager {

    public static GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("AllUnits");

    public static GameObject currentPlayer;
    public static int playerNum = 0;
    public static Dictionary<GameObject,int> initiativeOrder = new Dictionary<GameObject,int>();


    public static void NextPlayer() //// need to update to account for initiative order
    {
        if (currentPlayer != null) // on Start there is no current player
        {
            currentPlayer.GetComponent<Unit>().currentPath = null;
            currentPlayer.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
        playerNum++;
        
        if(playerNum >= initiativeOrder.Count)
        {
            playerNum = 0;
        }

        currentPlayer = initiativeOrder.Keys.ElementAt(playerNum);
        currentPlayer.GetComponent<Unit>().StartTurn();
        Debug.Log("Current Player : " + playerNum);
    }

    public static void SetInitiative()
    {
        var rawInitiative = new Dictionary<GameObject, int>();
        foreach(GameObject go in allPlayers)
        {
            Unit unit = go.GetComponent<Unit>();
            int initiative = (Random.Range(1, 20) + unit.myStats.initiativeBonus);
            rawInitiative.Add(go, initiative);
        }

        var sortedDict = from entry in rawInitiative orderby entry.Value descending select entry;
        foreach(KeyValuePair<GameObject,int> kvp in sortedDict)
        {
            initiativeOrder.Add(kvp.Key, kvp.Value);
        }

        foreach (KeyValuePair<GameObject, int> kvp in initiativeOrder)
        {
            Debug.Log("initativeOrder contains  Unit " + kvp.Key + " with an initiative of  " + kvp.Value);
        }

        currentPlayer = initiativeOrder.Keys.ElementAt(0);
        currentPlayer.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        currentPlayer.GetComponent<Unit>().StartTurn();
    }
}
