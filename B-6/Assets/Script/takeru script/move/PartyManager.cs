using UnityEngine;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;

    private Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddItem(ItemData item, int count)
    {
        if (items.ContainsKey(item))
        {
            items[item] += count;
        }
        else
        {
            items.Add(item, count);
        }

        Debug.Log(item.itemName + " ‚ð " + count + "ŒÂ“üŽè");
    }


    public int GetItemCount(ItemData item)
    {
        if (items.ContainsKey(item))
        {
            return items[item];
        }

        return 0;
    }


    public void RemoveItem(ItemData item, int count)
    {
        if (GetItemCount(item) >= count)
        {
            items[item] -= count;
        }
    }
}