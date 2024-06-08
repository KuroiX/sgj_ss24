using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public enum CardAction
{
    Up,
    Down,
    Left,
    Right,
    UpD,
    DownD,
    LeftD,
    RightD
}

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public List<CardAction> cardActions;
    public Texture2D image;
}