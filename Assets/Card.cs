using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Card
{
    public string cardName;
    public string description;
    public CardType type;
    public int value;
}

public enum CardType
{
    Attack,
    Defense,
    Special
}