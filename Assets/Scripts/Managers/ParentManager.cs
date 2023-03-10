namespace Managers
{
    public class ParentManager : Singleton<ParentManager>
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
