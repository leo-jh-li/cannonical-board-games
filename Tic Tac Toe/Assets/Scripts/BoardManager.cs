using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
	UP,
	UP_RIGHT,
	RIGHT,
	DOWN_RIGHT,
	DOWN,
	DOWN_LEFT,
	LEFT,
	UP_LEFT
}

public abstract class BoardManager : MonoBehaviour {

	public Game game;
	public GameObject cellPrefab;
	public Vector3 startPosition;
	public Vector3 rotation;
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
		board = new Cell[cols, rows];

		for (int i = 0; i < cols; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				Vector3 cellPosition = CoordToPosition(i, j);
				board[i, j] = Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform).GetComponent<Cell>();
				board[i, j].board = this;
				board[i, j].coord = new Coord(i, j);
			}
		}
		transform.position = startPosition;
		transform.eulerAngles = rotation;
	}

	public void ResetBoard()
	{
		for (int i = 0; i < cols; i++)
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
		// Origin coord is considered the middle of the board
		Vector2 origin = new Vector2((cols - 1) / 2f, (rows - 1) / 2f);
		return new Vector3(transform.position.x + (-origin.x + i) * (cellWidth + borderWidth + marginWidth), transform.position.y + (-origin.y + j) * (cellWidth + borderWidth + marginWidth), transform.position.z);
	}

	public void SetBoardDestination()
	{
		destination = new Vector3(boardX.GetRandom(), boardY.GetRandom(), transform.position.z);
		StartCoroutine(MoveBoard());
	}

	public bool IsValidCoord(Coord coord)
	{
		return coord.x >= 0 && coord.x < cols && coord.y >= 0 && coord.y < rows;
	}
	
	// Returns the Cell dist Cells away from startCell in the given Direction. Returns null if no such Cell exists.
	public Cell GetRelativeCell(Cell startCell, Direction dir, int dist)
	{
		if (startCell == null)
		{
			return null;
		}
		Coord desiredCoord = new Coord(startCell.coord.x, startCell.coord.y);
		if (dir == Direction.UP) {
			desiredCoord.y += dist;
		} else if (dir == Direction.RIGHT) {
			desiredCoord.x += dist;
		} else if (dir == Direction.DOWN) {
			desiredCoord.y -= dist;
		} else if (dir == Direction.LEFT) {
			desiredCoord.x -= dist;
		} else if (dir == Direction.UP_RIGHT) {
			return GetRelativeCell(GetRelativeCell(startCell, Direction.UP, dist), Direction.RIGHT, dist);
		} else if (dir == Direction.DOWN_RIGHT) {
			return GetRelativeCell(GetRelativeCell(startCell, Direction.DOWN, dist), Direction.RIGHT, dist);
		} else if (dir == Direction.DOWN_LEFT) {
			return GetRelativeCell(GetRelativeCell(startCell, Direction.DOWN, dist), Direction.LEFT, dist);
		} else if (dir == Direction.UP_LEFT) {
			return GetRelativeCell(GetRelativeCell(startCell, Direction.UP, dist), Direction.LEFT, dist);
		} else {
			Debug.LogWarning("Invalid direction.");
		}
		if (IsValidCoord(desiredCoord))
		{
			return board[desiredCoord.x, desiredCoord.y];
		}
		return null;
	}

	public Cell GetRelativeCell(Cell startCell, Direction dir)
	{
		return GetRelativeCell(startCell, dir, 1);
	}

	public abstract void CheckGameOver(Cell cell);
}
