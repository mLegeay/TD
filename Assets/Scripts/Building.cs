using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
	private BuildingType buildType;
	public BuildingType BuildType { get{ return buildType; } set{ buildType=value; }}

	private Node node;
	public Node Node { get { return node; } set { node = value; } }

	private Transform target;
	private Unit targetEnemy;
	public string enemyTag = "Enemy";

	public float turnSpeed = 10f;
	public float range = 3f;

	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget ()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			print (shortestDistance);
			target = nearestEnemy.transform;
			targetEnemy = nearestEnemy.GetComponent<Unit>();
		} else
		{
			target = null;
		}

	}

	// Update is called once per frame
	void Update () {
		if (target == null)
		{
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);;
			return;
		}

		LockOnTarget();
	}

	void LockOnTarget ()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
	
}