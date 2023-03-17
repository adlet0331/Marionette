using System;
using System.IO;
using DataBaseScripts;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class DataBaseCollection
    {
        public SceneInfoDataBase sceneInfoDataBase;
        public InteractionDataBase interactionDataBase;
        public DollTalkDataBase dollTalkDataBase;
        public AnimaDataBase animaDataBase;
        public StellaDataBase stellaDataBase;
        public ItemDataBase itemDataBase;
        public void LoadAllDataBase()
        {
            interactionDataBase = Resources.Load(Path.Combine("DataBase", "InteractionDataBase"), typeof(InteractionDataBase)) as InteractionDataBase;
            dollTalkDataBase = Resources.Load(Path.Combine("DataBase", "DollTalkDataBase"), typeof(DollTalkDataBase)) as DollTalkDataBase;
            animaDataBase = Resources.Load(Path.Combine("DataBase", "AnimaDataBase"), typeof(AnimaDataBase)) as AnimaDataBase;
            stellaDataBase = Resources.Load(Path.Combine("DataBase", "StellaDataBase"), typeof(StellaDataBase)) as StellaDataBase;
            itemDataBase = Resources.Load(Path.Combine("DataBase", "ItemDataBase"), typeof(ItemDataBase)) as ItemDataBase;
            sceneInfoDataBase = Resources.Load(Path.Combine("DataBase", "SceneInfoDataBase"), typeof(SceneInfoDataBase)) as SceneInfoDataBase;
            
            interactionDataBase.LoadJson();
            dollTalkDataBase.LoadJson();
            animaDataBase.LoadJson();
            stellaDataBase.LoadJson();
            itemDataBase.LoadJson();
            sceneInfoDataBase.LoadJson();
        }
    }
}
