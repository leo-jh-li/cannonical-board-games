using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	ACTIVE,
	GAMEOVER
}

public abstract class Game : MonoBehaviour
{
	public UIManager uiManager;
	public BoardManager board;

	[SerializeField]
	protected GameObject player;
	[SerializeField]
	protected float gameAreaLength;
	[SerializeField]
	protected float cannonHeight;
	[SerializeField]
	protected int turnsPerReposition;
	private int turnsSinceReposition;
	[SerializeField]
	private float waitAfterShot;
	[SerializeField]
	protected GameObject projectileBlue; // Player 1 aka X
	[SerializeField]
	protected GameObject projectileRed; // Player 2 aka O
	[SerializeField]
	protected Material materialBlue;
	[SerializeField]
	protected Material materialRed;

	[SerializeField]
	protected Vector3 cameraOffset;
	[SerializeField]
	protected Vector3 cameraRotation;

	protected CameraBehaviour cam;
	protected GameState gameState;
	protected List<CannonController> cannons;

	protected void Start()
	{
		cam = Camera.main.GetComponent<CameraBehaviour>();
		cannons = new List<CannonController>();

		//TODO: change/move elsewhere
		StartGame();
	}

	//TODO: for testing
	//protected void Update()
	//{
	//	if (Input.GetKeyDown(KeyCode.R))
	//	{
	//		GetComponent<TGame>().RestartGame();
	//	}
	//}

	protected void StartGame()
	{
		InitializeGame();
		gameState = GameState.ACTIVE;
		StartCoroutine(ProcessTurn());
		uiManager.UpdateChargeBar(0);
	}

	protected virtual void InitializeGame()
	{
		Player player1 = Instantiate(player, transform.position + new Vector3(0, cannonHeight, -gameAreaLength), Quaternion.identity).GetComponent<Player>();
		player1.playerMaterial = materialBlue;
		CannonController cannon1 = player1.GetComponentInChildren<CannonController>();
		//TODO:temp
		cannon1.GetComponentInChildren<MeshRenderer>().material = materialBlue;
		cannon1.player = player1;
		cannon1.projectilePrefab = projectileBlue;
		cannon1.uiManager = uiManager;
		cannons.Add(cannon1);

		Player player2 = Instantiate(player, transform.position + new Vector3(0, cannonHeight, gameAreaLength), Quaternion.Euler(new Vector3(0, 180, 0))).GetComponent<Player>();
		player2.playerMaterial = materialRed;
		CannonController cannon2 = player2.GetComponentInChildren<CannonController>();
		//TODO:temp
		cannon2.GetComponentInChildren<MeshRenderer>().material = materialRed;
		cannon2.player = player2;
		cannon2.projectilePrefab = projectileRed;
		cannon2.uiManager = uiManager;
		cannons.Add(cannon2);
	}

	protected IEnumerator ProcessTurn()
	{
		while (gameState == GameState.ACTIVE)
		{
			foreach (CannonController cannon in cannons)
			{
				cannon.cannonState = CannonState.ACTIVE;
				cam.MoveBehindObject(cannon.transform.parent, cameraOffset, cameraRotation);
				//TODO: UI saying whose turn it is?
				while (gameState == GameState.ACTIVE && cannon.cannonState == CannonState.ACTIVE)
				{
					yield return null;
				}
				//TODO: change?
				yield return new WaitForSeconds(waitAfterShot);
				turnsSinceReposition++;
				if (gameState == GameState.ACTIVE)
				{
					uiManager.UpdateChargeBar(0);
				}
				else
				{
					// End game after winning move
					break;
				}
			}
			// TODO: wait for shot to finish first? (when the ball gets destroyed, send msg to owner to EndTurn)
			if (gameState == GameState.ACTIVE && turnsSinceReposition >= turnsPerReposition)
			{
				board.SetBoardDestination();
				turnsSinceReposition = 0;
			}
		}
	}

	public abstract void EndGame(Player winner);
}
