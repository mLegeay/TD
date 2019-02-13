using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingType
{
	private int ressourceCost;
	public int RessourceCost { get { return ressourceCost; } set { ressourceCost = value; } }

	private Sprite buttonSprite;
	public Sprite ButtonSprite { get { return buttonSprite; } }

	public BuildingType(int ressourceCost, Sprite buttonSprite)
	{
		this.buttonSprite = buttonSprite;
		this.ressourceCost = ressourceCost;
	}

	public bool CanAfford(Player player)
	{
		return player.Money >= ressourceCost;
	}

	public void Afford(Player player)
	{
		player.Money -= ressourceCost;
	}
}
