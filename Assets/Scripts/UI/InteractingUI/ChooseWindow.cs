using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using InGameObjects.Interaction;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
	[Serializable]
	public class ChooseUIData
	{
		public List<string> scriptList;
		public List<InteractingObject> interactingObjectList;
	}
	public class ChooseWindow : UIControlWindow<ChooseUIData>
	{
		[SerializeField] private GameObject chooseButtonField;
		[SerializeField] private List<GameObject> GameObjectList;
		
		public override void DeActivate()
		{
			foreach (var gameObject in GameObjectList)
			{
				Destroy(gameObject);
			}
			GameObjectList.Clear();
			CloseWindow();
		}
		public override void Activate()
		{
			GameObjectList.Clear();
			OpenWindow();
		}
		public override void Interact()
		{
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
				button.GetComponent<Button>().onClick.AddListener(async () => await InteractWithIndexAsync(i1));
				GameObjectList.Add(button);
			}
		}

		private async UniTask InteractWithIndexAsync(int idx)
        {
	        // TODO: 반드시 봐야할 것 
	        GamePlayManager.Instance.AddInteractingObject(data.interactingObjectList[idx]);
	        DeActivate();
	        await data.interactingObjectList[idx].InteractAsync();
        }
	}
}

