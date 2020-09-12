using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipCore : ScriptableObject
{
    public abstract void Act(ShipController controller);
}
