using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * 
 * Script to contabilize the points in the games levels.
 * 
 * Author: Hugo Tonette
 * 
 */

public class PointSystem : MonoBehaviour
{
	[HideInInspector]
	public List<PointsClass> PointsList = new List<PointsClass>();      // Here to save the points in each level


	// This function Adds a level or Update an existing one
	public void AddUpdateLevel(string newLevelName, int newLevelPoint)
	{
		// Check if the points are correct
		if (!(newLevelPoint < 0 || newLevelPoint > 3))
		{
			// Checks if the list is empty
			if (PointsList.Count == 0)
			{
				//If empty, create a new item in the list
				PointsList.Add(new PointsClass(newLevelName, newLevelPoint));
			}
			else
			{
				// Discover if item with specific name exists.
				PointsClass result = PointsList.First(s => s.LevelName == newLevelName);

				if(result == null)
				{
					// Add new item
					PointsList.Add(new PointsClass(newLevelName, newLevelPoint));
				}
				else
				{
					// Update item
					int index = PointsList.FindIndex(0, s => s.LevelName == newLevelName);
					PointsList[index].LevelName = newLevelName;
					PointsList[index].LevelPoints = newLevelPoint;
				}
			}
		}
		else
		{
			Debug.Log("Save Failed: Wrong number of points. Points must be limited to 0 to 3 points.");
		}
	}

	// This function clears the list, in case theres an option to reset game
	public void ClearRecords()
	{
		PointsList.Clear();
	}

	// This function checks to see the points of said level
	public int CheckList (string LevelNameCheck)
	{
		return PointsList.First(s => s.LevelName == LevelNameCheck).LevelPoints;
	}
}
