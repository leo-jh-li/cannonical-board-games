using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBoard : BoardManager
{
	private List<Line> remainingLines;  // Board lines that have at least one empty cell in them

	protected override void InitializeBoard()
	{
		base.InitializeBoard();
		remainingLines = new List<Line>();
		remainingLines.Add(new Line(new List<Coord> { new Coord(0, 0), new Coord(0, 1), new Coord(0, 2) }));
		remainingLines.Add(new Line(new List<Coord> { new Coord(1, 0), new Coord(1, 1), new Coord(1, 2) }));
		remainingLines.Add(new Line(new List<Coord> { new Coord(2, 0), new Coord(2, 1), new Coord(2, 2) }));
		remainingLines.Add(new Line(new List<Coord> { new Coord(0, 0), new Coord(1, 0), new Coord(2, 0) }));
		remainingLines.Add(new Line(new List<Coord> { new Coord(0, 1), new Coord(1, 1), new Coord(2, 1) }));
		remainingLines.Add(new Line(new List<Coord> { new Coord(0, 2), new Coord(1, 2), new Coord(2, 2) }));
		remainingLines.Add(new Line(new List<Coord> { new Coord(0, 0), new Coord(1, 1), new Coord(2, 2) }));
		remainingLines.Add(new Line(new List<Coord> { new Coord(0, 2), new Coord(1, 1), new Coord(2, 0) }));
	}

	protected override void GetCellWidth()
	{
		GameObject cell = Instantiate(cellPrefab);
		cellWidth = cell.GetComponent<Collider>().bounds.size.x;
		Destroy(cell);
	}

	// Return true iff this line can still be used to win the game
	private bool IsClaimable(Line line)
	{
		HashSet<Player> ownersInLine = new HashSet<Player>();
		foreach (Coord coord in line.GetCoords())
		{
			if (board[coord.x, coord.y].owner != null)
			{
				if (!ownersInLine.Contains(board[coord.x, coord.y].owner))
				{
					ownersInLine.Add(board[coord.x, coord.y].owner);
					if (ownersInLine.Count > 1)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	public override void CheckGameOver(Cell cell)
	{
		for (int i = remainingLines.Count - 1; i >= 0; i--)
		{
			if (remainingLines[i].Contains(cell.coord))
			{
				bool winningLine = true;
				foreach (Coord coord in remainingLines[i].GetCoords())
				{
					if (board[coord.x, coord.y].owner != cell.owner)
					{
						winningLine = false;
					}
				}
				if (winningLine)
				{
					game.EndGame(cell.owner);
				}
				else if (!IsClaimable(remainingLines[i]))
				{
					remainingLines.RemoveAt(i);
				}
			}
		}
		if (remainingLines.Count == 0)
		{
			game.EndGame(null);
		}
	}
}
