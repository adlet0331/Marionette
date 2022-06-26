using UnityEngine;

/* BoxCollider2D (Trigger X) 붙어있는 애들
 * 
 * 부딛히면 InteractionList에 넣어줘서 InteractionWindow 띄워줌
 */

[RequireComponent(typeof(BoxCollider2D))]

public class ScriptObject : InteractionObject
{
    [SerializeField] private bool isScriptable;
    private void Start()
    {
        this.objectType = InteractionObjectType.ScriptableObject;
    }
    public override void Interact() 
    {
        if (isScriptable)
            WindowManager.Instance.scriptWindow.Activate(this.idx, this.objectType);
    }
}
