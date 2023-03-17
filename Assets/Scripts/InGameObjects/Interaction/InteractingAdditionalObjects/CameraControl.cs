using System;
using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class CameraControl : ADataInteractionObject<CameraControlData>
    {
        public override async UniTask<bool> Interact()
        {
            if (data.type == CameraWalkType.FadeOut)
            {
                GamePlayManager.Instance.WindowsInstances.fadeInOutBoard.gameObject.SetActive(true);
                float timePassed = 0.0f;
                Color color = GamePlayManager.Instance.WindowsInstances.fadeInOutBoard.color;
                color.a = 0.0f;

                while (timePassed <= data.time)
                {
                    color.a = timePassed / data.time;
                    GamePlayManager.Instance.WindowsInstances.fadeInOutBoard.color = color;
                    timePassed += Time.fixedDeltaTime * 1000.0f;
                    await UniTask.Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime));
                }
                
                GamePlayManager.Instance.WindowsInstances.fadeInOutBoard.gameObject.SetActive(false);
            }
            else if (data.type == CameraWalkType.FadeIn)
            {
                GamePlayManager.Instance.WindowsInstances.fadeInOutBoard.gameObject.SetActive(true);
                float timePassed = 0.0f;
                Color color = GamePlayManager.Instance.WindowsInstances.fadeInOutBoard.color;
                color.a = 1.0f;

                while (timePassed <= data.time)
                {
                    color.a = 1 - timePassed / data.time;
                    GamePlayManager.Instance.WindowsInstances.fadeInOutBoard.color = color;
                    timePassed += Time.fixedDeltaTime * 1000.0f;
                    await UniTask.Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime));
                }
                
                GamePlayManager.Instance.WindowsInstances.fadeInOutBoard.gameObject.SetActive(false);
            }
            else if (data.type == CameraWalkType.CameraWalk)
            {
                await GamePlayManager.Instance.CameraMoveTargetAsync(data.startPoint.localPosition, data.endPoint.localPosition, data.time, Time.fixedDeltaTime);
            }
            return true;
        }
    }
}