using UnityEngine;

namespace Managers
{
    public class StressManager : Singleton<StressManager>
    {
        [SerializeField] private float _stress = 0;
        public float Stress => _stress;

        public float AddStress(float val)
        {
            _stress += val;
            return _stress;
        }
    }
}
