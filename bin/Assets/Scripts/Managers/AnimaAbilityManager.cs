using System.Collections.Generic;
using DataBaseScripts;
using UnityEngine;

namespace Managers
{
    public class AnimaAbilityManager : Singleton<AnimaAbilityManager>
    {
        [SerializeField] private AnimaAbilityDataBase animaAbilityDataBase;
        [SerializeField] private List<int> animaAbilityLevelList;
        [SerializeField] private List<int> animaAbilityCountList;
    
        private void Start()
        {
            animaAbilityLevelList.Clear();
            animaAbilityCountList.Clear();
        
            animaAbilityLevelList.Add(-1);
            animaAbilityCountList.Add(-1);
        
            for (int i = 1; i < animaAbilityDataBase.dataList.Count; i++)
            {
                animaAbilityLevelList.Add(animaAbilityDataBase.dataList[i].initStatus);
                animaAbilityCountList.Add(0);
            }
        }

        public void IncrementAnimaAbility(int idx, int count)
        {
            animaAbilityCountList[idx] += count;
            var currentAbilityInfo = animaAbilityDataBase.dataList[idx];
            int currentLevel = animaAbilityLevelList[idx];
            if (animaAbilityCountList[idx] > currentAbilityInfo.levelUpCount[currentLevel] && currentLevel < currentAbilityInfo.maxLevel)
            {
                animaAbilityCountList[idx] -= animaAbilityDataBase.dataList[idx].levelUpCount[currentLevel];
                animaAbilityDataBase.dataList[idx].levelUpCount[currentLevel] += 1;
            }
        }
    }
}