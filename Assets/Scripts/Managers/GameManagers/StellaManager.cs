using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections.Generic;
using UnityEngine.Serialization;

namespace Managers
{
    [Serializable]
    public class StellaInfo
    {
        public string name;
        public string spriteName;
        public List<string> descriptionList;
        public int level;
        public int exp;
    }
    [Serializable]
    public class StellaManager : AGameManager
    {
        [SerializeField] private int firstEquipedStellaIdx;
        [SerializeField] private int secondEquipedStellaIdx;
        [FormerlySerializedAs("stellaInfoDictionary")]
        [FormerlySerializedAs("stellaInfoList")]
        [Header("DO NOT Control UDictionary MANUALLY")]
        [SerializeField] private UDictionary<int, StellaInfo> stellaInfoUDictionary;
        public Dictionary<int, StellaInfo> StellaInfoDictionary => new Dictionary<int, StellaInfo>(stellaInfoUDictionary);
    
        public void IncrementStella(int idx, int count)
        {
            var currentStellaStaticInfo = GamePlayManager.Instance.dataBaseCollection.stellaDataBase.dataKeyDictionary[idx];
            var currentStellaInfo = stellaInfoUDictionary[idx];
            
            currentStellaInfo.exp += count;
            // Level Up 판정
            int currentLevel = currentStellaInfo.level;
            if (currentStellaInfo.exp >= currentStellaStaticInfo.levelUpCountList[currentLevel] && currentLevel < currentStellaStaticInfo.maxLevel)
            {
                currentStellaInfo.exp -= GamePlayManager.Instance.dataBaseCollection.stellaDataBase.dataKeyDictionary[idx].levelUpCountList[currentLevel];
                currentStellaInfo.level += 1;
            }
        }
        
        public Dictionary<int, StellaInfo> InitStellaInfoList()
        {
            stellaInfoUDictionary = new UDictionary<int, StellaInfo>();
            var stellaDataBaseDict = GamePlayManager.Instance.dataBaseCollection.stellaDataBase.dataKeyDictionary;
            foreach (var stellaData in stellaDataBaseDict)
            {
                var newStellaInfo = new StellaInfo();
                newStellaInfo.name = stellaData.Value.name;
                newStellaInfo.spriteName = stellaData.Value.spriteName;
                newStellaInfo.descriptionList = stellaData.Value.descriptionList;
                newStellaInfo.level = 0;
                newStellaInfo.exp = 0;
                stellaInfoUDictionary[stellaData.Value.idx] = newStellaInfo;
            }
            return new Dictionary<int, StellaInfo>(stellaInfoUDictionary);
        }

        public void LoadStellaInfoDictionary(Dictionary<int, StellaInfo> pStellaInfoList)
        {
            stellaInfoUDictionary = new UDictionary<int, StellaInfo>(pStellaInfoList);
        }
        public override void Start()
        {
            
        }
    }
}