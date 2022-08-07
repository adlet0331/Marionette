using System;
using System.IO;
using DataBaseScripts;
using UnityEngine;

namespace Managers
{
    public class DataBaseManager : Singleton<DataBaseManager>
    {
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
            interactionDataBase = Resources.Load(Path.Combine("DataBase", "InteractionDataBase"), typeof(InteractionDataBase)) as InteractionDataBase;
            dollTalkDataBase = Resources.Load(Path.Combine("DataBase", "DollTalkDataBase"), typeof(DollTalkDataBase)) as DollTalkDataBase;
            animaDataBase = Resources.Load(Path.Combine("DataBase", "AnimaDataBase"), typeof(AnimaDataBase)) as AnimaDataBase;
            stellaAbilityDataBase = Resources.Load(Path.Combine("DataBase", "StellaAbilityDataBase"), typeof(StellaAbilityDataBase)) as StellaAbilityDataBase;
            itemDataBase = Resources.Load(Path.Combine("DataBase", "ItemDataBase"), typeof(ItemDataBase)) as ItemDataBase;
        }

        private void loadDataBase()
        {
            interactionDataBase.LoadJson();
            dollTalkDataBase.LoadJson();
            animaDataBase.LoadJson();
            stellaAbilityDataBase.LoadJson();
            itemDataBase.LoadJson();
        }
    }
}
