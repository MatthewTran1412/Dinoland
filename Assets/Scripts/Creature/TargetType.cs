using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetType : MonoBehaviour
{
    public enum Type{
        Herb,
        Water,
        Herbivore,
        Carnivore,
        Meat,
    }
    public Type m_Type;
}
