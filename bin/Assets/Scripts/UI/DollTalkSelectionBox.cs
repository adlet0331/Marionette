using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class DollTalkSelectionBox
    {
        [SerializeField] private GameObject SelectionArrow;
        [SerializeField] private Text SelectionText;

        public void ActivateSelection(bool isActivating)
        {
            if (isActivating)
            {
                SelectionArrow.gameObject.SetActive(true);
            }
            else
            {
                SelectionArrow.gameObject.SetActive(false);
            }
        }

        public void SetText(string textString)
        {
            SelectionText.text = textString;
        }
    }
}