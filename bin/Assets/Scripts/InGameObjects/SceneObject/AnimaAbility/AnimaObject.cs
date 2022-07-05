using DataBaseScripts;
using UnityEngine;

public class AnimaObject : InteractionObject
{
    [SerializeField] private int idx = 0;
    [SerializeField] private int upCount = 1;
    public override void Interact()
    {
        AnimaAbilityManager.Instance.IncrementAnimaAbility(idx, upCount);
    }
}
