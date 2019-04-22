using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PointsClass
{
	public string LevelName;                // Defines level name
	[Range(0, 3)] public int LevelPoints;   // Defines number of points in said level. Limited to 0 to 3 stars


	public PointsClass(string newLevelName, int newLevelPoints)
	{
		LevelName = newLevelName;
		LevelPoints = newLevelPoints;
	}
}
