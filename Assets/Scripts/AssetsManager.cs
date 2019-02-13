using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AssetsManager : MonoBehaviour
{
	public Sprite emptyCell;

	public Sprite[] tower = new Sprite[5];

	public AudioSource buildingAudioSource;

	public Sprite[] logo = new Sprite[4];

	private static AssetsManager instance;
	public static AssetsManager Instance
	{
		get { return instance; }
	}

	public AssetsManager()
	{
		instance = this;
	}

	private void Start()
	{
		buildingAudioSource = GetComponent<AudioSource>();
	}
}
