using System.Collections.Generic;
using DataBaseScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class AnimaAbilityManager : Singleton<AnimaAbilityManager>
    {
        [FormerlySerializedAs("animaAbilityDataBase")] [SerializeField] private StellaAbilityDataBase stellaAbilityDataBase;
        [SerializeField] private List<int> animaAbilityLevelList;
        [SerializeField] private List<int> animaAbilityCountList;
    
        private void Start()
        {
            animaAbilityLevelList.Clear();
            animaAbilityCountList.Clear();
        
            animaAbilityLevelList.Add(-1);
            animaAbilityCountList.Add(-1);
        
            for (int i = 1; i < stellaAbilityDataBase.dataKeyDictionary.Count; i++)
            {
                animaAbilityLevelList.Add(stellaAbilityDataBase.dataKeyDictionary[i].initStatus);
                animaAbilityCountList.Add(0);
            }
        }

        public void IncrementAnimaAbility(int idx, int count)
        {
            animaAbilityCountList[idx] += count;
            var currentAbilityInfo = stellaAbilityDataBase.dataKeyDictionary[idx];
            int currentLevel = animaAbilityLevelList[idx];
            if (animaAbilityCountList[idx] > currentAbilityInfo.levelUpCount[currentLevel] && currentLevel < currentAbilityInfo.maxLevel)
            {
                animaAbilityCountList[idx] -= stellaAbilityDataBase.dataKeyDictionary[idx].levelUpCount[currentLevel];
                stellaAbilityDataBase.dataKeyDictionary[idx].levelUpCount[currentLevel] += 1;
            }
        }
    }
}