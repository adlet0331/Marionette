using System;
using UnityEngine;

[Serializable]
public class StressControl : AbstractInteractionObject
{
    [SerializeField] private int stressAddNum;
    public override void Interact()
    {
        StressManager.Instance.AddStress(stressAddNum);
    }
}