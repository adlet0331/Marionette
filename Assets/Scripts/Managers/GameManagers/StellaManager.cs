using System;
using System.Collections.Generic;
using UnityEngine;

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
    [Serializable]
    public class StellaManager : AGameManager
    {
        [SerializeField] private int firstEquipedStellaIndex;
        [SerializeField] private int secondEquipedStellaIndex;
        [SerializeField] private List<StellaInfo> stellaInfoList;
        public List<StellaInfo> StellaInfoList => stellaInfoList.ConvertAll(o => o);
    
        public void IncrementStella(int idx, int count)
        {
            var currentStellaStaticInfo = GamePlayManager.Instance.dataBaseCollection.stellaDataBase.dataKeyDictionary[idx];
            var currentStellaInfo = _getStellaInfo(idx);
            
            currentStellaInfo.exp += count;
            // Level Up 판정
            int currentLevel = currentStellaInfo.level;
            if (currentStellaInfo.exp >= currentStellaStaticInfo.levelUpCountList[currentLevel] && currentLevel < currentStellaStaticInfo.maxLevel)
            {
                currentStellaInfo.exp -= GamePlayManager.Instance.dataBaseCollection.stellaDataBase.dataKeyDictionary[idx].levelUpCountList[currentLevel];
                currentStellaInfo.level += 1;
            }
        }
        
        public List<StellaInfo> InitStellaInfoList()
        {
            stellaInfoList = new List<StellaInfo>();
            var stellaDataBaseDict = GamePlayManager.Instance.dataBaseCollection.stellaDataBase.dataKeyDictionary;
            foreach (var stellaData in stellaDataBaseDict)
            {
                var newStellaInfo = new StellaInfo();
                newStellaInfo.idx = stellaData.Value.idx;
                newStellaInfo.name = stellaData.Value.name;
                newStellaInfo.spriteName = stellaData.Value.spriteName;
                newStellaInfo.descriptionList = stellaData.Value.descriptionList;
                newStellaInfo.level = 0;
                newStellaInfo.exp = 0;
                stellaInfoList.Add(newStellaInfo);
            }
            return new List<StellaInfo>(stellaInfoList);
        }

        public void LoadStellaInfoList(List<StellaInfo> pStellaInfoList)
        {
            stellaInfoList = pStellaInfoList;
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
        public override void Start()
        {
            
        }
    }
}