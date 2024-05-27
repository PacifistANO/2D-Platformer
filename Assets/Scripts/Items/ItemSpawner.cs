using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<Item> _itemPrefabs;

    private Transform[] _points;

    private void Start()
    {
        _points = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _points[i] = transform.GetChild(i);
            Spawn(_points[i]);
        }
    }

    private void Spawn(Transform point)
    {
        int itemId = Random.Range(0, _itemPrefabs.Count);
        Item item = Instantiate(_itemPrefabs[itemId], point.position, Quaternion.identity);
        item.transform.SetParent(point);
        item.IsCollected += DeleteItem;
    }

    private void DeleteItem(Item item)
    {
        item.IsCollected -= DeleteItem;
        Destroy(item.gameObject);
    }
}
