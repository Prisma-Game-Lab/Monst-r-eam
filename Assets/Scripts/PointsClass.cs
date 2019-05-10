using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PointsClass
{
	public string LevelName;                // Defines level name
	public string SceneName;				// defines the name of the scene that should be loaded for this level 
	[Range(0, 3)] public int LevelPoints;   // Defines number of points in said level. Limited to 0 to 3 stars
	public int levelPage;
	public int levelNumber;
	public bool cleared; // meio redudante, dado que já daria pra ver pelo número de estrelas

}
