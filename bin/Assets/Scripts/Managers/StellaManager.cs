using System;
using System.Collections.Generic;
using DataBaseScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    [Serializable]
    public class StellaInfo
    {
        public int idx;
        public string name;
        public string spriteName;
        public List<string> descriptionList;
        public int level;
        public int exp;
    }
    public class StellaManager : Singleton<StellaManager>
    {
        [SerializeField] private int firstEquipedStellaIndex;
        [SerializeField] private int secondEquipedStellaIndex;
        [SerializeField] private List<StellaInfo> stellaInfoList;
    
        public void IncrementStella(int idx, int count)
        {
            var currentStellaStaticInfo = DataBaseManager.Instance.stellaDataBase.dataKeyDictionary[idx];
            var currentStellaInfo = _getStellaInfo(idx);
            
            currentStellaInfo.exp += count;
            // Level Up 판정
            int currentLevel = currentStellaInfo.level;
            if (currentStellaInfo.exp >= currentStellaStaticInfo.levelUpCountList[currentLevel] && currentLevel < currentStellaStaticInfo.maxLevel)
            {
                currentStellaInfo.exp -= DataBaseManager.Instance.stellaDataBase.dataKeyDictionary[idx].levelUpCountList[currentLevel];
                currentStellaInfo.level += 1;
            }
        }
        
        public void Load(List<StellaInfo> list)
        {
            stellaInfoList = list;
        }
        
        public List<StellaInfo> GetStellaInfoList()
        {
            return stellaInfoList.ConvertAll(o => o);
        }

        public StellaInfo GetStellaInfoFromIdx(int idx)
        {
            return _getStellaInfo(idx);
        }

        private StellaInfo _getStellaInfo(int idx)
        {
            foreach (var stella in stellaInfoList)
            {
                if (stella.idx == idx)
                    return stella;
            }
            return null;
        }
    }
}