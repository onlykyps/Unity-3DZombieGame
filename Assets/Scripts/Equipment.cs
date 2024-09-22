using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
	public EquipementSlot equipSlot;
	public SkinnedMeshRenderer mesh;

	public int armorModifier;
	public int damageModifier;

	public override void Use()
	{
		base.Use();
		EquipmentManager.instance.Equip(this);
		//remove it from the inventory
		RemoveFromInventory();
	}
}

public enum EquipementSlot { Head, Chest, Legs, Weapon, Shield, Feet}
