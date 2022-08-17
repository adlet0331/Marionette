using System.Collections.Generic;
using DataBaseScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class StellaManager : Singleton<StellaManager>
    {
        [FormerlySerializedAs("animaAbilityDataBase")] [SerializeField] private StellaDataBase stellaDataBase;
        [SerializeField] private List<int> animaAbilityLevelList;
        [SerializeField] private List<int> animaAbilityCountList;
    
        private void Start()
        {
            animaAbilityLevelList.Clear();
            animaAbilityCountList.Clear();
        
            animaAbilityLevelList.Add(-1);
            animaAbilityCountList.Add(-1);
        
            for (int i = 1; i < stellaDataBase.dataKeyDictionary.Count; i++)
            {
                animaAbilityLevelList.Add(stellaDataBase.dataKeyDictionary[i].initStatus);
                animaAbilityCountList.Add(0);
            }
        }

        public void IncrementAnimaAbility(int idx, int count)
        {
            animaAbilityCountList[idx] += count;
            var currentAbilityInfo = stellaDataBase.dataKeyDictionary[idx];
            int currentLevel = animaAbilityLevelList[idx];
            if (animaAbilityCountList[idx] > currentAbilityInfo.levelUpCountList[currentLevel] && currentLevel < currentAbilityInfo.maxLevel)
            {
                animaAbilityCountList[idx] -= stellaDataBase.dataKeyDictionary[idx].levelUpCountList[currentLevel];
                stellaDataBase.dataKeyDictionary[idx].levelUpCountList[currentLevel] += 1;
            }
        }
    }
}