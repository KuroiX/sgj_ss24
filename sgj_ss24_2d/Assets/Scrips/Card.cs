using System.Collections.Generic;
using UnityEngine;

public enum CardAction
{
    Up,
    Down,
    Left,
    Right
}

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public List<CardAction> cardActions;
}