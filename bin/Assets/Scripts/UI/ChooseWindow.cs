using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DataBaseScripts;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
	public class ChooseWindow : UIControlWindow<ChooseData>
	{
		[SerializeField] private GameObject chooseButtonField;
		[SerializeField] private List<GameObject> GameObjectList;
		
		private void Start()
		{
			Canvas.ForceUpdateCanvases();
			GameObjectList.Clear();
		}

		public override void Activate()
        {
	        this.OpenWindow();

	        if (GameObjectList.Count != 0)
		        throw new Exception("ChooseWindow's GameObjectList is not Initiated");
	        
	        RectTransform rectTransform = chooseButtonField.GetComponent<RectTransform>();
	        var sizeDelta = rectTransform.rect;
	        float width = sizeDelta.x * 2;
	        float height = - sizeDelta.y * 2;
	        
	        Debug.Log(height);
	        
	        for (int i = 0; i < data.scriptList.Count; i++)
	        {
		        var i1 = i;
		        GameObject button = Instantiate(
			        Resources.Load<GameObject>(Path.Combine("Prefabs", "UI", "ChooseButton")), 
			        chooseButtonField.transform, true);
		        button.transform.localPosition = new Vector3(
			        0, 
			        height / 2 - (height / (data.scriptList.Count + 1)) * (i1 + 1), 
			        0);
		        button.name = "button_" + i;
		        button.GetComponentInChildren<Text>().text = data.scriptList[i];
		        button.GetComponent<Button>().onClick.AddListener(() => InteractWithIndex(i1));
		        GameObjectList.Add(button);
	        }
        }

		public void CloseWindow()
		{
			foreach (var gameObject in GameObjectList)
			{
				Destroy(gameObject);
			}
			GameObjectList.Clear();
			base.CloseWindow();
		}

        private void InteractWithIndex(int idx)
        {
	        PlayerManager.Instance.interactingPlayer.SetFstInteractObj(data.interactionGameObjectList[idx]);

	        CloseWindow();
	        
	        data.interactionGameObjectList[idx].Interact();
        }
	}
}

