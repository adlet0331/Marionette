﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using UnityEngine;

/* InteractAsync 되는 객체
 * [상호작용 물체의 종류]
            2. 오브젝트 생성 / 소멸 - ObjectCreateDelete
            3. 대사 - Script
            4. 선택지 - Choose
            5. 맵 이동 - Teleport
    6. 애니메이션 - Animation
    7. 카메라 워크 - CameraWalk
        8. 아이템 획득, 삭제 - ItemControl
        9. 스트레스 수치 조정 - StressControl
        10. 잠김 - Lock
    11. 아니마 획득 - AnimaAdd
    12. 이벤트 - 내부 코드로 대응
 */

namespace InGameObjects.Interaction
{
    public class InteractingObject : MonoBehaviour
    {
        [SerializeField] private int idx;
        [SerializeField] private List<int> dataType;
        [SerializeField] private List<bool> goNextImmediatly;
        [SerializeField] private List<GameObject> interactingObjectList;
        [SerializeField] private bool disableAfterInteract;

        [SerializeField] private int currentInteractIndex = 0;

        public int Idx
        {
            get => idx;
        }

        public bool DisableAfterInteract
        {
            get => disableAfterInteract;
        }

        public void AddInteractingObject(GameObject gameObject)
        {
            interactingObjectList.Add(gameObject);
        }

        public void Initiate(int index, List<int> typeList, List<bool> goNextList, bool disableAfterInteract)
        {
            idx = index;
            dataType = typeList;
            goNextImmediatly = goNextList;
            this.disableAfterInteract = disableAfterInteract;
        }
        
        /*
         * Interact가 끝났는지 Return
         */
        public async UniTask<bool> InteractAsync()
        {
            // GameObject 생성/제거
            bool isInteractionEnd;
            if (dataType[currentInteractIndex] == 2)
            {
                isInteractionEnd = await interactingObjectList[currentInteractIndex].GetComponent<ObjectControl>().Interact();
            }
            // 대사 Script
            if (dataType[currentInteractIndex] == 3)
            {
                isInteractionEnd = await interactingObjectList[currentInteractIndex].GetComponent<ScriptControl>().Interact();
            }
            // 선택지 Choose
            else if (dataType[currentInteractIndex] == 4)
            {
                isInteractionEnd = await interactingObjectList[currentInteractIndex].GetComponent<ChooseControl>().Interact();
            }
            // Move
            else if (dataType[currentInteractIndex] == 5)
            {
                isInteractionEnd = await interactingObjectList[currentInteractIndex].GetComponent<MoveControl>().Interact();
            }
            // Camera Walk
            else if (dataType[currentInteractIndex] == 7)
            {
                isInteractionEnd = await interactingObjectList[currentInteractIndex].GetComponent<CameraControl>().Interact();
            }

            // ItemControl
            else if (dataType[currentInteractIndex] == 8)
            {
                isInteractionEnd = await interactingObjectList[currentInteractIndex].GetComponent<ItemControl>().Interact();
            }
            // Stress Control
            else if (dataType[currentInteractIndex] == 9)
            {
                isInteractionEnd = await interactingObjectList[currentInteractIndex].GetComponent<ScriptControl>().Interact();
            }
            // 잠김 Lock
            else if (dataType[currentInteractIndex] == 10)
            {
                isInteractionEnd = await interactingObjectList[currentInteractIndex].GetComponent<LockControl>().Interact();
            }
            // 이벤트 Event
            else if (dataType[currentInteractIndex] == 11)
            {
                throw new NotImplementedException();
            }
            // IInteractionObjectWithUI 실행
            else
            {
                throw new InteractTypeNotDefinedException(this.currentInteractIndex, currentInteractIndex);
            }

            if (isInteractionEnd)
            {
                currentInteractIndex++;
                if (currentInteractIndex == dataType.Count)
                {
                    currentInteractIndex = 0;
                    return true;
                }
                
                // 끝이 아니라면 다음 거 바로 띄워주기
                await InteractAsync();
            }
            
            return false;
        }

        public class InteractTypeNotDefinedException : Exception
        {
            public InteractTypeNotDefinedException(int idx, int datatype)
            {
                Message = $"InteractAsync DataType {datatype} is not defined. Check InteractionDataBase Data index : {idx}";
            }

            public override string Message { get; }
        }
    }
}
