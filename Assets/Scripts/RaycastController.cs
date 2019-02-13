using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;

	private Node[,] grid;

	// Use this for initialization
	void Start () {
		grid = MapGrid.Instance.grid;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				float x = hit.point.x + 0.5f;
				float z = hit.point.z + 0.5f;

				int gridX = (int)Mathf.Floor (x);
				int gridZ = (int)Mathf.Floor (z);

				gridX = gridX == 0 ? 1 : gridX;
				gridZ = gridZ == 0 ? 1 : gridZ;

				gridX = gridX >= 20 ? 19 : gridX;
				gridZ = gridZ == 20 ? 19 : gridZ;

				BuildManager.Instance.Construct (grid[gridX, gridZ]);
			}
		}
	}
}
