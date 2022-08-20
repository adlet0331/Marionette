using System.Collections.Generic;
using DataBaseScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class StellaManager : Singleton<StellaManager>
    {
        [FormerlySerializedAs("animaAbilityDataBase")] [SerializeField] private StellaDataBase stellaDataBase;
        [SerializeField] private List<int> stellaLevelList;
        [SerializeField] private List<int> stellaExpCountList;
    
        private void Start()
        {
            stellaLevelList.Clear();
            stellaExpCountList.Clear();
        
            stellaLevelList.Add(-1);
            stellaExpCountList.Add(-1);
        
            for (int i = 1; i < stellaDataBase.dataKeyDictionary.Count; i++)
            {
                stellaLevelList.Add(stellaDataBase.dataKeyDictionary[i].initStatus);
                stellaExpCountList.Add(0);
            }
        }
        
        public List<int> GetStellaLevelList()
        {
            return stellaLevelList.ConvertAll(o => o);
        }
        
        public List<int> GetStellaExpCountList()
        {
            return stellaExpCountList.ConvertAll(o => o);
        }

        public void IncrementStella(int idx, int count)
        {
            stellaExpCountList[idx] += count;
            var currentAbilityInfo = stellaDataBase.dataKeyDictionary[idx];
            int currentLevel = stellaLevelList[idx];
            if (stellaExpCountList[idx] > currentAbilityInfo.levelUpCountList[currentLevel] && currentLevel < currentAbilityInfo.maxLevel)
            {
                stellaExpCountList[idx] -= stellaDataBase.dataKeyDictionary[idx].levelUpCountList[currentLevel];
                stellaDataBase.dataKeyDictionary[idx].levelUpCountList[currentLevel] += 1;
            }
        }
    }
}