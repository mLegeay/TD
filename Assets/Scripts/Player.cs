using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private int money;
	public int Money { get { return money; } set { money = value; } }

	private static Player instance;
	public static Player Instance
	{
		get { return instance; }
	}

	public Player()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
