using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	private static BuildManager instance;
	public static BuildManager Instance
	{
		get { return instance; }
	}

	public BuildManager()
	{
		instance = this;
	}

	public bool Construct(Node node)
	{
		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.layer = 8;
		cube.transform.position = new Vector3 (node.gridX, 0.5f, node.gridY);

		if (MapGrid.Instance.grid [node.gridX, node.gridY].walkable && MapGrid.Instance.grid [node.gridX - 1, node.gridY].walkable && MapGrid.Instance.grid [node.gridX, node.gridY - 1].walkable && MapGrid.Instance.grid [node.gridX - 1, node.gridY - 1].walkable) {
			MapGrid.Instance.grid [node.gridX, node.gridY].walkable = false;
			MapGrid.Instance.grid [node.gridX - 1, node.gridY].walkable = false;
			MapGrid.Instance.grid [node.gridX, node.gridY - 1].walkable = false;
			MapGrid.Instance.grid [node.gridX - 1, node.gridY - 1].walkable = false;

			if (!CanBuild (node)) {
				Destroy (cube);
				MapGrid.Instance.grid [node.gridX, node.gridY].walkable = true;
				MapGrid.Instance.grid [node.gridX - 1, node.gridY].walkable = true;
				MapGrid.Instance.grid [node.gridX, node.gridY - 1].walkable = true;
				MapGrid.Instance.grid [node.gridX - 1, node.gridY - 1].walkable = true;
			}
		} else {
			Destroy (cube);
		}

		foreach (Unit unit in Unit.UnitList) {
			unit.findNewPath ();
		}
		return true;
	}
		
	public bool CanBuild(Node node)
	{
		// if node empty and node doesn't block pathfinding from starting point to endpoint, then ++ if in boundaries
		// true
		// else
		// false
		bool canBuild = false;
		SpawnerController.Instance.findNewPath ();
		if (SpawnerController.Instance.PathSucess) {
			canBuild = true;
		}
		return canBuild;
	}
}
