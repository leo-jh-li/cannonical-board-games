using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardManager : MonoBehaviour {

	public Game game;
	public GameObject cellPrefab;
	public Vector3 startPosition;
	public RandomRange boardX;
	public RandomRange boardY;
	protected Cell[,] board;
	[SerializeField]
	protected int rows;
	[SerializeField]
	protected int cols;
	[SerializeField]
	protected float moveStartupTime;
	[SerializeField]
	protected float moveTime;
	protected Vector3 destination;
	[SerializeField]
	protected float borderWidth; // Approximately equal to the width of a "line" on the board
	[SerializeField]
	protected float marginWidth; // Width of area to leave between cell and board borders
	protected float cellWidth;

	protected void Start()
	{
		GetCellWidth();
		InitializeBoard();
	}

	protected abstract void GetCellWidth();

	protected virtual void InitializeBoard()
	{
		board = new Cell[rows, cols];

		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				Vector3 cellPosition = CoordToPosition(i, j);
				board[i, j] = Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform).GetComponent<Cell>();
				board[i, j].board = this;
				board[i, j].coord = new Coord(i, j);
			}
		}
		transform.position = startPosition;
	}

	public void ResetBoard()
	{
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				Destroy(board[i, j].gameObject);
			}
		}
		InitializeBoard();
	}

	IEnumerator MoveBoard()
	{
		yield return new WaitForSeconds(moveStartupTime);
		Vector3 startPosition = transform.position;
		float i = 0;
		float rate = 1 / moveTime;
		while (i < 1)
		{
			i += Time.deltaTime * rate;
			transform.position = Vector3.Lerp(startPosition, destination, i);
			yield return null;
		}
	}

	// Convert a set of coordinates to its position relative to the origin
	private Vector3 CoordToPosition(int i, int j)
	{
		// Origin is considered the middle cell
		Coord origin = new Coord(rows / 2, cols / 2);
		return new Vector3(transform.position.x + (origin.x - i) * (cellWidth + borderWidth + marginWidth), transform.position.y - (origin.y - j) * (cellWidth + borderWidth + marginWidth), transform.position.z);
	}

	public void SetBoardDestination()
	{
		destination = new Vector3(boardX.GetRandom(), boardY.GetRandom(), transform.position.z);
		StartCoroutine(MoveBoard());
	}

	public abstract void CheckGameOver(Cell cell);
}
