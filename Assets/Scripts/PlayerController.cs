using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	private static PlayerController instance;
	public static PlayerController Instance
	{ get { return instance; } }

	private Player player;
	public Player Player
	{ get { return player; } }

	private Node selectedNode;
	public Node SelectedNode { get{return selectedNode;} set { selectedNode = value; }}

	public PlayerController()
	{
		instance = this;
	}

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}