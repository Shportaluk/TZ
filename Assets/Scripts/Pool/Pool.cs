using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Pool<TItem> : MonoBehaviour
    where TItem : MonoBehaviour, IPoolElement
{
    [SerializeField] private TItem _prefab;
    private readonly List<TItem> _items = new List<TItem>();

    public TItem Get()
    {
        TItem item = FindUnUseItem();

        if (item == null)
        {
            item = GameObject.Instantiate(_prefab);
            _items.Add(item);
        }

        item.SetUse();
        return item;
    }

    public IEnumerable<TItem> GetUsedItems() =>
        _items.Where(e => e.IsUsing == true);

    private TItem FindUnUseItem() =>
        _items.Find(e => e.IsUsing == false);
}