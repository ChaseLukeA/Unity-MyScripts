/**
 *
 * GameUtility Script
 * Created by Luke A Chase - chase.luke.a@gmail.com
 * 
 * -------------------------------------------------------------
 * Global methods and enums for use with other custom scripts I
 * have written for Unity
 * -------------------------------------------------------------
*/

using UnityEngine;
using System;

public static class GameUtility
{
	// returns unsigned value of a signed int
	public static int unsignedValue(int number)
	{
		return (int) Mathf.Sqrt(Mathf.Pow(number, 2));
	}

	// returns unsigned value of a signed float
	public static float unsignedValue(float number)
	{
		return Mathf.Sqrt(Mathf.Pow(number, 2));
	}
}


public enum Axis
{
	x, y, z
}


public enum Direction
{
	Positive = 1,
	Negative = -1
}


public enum MoveTo
{
	Origin,
	Destination
}


public enum Layer
{
	Everything = 1,
	Player = 8
}