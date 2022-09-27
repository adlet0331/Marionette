using Cysharp.Threading.Tasks;
using DataBaseScripts;
using InGameObjects.Scene;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public sealed class MoveControl : ADataInteractionObject<MoveControlData>
    {
        public override async UniTask<bool> Interact()
        {
            if (SceneManager.GetActiveScene().name == data.destinationScene.ToString())
            {
                var sceneMovePoints = GameObject.FindGameObjectsWithTag("SceneMovePoint");
                Debug.Assert(sceneMovePoints.Length != 0, "sceneMovePoints.Length is 0");
                foreach (var sceneMovePointobj in sceneMovePoints)
                {
                    if (sceneMovePointobj.GetComponent<SceneMovePoint>().idx == data.idx)
                    {
                        PlayerManager.Instance.moveablePlayerObject.transform.localPosition = sceneMovePointobj.gameObject.transform.localPosition;
                        return true;
                    }
                }
            }
            else
            {
                await SceneSwitchManager.Instance.SwitchScene(data.destinationScene, data.idx);
            }
            return true;
        }
    }
}