using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    public int panelIndex = 0;

    public abstract void Reset();

    public abstract void Init();
}
