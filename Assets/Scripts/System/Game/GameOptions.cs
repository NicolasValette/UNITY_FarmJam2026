using System;
using FarmJam2026.Assets.Scripts.System;
using UnityEngine;

namespace FarmJam2026
{
    public class GameOptions : MonoBehaviour
    {
        public static GameOptions Instance { get; private set;  }

        private void Awake()
        {
            Instance = this;
        }

        [Header("Skills?")]
        [Range(0.0f, 1.0f)]
        public float MutationChance = 0.05f;

        [Header("Genes Options")]
        public LimitRange<float> GrowthTimeLimits = new LimitRange<float>(0.1f, 60f);
        public LimitRange<float> LifeSpanLimits = new LimitRange<float>(2f, 120f);
        public LimitRange<float> ScaleLimits = new LimitRange<float>(1f, 5f);
        public LimitRange<float> SporeGrowthLimits = new LimitRange<float>(0.1f, 30f);
        public LimitRange<int> SporeCountLimits = new LimitRange<int>(0, 2);
        public LimitRange<int> BiomassValueLimits = new LimitRange<int>(0, 200);

        [Header("Misc")]
        public ColorDictionary ColorDico;
    }
}
