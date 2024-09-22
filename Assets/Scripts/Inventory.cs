﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region Singleton
	public static Inventory instance;
	void Awake()
	{
		if (instance != null)
		{
			Debug.Log("More than one instance of Inventory found");
			return;
		}
		instance = this;
	}
	#endregion

	public List<Item> items = new List<Item>();
	public int space = 2;
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallBack;

	public bool Add(Item item)
	{
		if (!item.isDefaultItem)
		{
			if (items.Count >= space)
			{
				Debug.Log("We do not have enough room");
				return false;
			}
			items.Add(item);
			if (onItemChangedCallBack != null)
				onItemChangedCallBack.Invoke();
		}
		return true;
	}

	public void Remove(Item item)
	{
		items.Remove(item);
		if (onItemChangedCallBack != null)
			onItemChangedCallBack.Invoke();
	}
}
