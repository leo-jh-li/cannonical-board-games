using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Board : BoardManager
{
	[SerializeField]
	private int winningLineLength; // Number of cells in a row needed to win
	[SerializeField]
	protected Material[] colMaterials;

	protected override void InitializeBoard()
	{
		base.InitializeBoard();
		for (int i = 0; i < cols; i++) {
			// Set bottom row to claimable
			((C4Cell)board[i, 0]).claimable = true;
			// Set column colour
			for (int j = 0; j < rows; j++) {
				// ((C4Cell)board[i, 0]).claimable = true;
				if (colMaterials.Length > i) {
            		((C4Cell)board[i, j]).SetCellMaterial(colMaterials[i]);
				}
			}
		}
	}

	protected override void GetCellWidth()
	{
		GameObject cell = Instantiate(cellPrefab);
		cellWidth = cell.GetComponentInChildren<Renderer>().bounds.size.x;
		Destroy(cell);
	}

	// Count consecutive claimed cells in the given directions and return true iff this is a winning line
	public bool CheckLine(Cell cell, List<Direction> dirs) {
		int consecutiveCells = 1;
		int dist = 1;
		// Count cells owned by the same player in all directions
		foreach (Direction dir in dirs) {
			Cell currCell = GetRelativeCell(cell, dir, dist);
			while (currCell != null && currCell.owner == cell.owner) {
				consecutiveCells++;
				if (consecutiveCells >= winningLineLength) {
					return true;
				}
				dist++;
				currCell = GetRelativeCell(cell, dir, dist);
			}
			dist = 1;
		}
		return false;
	}

	// Check for winningLineLength consecutive cells in any direction from the given cell and end the game if necessary
	public override void CheckGameOver(Cell cell)
	{
		// Check vertical, horizontal, and diagonal lines
		bool gameOver = CheckLine(cell, new List<Direction> { Direction.DOWN }) ||
						CheckLine(cell, new List<Direction> { Direction.LEFT, Direction.RIGHT }) ||
						CheckLine(cell, new List<Direction> { Direction.UP_LEFT, Direction.DOWN_RIGHT }) ||
						CheckLine(cell, new List<Direction> { Direction.DOWN_LEFT, Direction.UP_RIGHT });

		if (gameOver) {
			game.EndGame(cell.owner);
		}
		//TODO: end game if unwinnable
		// if (remainingLines.Count == 0)
		// {
		// 	game.EndGame(null);
		// }
	}
}
