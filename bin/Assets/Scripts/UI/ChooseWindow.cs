using System.Collections;
using System.Collections.Generic;
using DataBaseScripts;
using UnityEngine;

namespace UI {
	public class ChooseWindow : WindowObject
	{
    	[SerializeField] private GameObject chooseButtonPrefab;
    	[SerializeField] private GameObject chooseButtonField;
        [SerializeField] private ChooseData chooseData;
        public override void Activate()
    	{
        	this.OpenWindow();
   		}
		

		public void buttonGenerate(int num) // 1, 2, 3, 4 only
		{
			for (int i=1; i<=num; i++) {
				Instantiate(chooseButtonPrefab, 
					new Vector3(0.0f, 22.5f*(4-num) + 45.0f * i - 10.0f, 0.0f), 
					Quaternion.identity, 
					chooseButtonField.transform);
			}
		}
	}
}

