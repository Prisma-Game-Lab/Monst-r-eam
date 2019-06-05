using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * 
 * Script to contabilize the points in the games levels.
 * 
 * Author: Hugo Tonette, André Mazal Krauss
 * 
 */

//um hackzinho para o Unity serializar listas de listas
[System.Serializable]
 public class ListWrapper
 {
    public List<PointsClass> myList;

	// Define the indexer to allow client code to use [] notation.
	public PointsClass this[int i]
	{
		get { return myList[i]; }
		set { myList[i] = value; }
	}
 }

 //uma dúvida para o Hugo: fases não cumpridas, devem ser armazenadas com 0 estrelas?
 //ou só deixar "não preenchido" basta? Me é estranho isso
[Serializable]
[CreateAssetMenu]
public class PointSystem : ScriptableObject 
{
	
	//uma lista de listas, para podermos indexar por página e por level
	[Header("Uma lista de lista, indexada por página e por numero do level")]
	public List<ListWrapper> PointsList;

	// This function Adds a level or Update an existing one
	public void UpdateLevel(int levelPage, int levelNumber, int newLevelPoint, bool newLevelCleared, bool[] newCharSaved)
	{
		// Check if the points are correct
		int currPoints = GetLevelPoints(levelPage, levelNumber);
		//esse <= vs < decide se o jogo é "conservador" ou "sempre atualizado" em relação à quais personagens ele mostra pro score
		//ele troca, na pontuação mostrada, uma run com ze e dedé por uma run com zé e mané? 
		//SIM => '<'
		//NÃO => '<='
		if(newLevelPoint < currPoints)
		{
			return;
		}

		if (!(newLevelPoint < 0 || newLevelPoint > 3))
		{
			// Checks if the list is empty
			Debug.Assert(PointsList != null && PointsList.Count != 0);
					
			// try to Update item
			if(PointsList[levelPage] != null && PointsList[levelPage][levelNumber] != null)
			{
				
				PointsList[levelPage][levelNumber].cleared = newLevelCleared;
				PointsList[levelPage][levelNumber].LevelPoints = newLevelCleared ? newLevelPoint : 0;
				//atualiza relação de bixins salvos
				for(int i = 0; i < 3; i++)
				{
					PointsList[levelPage][levelNumber].charSaved[i] = newCharSaved[i];
				}
				
			}
			else
			{
				Debug.LogWarning("Alter points failed. Invalid indexes for page and/or level");
			}
						
		}
		else
		{
			Debug.LogWarning("Save Failed: Wrong number of points. Points must be limited to 0 to 3 points.");
		}
	}


	// This function gets the points of said level
	public int GetLevelPoints (int levelPage, int levelNumber)
	{
		if(PointsList[levelPage] != null && PointsList[levelPage][levelNumber] != null)
		{
				
			return PointsList[levelPage][levelNumber].LevelPoints;
				
		}
		else
		{
			Debug.LogWarning("Get points failed: Invalid indexes for page and/or level");
			return 0;
		}
	}

	
	public PointsClass GetLevelInfo (int levelPage, int levelNumber)
	{
		try
		{
		
			if(PointsList[levelPage] != null && PointsList[levelPage][levelNumber] != null)
			{
					
				return PointsList[levelPage][levelNumber];
					
			}
			else
			{
				Debug.LogWarning("Get points failed: Invalid indexes for page and/or level");
				return null;
			}
		}
		catch(ArgumentOutOfRangeException)
		{
			Debug.LogWarning("Get points failed: Invalid indexes for page and/or level");
			return null;
		}
	}

}
