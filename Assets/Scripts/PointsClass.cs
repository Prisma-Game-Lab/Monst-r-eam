using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PointsClass
{
	[SerializeField]
	private int levelPoints;   // Defines number of points in said level. Limited to 0 to 3 stars
	public int LevelPoints
	{
		get
		{
			return levelPoints;
		}
		set
		{
			//bloqueia de diminuir o valor e de setar para fora de [0,3]
			if(value  >=  0 && value <= 3 && value > levelPoints)
			{
				levelPoints = value;
			}
		}
	}

	public bool[] charSaved = new bool[3];

	public int levelPage;
	public int levelNumber;
	public bool cleared; // meio redudante, dado que já daria pra ver pelo número de estrelas

}
