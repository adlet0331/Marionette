using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Item ���� / ���� ������ ������Ʈ
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
