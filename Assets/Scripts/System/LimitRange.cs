using System;

namespace FarmJam2026.Assets.Scripts.System
{
    [Serializable]
    public class LimitRange<T>
    {
        public LimitRange(T min, T max)
        {
            Min = min;
            Max = max;
        }

        public T Min;
        public T Max;
    }
}