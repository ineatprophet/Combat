using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

    // tileX and tileY represent the correct map-tile position
    // for this piece.  Note that this doesn't necessarily mean
    // the world-space coordinates, because our map might be scaled
    // or offset or something of that nature.  Also, during movement
    // animations, we are going to be somewhere in between tiles.
    GameController gameController;
	public int tileX;
	public int tileY;
    public bool canBeTargeted = true;

    public BasicStats myStats = new BasicStats();
    public List<BaseAction> myActions = new List<BaseAction>();
    public List<GameObject> myActionButtons = new List<GameObject>();
    public Weapon equippedWeapon = new Dagger();
	public TileMap map;

	// Our pathfinding info.  Null if we have no destination ordered.
	public List<Node> currentPath = null;

	// How far this unit can move in one turn. Note that some tiles cost extra.
	int moveSpeed = 6;
	public float remainingMovement=6;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameController>();
        //initialize all actions owned by this unit1
        myActions.Add(new BasicMeleeAttack());
    }

    void Update()
    {
        CheckPathing();
    }


    void CheckPathing()
    {
        // Draw our debug line showing the pathfinding!
        if (currentPath != null)
        {
            int currNode = 0;

            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) +
                    new Vector3(0, 0, -0.5f);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y) +
                    new Vector3(0, 0, -0.5f);

                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }

        // Have we moved our visible piece close enough to the target tile that we can
        // advance to the next step in our pathfinding?
        if (Vector3.Distance(transform.position, map.TileCoordToWorldCoord(tileX, tileY)) < 0.1f)
            AdvancePathing();

        // Smoothly animate towards the correct map tile.
        transform.position = Vector3.Lerp(transform.position, map.TileCoordToWorldCoord(tileX, tileY), 5f * Time.deltaTime);
    
    }

	// Advances our pathfinding progress by one tile.
	void AdvancePathing() {
		if(currentPath==null)
			return;

		if(remainingMovement <= 0)
			return;

		// Teleport us to our correct "current" position, in case we
		// haven't finished the animation yet.
		transform.position = map.TileCoordToWorldCoord( tileX, tileY );

		// Get cost from current tile to next tile
		remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y );
		
		// Move us to the next tile in the sequence
		tileX = currentPath[1].x;
		tileY = currentPath[1].y;
		
		// Remove the old "current" tile from the pathfinding list
		currentPath.RemoveAt(0);
		
		if(currentPath.Count == 1) {
			// We only have one tile left in the path, and that tile MUST be our ultimate
			// destination -- and we are standing on it!
			// So let's just clear our pathfinding info.
			currentPath = null;
		}
	}

    public void StartTurn()
    {
        remainingMovement = moveSpeed;
        myStats.immediateAction = true;
        myStats.standardAction = true;
        myStats.minorAction = true;
        myStats.moveAction = true;
        this.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        BuildActionMenu();
        //gameController.attackButton.onClick.AddListener(() => { this.myActions[0].MenuClick(); });
    }

    public void EndTurn()
    {
        this.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.white;

        //gameController.attackButton.onClick.RemoveAllListeners();
    }   

    public void SampleFunction()
    {
        Debug.Log("Sample called");
    }

    public void BuildActionMenu()
    {
        foreach(BaseAction a in myActions)
        {
            GameObject button = Instantiate(gameController.actionButtonPrefab);
            button.transform.SetParent(gameController.actionPanel.transform);
            myActionButtons.Add(button);
            //build a button with mouseover and click actions 
            button.GetComponentInChildren<Text>().text = a.actionName;
            button.GetComponent<Button>().onClick.AddListener(() => { a.MenuClick(this); });
        }
    }
}
