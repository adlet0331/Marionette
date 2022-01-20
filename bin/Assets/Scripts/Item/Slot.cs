using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] bool isEmpty;
    [SerializeField] Image image;

    public void SetIsEmpty(bool isEmpty)
    {
        this.isEmpty = isEmpty;
    }
    public void SetImage(Sprite itemSprite)
    {
        image.sprite = itemSprite;
        return;
    }
}
