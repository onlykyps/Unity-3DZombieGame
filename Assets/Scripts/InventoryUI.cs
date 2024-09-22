using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public Transform itemsParent;
	public InventorySlot[] slots;
	public GameObject inventoryUI;
	Inventory inventory;
	// Start is called before the first frame update
	void Start()
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallBack = UpDateUI;
		slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		inventoryUI.GetComponent<Canvas>().enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			//inventoryUI.SetActive(!inventoryUI.activeSelf);
			inventoryUI.GetComponent<Canvas>().enabled = !inventoryUI.GetComponent<Canvas>().enabled;

		}
	}

	void UpDateUI()
	{
		Debug.Log("Updating UI");
		for (int i = 0; i < slots.Length; i++)
		{
			if (i < inventory.items.Count)
			{
				slots[i].AddItem(inventory.items[i]);
			}
			else
			{
				slots[i].ClearSlot();
			}
		}
	}
}
