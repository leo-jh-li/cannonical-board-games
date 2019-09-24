using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Board : BoardManager
{
	protected override void InitializeBoard()
	{
		base.InitializeBoard();
		// Set bottom row to claimable
		for (int i = 0; i < rows; i++) {
			((C4Cell)board[i, 0]).claimable = true;
		}
	}

	protected override void GetCellWidth()
	{
		GameObject cell = Instantiate(cellPrefab);
		cellWidth = cell.GetComponentInChildren<Renderer>().bounds.size.x;
		Destroy(cell);
	}

	public override void CheckGameOver(Cell cell)
	{
		//TODO
		//go in one dir, count, if 4, end
		//go opposite dir, if 4, end
		//repeat for other orientations (unnecessary enum?)
		//vert: no need to check above this cell
		
		// use direction for activating above cell on the board when it gets hit


		// for (int i = remainingLines.Count - 1; i >= 0; i--)
		// {
		// 	if (remainingLines[i].Contains(cell.coord))
		// 	{
		// 		bool winningLine = true;
		// 		foreach (Coord coord in remainingLines[i].GetCoords())
		// 		{
		// 			if (board[coord.x, coord.y].owner != cell.owner)
		// 			{
		// 				winningLine = false;
		// 			}
		// 		}
		// 		if (winningLine)
		// 		{
		// 			game.EndGame(cell.owner);
		// 		}
		// 		else if (!IsClaimable(remainingLines[i]))
		// 		{
		// 			remainingLines.RemoveAt(i);
		// 		}
		// 	}
		// }
		// if (remainingLines.Count == 0)
		// {
		// 	game.EndGame(null);
		// }



	}
}
