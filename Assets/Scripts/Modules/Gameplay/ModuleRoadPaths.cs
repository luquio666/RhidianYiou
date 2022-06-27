using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ModuleRoadPaths))]
public class ModuleRoadPathsEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		ModuleRoadPaths myScript = (ModuleRoadPaths)target;
		if (GUILayout.Button("Spawn Car"))
		{
			myScript.SpawnCar();
		}
		if (GUILayout.Button("Remove Last Car"))
		{
			myScript.RemoveLastCar();
		}

		if (GUILayout.Button("Remove All Cars"))
		{
			myScript.RemoveAllCars();
		}
	}
}
#endif

public class ModuleRoadPaths : Module
{
	public Transform[] Points;
	public GameObject[] Cars;

	public void SpawnCar()
	{
		var rndCar = Cars[Random.Range(0, Cars.Length)];
		var rndPosIndex = Random.Range(0,Points.Length);

		Vector3 startPos;
		Vector3 endPos;

		Vector3 spawnPos;
		if (rndPosIndex == Points.Length - 1)
		{
			startPos = Points[rndPosIndex - 1].position;
			endPos = Points[rndPosIndex].position;
			spawnPos = Vector3.Lerp(startPos, endPos, Random.Range(0, 1f));
		}
		else
		{
			rndPosIndex++;
			startPos = Points[rndPosIndex - 1].position;
			endPos = Points[rndPosIndex].position;
			spawnPos = Vector3.Lerp(startPos, endPos, Random.Range(0, 1f));
		}

		var car = Instantiate(rndCar, spawnPos, Quaternion.identity);
		car.transform.SetParent(this.transform);

		var carSprite = car.GetComponent<ModuleCar>();
		carSprite.SetCarSprite(startPos);
		
		var moveBetween = car.GetComponent<ModuleCarMoveBetweenPoints>();
		moveBetween.Points = Points;
		moveBetween.NextTarget = rndPosIndex;
	}

	public void RemoveLastCar()
	{
		if(this.transform.childCount > 0)
			DestroyImmediate(transform.GetChild(transform.childCount - 1).gameObject);
	}

	public void RemoveAllCars()
	{
        for (int i = transform.childCount-1; i >= 0 ; i--)
        {
			DestroyImmediate(transform.GetChild(i).gameObject);
        }
	}

	private void OnDrawGizmos()
	{
		// Check we have at least 2 points
		if (Points != null && Points.Length > 1)
		{
            for (int i = 0; i < Points.Length; i++)
            {
				// green sphere for first point
				if (i == 0)
				{
					Gizmos.color = Color.green;
					Gizmos.DrawWireSphere(Points[i].position, 0.25f);
				}
				// draw line between current and next point
				if (i + 1 < Points.Length)
				{
					Gizmos.color = Color.green;
					Gizmos.DrawLine(Points[i].position, Points[i+1].position);
				}

				// red sphere for last point
				if (i == Points.Length - 1)
				{
					Gizmos.color = Color.blue;
					Gizmos.DrawLine(Points[0].position, Points[i].position);

					Gizmos.color = Color.red;
					Gizmos.DrawWireSphere(Points[i].position, 0.25f);
				}
            }
		}
	}
}
