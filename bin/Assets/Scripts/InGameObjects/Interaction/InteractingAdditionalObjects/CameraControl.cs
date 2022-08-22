using System;
using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class CameraControl : IInteractionObject<CameraControlData>
    {
        public override async UniTask<bool> Interact()
        {
            if (data.type == CameraWalkType.FadeOut)
            {
                WindowManager.Instance.FadeInOutBoard.gameObject.SetActive(true);
                float timePassed = 0.0f;
                Color color = WindowManager.Instance.FadeInOutBoard.color;
                color.a = 0.0f;

                while (timePassed <= data.time)
                {
                    color.a = timePassed / data.time;
                    WindowManager.Instance.FadeInOutBoard.color = color;
                    timePassed += Time.fixedDeltaTime * 1000.0f;
                    await UniTask.Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime));
                }
                
                WindowManager.Instance.FadeInOutBoard.gameObject.SetActive(false);
            }
            else if (data.type == CameraWalkType.FadeIn)
            {
                WindowManager.Instance.FadeInOutBoard.gameObject.SetActive(true);
                float timePassed = 0.0f;
                Color color = WindowManager.Instance.FadeInOutBoard.color;
                color.a = 1.0f;

                while (timePassed <= data.time)
                {
                    color.a = 1 - timePassed / data.time;
                    WindowManager.Instance.FadeInOutBoard.color = color;
                    timePassed += Time.fixedDeltaTime * 1000.0f;
                    await UniTask.Delay(TimeSpan.FromSeconds(Time.fixedDeltaTime));
                }
                
                WindowManager.Instance.FadeInOutBoard.gameObject.SetActive(false);
            }
            else if (data.type == CameraWalkType.CameraWalk)
            {
                await CameraManager.Instance.CameraMoveTargetAsync(data.startPoint.localPosition, data.endPoint.localPosition, data.time, Time.fixedDeltaTime);
            }
            return true;
        }
    }
}