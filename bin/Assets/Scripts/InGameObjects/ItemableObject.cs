using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Item 지급 / 삭제 가능한 오브젝트
 * 
 */
public class ItemableObject : InteractionObject
{
    private void Start()
    {
        this.objectType = Type.ItemableObject;
    }
    public override void Interact()
    {
        throw new System.NotImplementedException();
    }
}
