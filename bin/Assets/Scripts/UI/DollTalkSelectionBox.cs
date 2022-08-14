using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DollTalkSelectionBox : MonoBehaviour
    {
        [SerializeField] private GameObject SelectionArrow;
        [SerializeField] private Text SelectionText;

        public void ActivateSelection(bool isActivating)
        {
            SelectionArrow.gameObject.SetActive(isActivating);
        }
        public void SetText(string textString)
        {
            SelectionText.text = textString;
        }
    }
}