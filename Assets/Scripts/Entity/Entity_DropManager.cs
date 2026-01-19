using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity_DropManager : MonoBehaviour
{
    [SerializeField] private GameObject itemDropPreFab;
    [SerializeField] private ItemListDataSO dropData;

    [Header("Drop restriction")]
    [SerializeField] private int maxRarityAmount = 1200;
    [SerializeField] private int maxItemToDrop = 3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            DropItems();
    }

    public virtual void DropItems()
    {
        if(dropData == null)
        {
            Debug.Log("You need to assign drop data on entity " + gameObject.name);
            return;
        }

        List<ItemDataSO> itemsToDrop = RollDrops();
        int amountToDrop = Mathf.Min(itemsToDrop.Count, maxItemToDrop);

        for (int i = 0; i < amountToDrop; i++)
        {
            CreateItemDrop(itemsToDrop[i]);
        }
    }

    protected void CreateItemDrop(ItemDataSO itemToDrop)
    {
        GameObject newItem = Instantiate(itemDropPreFab, transform.position, Quaternion.identity);
        newItem.GetComponent<Object_ItemPickup>().SetupItem(itemToDrop);
    }

    public List<ItemDataSO> RollDrops()
    {

        List<ItemDataSO> possibleDrops = new List<ItemDataSO>();
        List<ItemDataSO> finalDrops = new List<ItemDataSO>();
        float maxRarityAmount = this.maxRarityAmount;

        // Step 1 : roll each item base on rarity and max drop chance
        foreach (var item in dropData.itemList)  
        {
            float dropChance = item.GetDropChance();

            if(Random.Range(0, 100) <= dropChance)
                possibleDrops.Add(item);
        }

        // step 2 : sort by rarity (highest to lowest)
        possibleDrops = possibleDrops.OrderByDescending(item => item.itemRarity).ToList();

        // step 3: add items to final drop list until rarity limit on entity is reached

        foreach (var item in possibleDrops)
        {
            if(maxRarityAmount > item.itemRarity)
            {
                finalDrops.Add(item);
                maxRarityAmount = maxRarityAmount - item.itemRarity;
            }
        }

        return finalDrops;
    }
}
