using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	public Transform target;
	float speed = 0.5f;
	Vector3[] path;
	int targetIndex;
	public Vector3 currentWaypoint;

	private bool pathSucess;
	public bool PathSucess
	{ get { return pathSucess; } }

	static List<Unit> unitList = new List<Unit>();
	public static List<Unit> UnitList
	{ get { return unitList; } }

	// Use this for initialization
	void Start () {
		unitList.Add (this);
		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
	}

	public void findNewPath(){
		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccesful) {
		pathSucess = pathSuccesful;
		if (pathSuccesful) {
			path = newPath;
			targetIndex = 0;
			StopCoroutine ("FollowPath");
			StartCoroutine ("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		currentWaypoint = path [0];
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				currentWaypoint = path [targetIndex];
			}

			transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere (currentWaypoint, 0.2f);
	}
}
