using System.Collections.Generic;
using InGameObjects.Interaction.InteractingAdditionalObjects;
using Managers;
using UnityEngine;

/* Interact 되는 객체
 * [상호작용 물체의 종류]
        2. 오브젝트 생성 / 소멸 - ObjectCreateDelete
    3. 대사 - Script
    4. 선택지 - Choose
5. 이동 - Teleport
6. 애니메이션 - Animation
7. 카메라 워크 - CameraWalk
        8. 아이템 획득, 삭제 - ItemAddDelete
        9. 스트레스 수치 조정 - StressControl
    10. 잠김 - Lock
    11. 이벤트 - 내부 코드로 대응
 */

namespace InGameObjects.Interaction
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class InteractingObject : MonoBehaviour
    {
        [SerializeField] private int idx;
        [SerializeField] private List<int> dataType;
        [SerializeField] private List<int> dataIdx;
        [SerializeField] private List<AbstractInteractionObject> abstractInteractionObjectList;

        [SerializeField] private int currentInteractIndex;
        [SerializeField] private int currentInteractInnerIndex;

        public int Idx
        {
            get => idx;
        }

        private void Awake()
        {
            currentInteractIndex = 0;
        
            dataType.Clear();
            dataIdx.Clear();
            foreach (var data in DataBaseManager.Instance.InteractionDataBase.dataList[idx].typeList)
            {
                dataType.Add(data);
            }
            foreach (var data in DataBaseManager.Instance.InteractionDataBase.dataList[idx].idxList)
            {
                dataIdx.Add(data);
            }
        }

        public void Interact()
        {
        
        }

        private void Interact(int idx)
        {
            // 대사 Script
            if (dataType[idx] == 3)
            {
            
            }
            // 선택지 Choose
            else if (dataType[idx] == 4)
            {
            
            }
            // 잠김 Lock
            else if (dataType[idx] == 10)
            {
            
            }
            // 이벤트 Event
            else if (dataType[idx] == 11)
            {
            
            }
            // InteractionObject 실행
            else
            {
                Debug.Assert(abstractInteractionObjectList.Count == currentInteractIndex + 1);
            
                abstractInteractionObjectList[currentInteractIndex++].Interact();
            }
        }
    }
}
