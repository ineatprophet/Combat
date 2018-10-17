using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickableTile : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMap map;
    public GameObject currentOccupant = null; // Who is currently in this space

	void OnMouseUp() {
		//Debug.Log ("Click!");

		if(EventSystem.current.IsPointerOverGameObject())
			return;

		map.GeneratePathTo(TurnManager.currentPlayer, tileX, tileY);
	}

}
