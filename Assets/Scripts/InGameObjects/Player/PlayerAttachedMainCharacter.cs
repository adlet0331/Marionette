/*
 * 플레이어가 조종해서 움직이는 캐릭터
 * 한 씬에 하나만 존재해야함
 */

using Managers;

namespace InGameObjects.SceneObject.Player
{
    public class PlayerAttachedMainCharacter : PlayerWithHandLight
    {
        private void Start()
        {
            base.MovingObjectStart();
            GamePlayManager.Instance.LoadPlayerAttachedMainCharacter(gameObject);
        }
    }
}
