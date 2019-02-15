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

		if (!CanBuild (node)) {
			Destroy (cube);
		}

		foreach (Unit unit in Unit.UnitList) {
			unit.findNewPath ();
		}
		return true;
	}
		
	public bool CanBuild(Node node)
	{
		bool canBuild = false;

		if (MapGrid.Instance.grid [node.gridX, node.gridY].walkable &&
			MapGrid.Instance.grid [node.gridX - 1, node.gridY].walkable &&
			MapGrid.Instance.grid [node.gridX, node.gridY - 1].walkable &&
			MapGrid.Instance.grid [node.gridX - 1, node.gridY - 1].walkable &&
			!Physics.BoxCast(new Vector3(node.gridX, 0, node.gridY + 0.5f), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f)) &&
			!Physics.BoxCast(new Vector3(node.gridX - 1, 0, node.gridY), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f)) &&
			!Physics.BoxCast(new Vector3(node.gridX, 0, node.gridY - 1), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f)) &&
			!Physics.BoxCast(new Vector3(node.gridX - 1, 0, node.gridY - 1), new Vector3(0.5f, 0.5f, 0.5f), new Vector3(0.5f, 0.5f, 0.5f))){

			MapGrid.Instance.grid [node.gridX, node.gridY].walkable = false;
			MapGrid.Instance.grid [node.gridX - 1, node.gridY].walkable = false;
			MapGrid.Instance.grid [node.gridX, node.gridY - 1].walkable = false;
			MapGrid.Instance.grid [node.gridX - 1, node.gridY - 1].walkable = false;

			SpawnerController.Instance.findNewPath ();
			if (SpawnerController.Instance.PathSucess) {
				canBuild = true;
			} else {
				MapGrid.Instance.grid [node.gridX, node.gridY].walkable = true;
				MapGrid.Instance.grid [node.gridX - 1, node.gridY].walkable = true;
				MapGrid.Instance.grid [node.gridX, node.gridY - 1].walkable = true;
				MapGrid.Instance.grid [node.gridX - 1, node.gridY - 1].walkable = true;
			}

		} else {
			canBuild = false;
		}
		return canBuild;
	}
	/*
	void OnDrawGizmos()
	{
		if(node != null){
			Gizmos.color = Color.yellow;
			Gizmos.DrawRay (new Vector3(node.gridX, 0, node.gridY), new Vector3(0.5f, 0.5f, 0.5f));
			Gizmos.DrawWireCube (new Vector3(node.gridX, 0, node.gridY) + new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
			Gizmos.DrawWireCube (new Vector3(node.gridX - 1, 0, node.gridY) + new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
			Gizmos.DrawWireCube (new Vector3(node.gridX, 0, node.gridY - 1) + new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
			Gizmos.DrawWireCube (new Vector3(node.gridX - 1, 0, node.gridY - 1) + new Vector3(0.5f, 0.5f, 0.5f), Vector3.one);
		}
	}
	*/
}
