using DataBaseScripts;

namespace Managers
{
    public class DataBaseManager : Singleton<DataBaseManager>
    {
        public ScriptDataBase scriptDataBase;
        public ChooseDataBase chooseDataBase;
        public ItemControlDataBase itemControlDataBase;
        public StressControlDataBase stressControlDataBase;
        public LockDataBase lockDataBase;
        public InteractionDataBase interactionDataBase;
        public ItemDataBase itemDataBase;
    }
}
