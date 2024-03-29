﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using Managers;
using UnityEngine;

/* InteractAsync 되는 객체
 * [상호작용 물체의 종류]
    2. 오브젝트 생성 / 소멸 - ObjectControl
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
        [Header("데이터")]
        [SerializeField] private int idx;
        [SerializeField] private List<int> dataType;
        [SerializeField] private List<bool> goNextImmediatly;
        [SerializeField] private List<GameObject> interactingObjectList;
        [SerializeField] private bool disableAfterInteract;
        [SerializeField] private int currentInteractIndex = 0;
        public int Idx => idx;
        public bool DisableAfterInteract => disableAfterInteract;

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
        
        public void SetActiveNotify(bool isAc)
        {
            gameObject.SetActive(isAc);
            GamePlayManager.Instance.InteractionStatusChangedNotify(isAc, idx);

            var groupInteraction = FindObjectOfType<GroupInteraction>();
            var interactionObjects = groupInteraction.GetComponentsInChildren<InteractingObject>();
            foreach (var obj in interactionObjects)
            {
                if (obj.idx != idx)
                    continue;
                obj.gameObject.SetActive(isAc);
            }
        }
        
        /*
         * Interact가 끝났는지 Return
         */
        public async UniTask<bool> InteractAsync()
        {
            GamePlayManager.Instance.BlockInteract();
            // GameObject 생성/제거
            bool isInteractionEnd;
            isInteractionEnd = await interactingObjectList[currentInteractIndex].GetComponent<AInteractionObject>().Interact();
            
            // Lock이 해제 되었는지 확인
            if (dataType[currentInteractIndex] == 10)
            {
                if (isInteractionEnd && !interactingObjectList[currentInteractIndex].GetComponent<LockControl>().UnLocked)
                {
                    currentInteractIndex = dataType.Count;
                }
            }

            if (isInteractionEnd)
            {
                currentInteractIndex++;
                // 데이터의 끝
                if (currentInteractIndex >= dataType.Count)
                {
                    currentInteractIndex = 0;
                    GamePlayManager.Instance.UnBlockInteract();
                    return true;
                }
                
                // 끝이 아니라면 다음 거 바로 띄워주기
                if (goNextImmediatly[currentInteractIndex - 1])
                {
                    InteractAsync().Forget();
                    return false;
                }
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
