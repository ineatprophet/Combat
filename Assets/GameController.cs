using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject actionPanel;
    public GameObject actionButtonPrefab;

	public void NextTurn()
    {
        foreach (Transform childTransform in actionPanel.transform)
        {
            Destroy(childTransform.gameObject);
        }
        TurnManager.NextPlayer();
    }

    private void Start()
    {
        TurnManager.SetInitiative();
    }
}
