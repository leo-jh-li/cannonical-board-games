//using System.Collections;
using System.Collections.Generic;

public class Line
{
	private List<Coord> coords;

	public Line(List<Coord> coords)
	{
		this.coords = new List<Coord>();
		foreach (Coord coord in coords)
		{
			this.coords.Add(coord);
		}
	}

	public List<Coord> GetCoords()
	{
		return coords;
	}

	public bool Contains(Coord coord)
	{
		return coords.Contains(coord);
	}
}
