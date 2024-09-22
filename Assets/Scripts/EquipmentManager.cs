using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	#region Singleton
	public static EquipmentManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	Equipment[] currentEqipment;
	Inventory inventory;
	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;
	void Start()
	{
		int numSlots = System.Enum.GetNames(typeof(EquipementSlot)).Length;
		currentEqipment = new Equipment[numSlots];
		inventory = Inventory.instance;
	}

	public void Equip(Equipment newItem)
	{
		int slotIndex = (int)newItem.equipSlot;
		Equipment oldItem = null;
		if (currentEqipment[slotIndex] != null)
		{
			oldItem = currentEqipment[slotIndex];
			inventory.Add(oldItem);
		}
		if (onEquipmentChanged != null)
		{ onEquipmentChanged.Invoke(newItem, oldItem); }
		currentEqipment[slotIndex] = newItem;
	}

	public void UnEquip(int slotIndex)
	{
		if (currentEqipment[slotIndex] != null)
		{
			Equipment oldItem = currentEqipment[slotIndex];
			inventory.Add(oldItem);
			currentEqipment[slotIndex] = null;
			if (onEquipmentChanged != null)
			{ onEquipmentChanged.Invoke(null, oldItem); }
		}
	}

	public void UnEquipAll()
	{
		for (int i = 0; i < currentEqipment.Length; i++)
		{
			UnEquip(i);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			UnEquipAll();
		}
	}
}
