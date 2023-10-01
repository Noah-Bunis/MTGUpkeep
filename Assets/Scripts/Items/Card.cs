using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string title;
    public string cardName;
    public string rules;
    public string flavor;
    public Sprite art;
    public GameObject item;
    public Color titleColor;
    public Color selectedColor;
}
