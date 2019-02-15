using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	Node node;
	private static BuildManager instance;
	public static BuildManager Instance
	{
		get { return instance; }
	}

	public BuildManager()
	{
		instance = this;
		Node node = null;
	}

	public bool Construct(Node node)
	{
		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.layer = 8;
		cube.transform.position = new Vector3 (node.gridX, 0.5f, node.gridY);

		if (!CanBuild (node)) {
			Destroy (cube);
		} 
		else {
			foreach (Unit unit in Unit.UnitList) {
				unit.findNewPath ();
				if (!unit.PathSucess) {
					Destroy (cube);
					setWalkable (true);
				}
			}
		}
		return true;
	}
		
	public bool CanBuild(Node _node)
	{
		node = _node;
		Debug.Log (Physics.BoxCast(new Vector3(node.gridX - 1, 0, node.gridY - 1) + Vector3.one, new Vector3(1, 1, 1), new Vector3(1, 1, 1)));
		bool canBuild = false;

		if (MapGrid.Instance.grid [node.gridX, node.gridY].walkable &&
			MapGrid.Instance.grid [node.gridX - 1, node.gridY].walkable &&
			MapGrid.Instance.grid [node.gridX, node.gridY - 1].walkable &&
			MapGrid.Instance.grid [node.gridX - 1, node.gridY - 1].walkable){

			setWalkable (false);

			SpawnerController.Instance.findNewPath ();
			if (SpawnerController.Instance.PathSucess) {
				canBuild = true;
			} else {
				setWalkable (true);
			}

		} else {
			canBuild = false;
		}
		return canBuild;
	}

	void setWalkable(bool boolean){
		MapGrid.Instance.grid [node.gridX, node.gridY].walkable = boolean;
		MapGrid.Instance.grid [node.gridX - 1, node.gridY].walkable = boolean;
		MapGrid.Instance.grid [node.gridX, node.gridY - 1].walkable = boolean;
		MapGrid.Instance.grid [node.gridX - 1, node.gridY - 1].walkable = boolean;
	}
	void OnDrawGizmos()
	{
		if(node != null){
			Gizmos.color = Color.yellow;
			Gizmos.DrawRay (new Vector3(node.gridX, 0, node.gridY), new Vector3(0.5f, 0.5f, 0.5f));
			Gizmos.DrawWireCube (new Vector3(node.gridX, 0, node.gridY), Vector3.one + Vector3.one);
		}
	}
}
