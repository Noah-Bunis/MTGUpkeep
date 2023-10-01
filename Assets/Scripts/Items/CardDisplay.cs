using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text cardTitle;
    public Text cardRules;
    public Text cardFlavor;
    public Image cardArt;

    public CardButton button;
    public Button buttonUI;

    void Update()
    {
        cardTitle.color = card.titleColor;
        cardTitle.text = card.title;
        cardRules.text = card.rules;
        cardFlavor.text = card.flavor;
        cardArt.sprite = card.art;

        button.cardName = card.name;
        button.item = card.item;

        ColorBlock cb = buttonUI.colors;
        cb.selectedColor = card.selectedColor;
        cb.highlightedColor = card.selectedColor;
        buttonUI.colors = cb;
    }
}
