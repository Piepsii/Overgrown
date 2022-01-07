using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    Empty,
    BuildingOne,
    BuildingTwo,
    Whatever
}
public class Slot : MonoBehaviour
{
    public SlotType SlotType;
}

