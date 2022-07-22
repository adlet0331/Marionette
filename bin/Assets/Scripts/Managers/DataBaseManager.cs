using System;
using System.IO;
using DataBaseScripts;
using UnityEngine;

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
        public DollTalkDataBase dollTalkDataBase;
        public AnimaDataBase animaDataBase;
        public StellaAbilityDataBase stellaAbilityDataBase;
        public ItemDataBase itemDataBase;

        private void Start()
        {
            initializeDataBase();
        }
        private void initializeDataBase()
        {
            scriptDataBase = Resources.Load(Path.Combine("DataBase", "3_ScriptDataBase"), typeof(ScriptDataBase)) as ScriptDataBase;
            chooseDataBase = Resources.Load(Path.Combine("DataBase", "4_ChooseDataBase"), typeof(ChooseDataBase)) as ChooseDataBase;
            itemControlDataBase = Resources.Load(Path.Combine("DataBase", "8_ItemControlDataBase"), typeof(ItemControlDataBase)) as ItemControlDataBase;
            stressControlDataBase = Resources.Load(Path.Combine("DataBase", "9_StressControlDataBase"), typeof(StressControlDataBase)) as StressControlDataBase;
            lockDataBase = Resources.Load(Path.Combine("DataBase", "10_LockDataBase"), typeof(LockDataBase)) as LockDataBase;
            interactionDataBase = Resources.Load(Path.Combine("DataBase", "InteractionDataBase"), typeof(InteractionDataBase)) as InteractionDataBase;
            dollTalkDataBase = Resources.Load(Path.Combine("DataBase", "DollTalkDataBase"), typeof(DollTalkDataBase)) as DollTalkDataBase;
            animaDataBase = Resources.Load(Path.Combine("DataBase", "AnimaDataBase"), typeof(AnimaDataBase)) as AnimaDataBase;
            stellaAbilityDataBase = Resources.Load(Path.Combine("DataBase", "StellaAbilityDataBase"), typeof(StellaAbilityDataBase)) as StellaAbilityDataBase;
            itemDataBase = Resources.Load(Path.Combine("DataBase", "ItemDataBase"), typeof(ItemDataBase)) as ItemDataBase;
        }
    }
}
