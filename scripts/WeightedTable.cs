using System;
using System.Collections.Generic;
using System.Linq;

public class WeightedTable<T>
{
    Dictionary<T, int> items;
    int totalWeight;

    public WeightedTable()
    {
        items = new Dictionary<T, int>();
    }

    public void AddItem(T item, int weight)
    {
        if (items.Keys.Contains(item))
        {
            items[item] += weight;
        }
        else
        {
            items.Add(item, weight);
        }
        totalWeight += weight;
    }

    public void RemoveItem(T item)
    {
        if (items.Keys.Contains(item))
        {
            totalWeight -= items[item];
            items.Remove(item);
        }
    }

    public T PickItem()
    {
        T returnItem = default(T);
        Random rng = new Random();
        int chosenWeight = rng.Next(1, totalWeight);
        int iterationSum = 0;
        foreach (T item in items.Keys)
        {
            iterationSum += items[item];
            if (chosenWeight <= iterationSum)
            {
                return item;
            }
        }
        return returnItem;
    }
}