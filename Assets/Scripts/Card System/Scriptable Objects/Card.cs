using UnityEngine;
public abstract class Card : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] string description;
    [SerializeField]
    [Range(0, 100)]
    int cost;
    [SerializeField]
    [Range(0, 100)]
    int rarity;

}
