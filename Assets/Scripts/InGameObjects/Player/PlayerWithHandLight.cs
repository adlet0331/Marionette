using InGameObjects.Object;
using UnityEngine;
using UnityEngine.Rendering.Universal;


namespace InGameObjects.SceneObject.Player
{
    /// <summary>
    /// 손전등을 들고 있는, 플레이어가 조종하는 Moving Object
    /// </summary>
    public class PlayerWithHandLight : MovingObject
    {
        [SerializeField] private Light2D handLight;

        public void UpdateHandLightRotate(float zRotate)
        {
            if (!handLight)
                return;
            handLight.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotate));
        }
        
        private void Start()
        {
            base.MovingObjectStart();
        }
    }
}