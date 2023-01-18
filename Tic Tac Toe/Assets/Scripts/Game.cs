using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
	ACTIVE,
	GAME_OVER
}

public abstract class Game : MonoBehaviour
{
	public GameManager gameManager;
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

	protected Camera mainCam;
	[SerializeField]
	protected Vector3 cameraOffset;
	[SerializeField]
	protected Vector3 cameraRotation;
	[SerializeField]
	protected Camera c4BoardCam;

	[SerializeField]
	protected RectTransform chargeBar;
	[SerializeField]
	protected RectTransform chargeContent;


	protected CameraBehaviour camBehaviour;
	protected GameState gameState;
	protected List<CannonController> cannons;

	protected void Awake()
	{
		mainCam = Camera.main;
		camBehaviour = mainCam.GetComponent<CameraBehaviour>();
		cannons = new List<CannonController>();
	}

	//TODO: for testing
	//protected void Update()
	//{
	//	if (Input.GetKeyDown(KeyCode.R))
	//	{
	//		GetComponent<TGame>().RestartGame();
	//	}
	//}

	public void StartGame()
	{
		InitializeGame();
		SetupLayout();
		gameState = GameState.ACTIVE;
		StartCoroutine(ProcessTurn());
	}

	protected void ResetLayout() {
		// Reset to one main camera
		mainCam.gameObject.SetActive(true);
		mainCam.rect = new Rect(0, 0, 1, 1);
		c4BoardCam.gameObject.SetActive(false);
		// Set charge bar to its original position and reset its charge to 0
		chargeContent.localPosition = gameManager.chargeContentDefaultPos;
		uiManager.UpdateChargeBar(0);
	}

	// Setup UI and cameras for this game
	protected virtual void SetupLayout() {
		ResetLayout();
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
		cannon1.game = this;
		cannon1.uiManager = uiManager;
		cannons.Add(cannon1);
		cannons[0].player.playerType = PlayerType.ONE;

		Player player2 = Instantiate(player, transform.position + new Vector3(0, cannonHeight, gameAreaLength), Quaternion.Euler(new Vector3(0, 180, 0))).GetComponent<Player>();
		player2.playerMaterial = materialRed;
		CannonController cannon2 = player2.GetComponentInChildren<CannonController>();
		//TODO:temp
		cannon2.GetComponentInChildren<MeshRenderer>().material = materialRed;
		cannon2.player = player2;
		cannon2.projectilePrefab = projectileRed;
		cannon2.game = this;
		cannon2.uiManager = uiManager;
		cannons.Add(cannon2);
		cannons[1].player.playerType = PlayerType.TWO;
	}

	protected void CleanUpGame() {
		uiManager.EraseGameOver();
		foreach (CannonController cannon in cannons)
		{
			Destroy(cannon.transform.parent.gameObject);
		}
		cannons.Clear();
		board.ResetBoard();
	}

	public void RestartGame()
	{
		CleanUpGame();
		Debug.Log("restarted");
		StartGame();
	}

	public void ExitGame() {
		CleanUpGame();
		gameManager.ReturnToMenu();
	}

	public void EndGame(Player winner) {
		gameState = GameState.GAME_OVER;
		GetComponent<GameOverController>().enabled = true;
		Debug.Log("GAME OVER");
		uiManager.ShowGameOver(winner);
		if (winner == null)
		{
			Debug.Log("DRAW");
		}
		else
		{
			Debug.Log(winner.playerName + " WINS");
		}
	}

	protected IEnumerator ProcessTurn()
	{
		while (gameState == GameState.ACTIVE)
		{
			foreach (CannonController cannon in cannons)
			{
				cannon.cannonState = CannonState.ACTIVE;
				camBehaviour.MoveBehindObject(cannon.transform.parent, cameraOffset, cameraRotation);
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
				// Move board if necessary
				if (gameState == GameState.ACTIVE && turnsSinceReposition >= turnsPerReposition)
				{
					board.SetBoardDestination();
					turnsSinceReposition = 0;
				}
			}
		}
	}
}
