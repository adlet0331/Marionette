

/* 상호작용 가능한 가장 가까운 오브젝트 정보 띄워주는 창
 * PlayerManager에서 호출
 * 
 * 아래 2개 출력
 * - 오브젝트 이름
 * - InGameObjectManager index = 0 인 String
 */
namespace UI
{
    public class InteractableWindow : WindowObject
    {
        public override void Activate()
        {
            this.gameObject.SetActive(true);
        }

        public new void CloseWindow()
        { 
            this.gameObject.SetActive(false);
        }
    }
}
