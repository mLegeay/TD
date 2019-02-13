using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

	public Transform target;

	private bool pathSucess;
	public bool PathSucess
	{ get { return pathSucess; } }

	private static SpawnerController instance;
	public static SpawnerController Instance
	{ get { return instance; } }

	void Awake() {
		instance = this;
	}

	void Start () {
		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
	}

	public void findNewPath(){
		PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccesful) {
		pathSucess = pathSuccesful;
	}
}
