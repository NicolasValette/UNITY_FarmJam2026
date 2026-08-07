using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    public class FirefliesHolder : MonoBehaviour
    {
        [SerializeField]
        private GameObject FireflyPrefab;
        [SerializeField]
        private int _number;
        [field:SerializeField]
        public float Radius { get; set; }

        private List<GameObject> _firefliesList = new List<GameObject>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            for (int i = 0; i< _number;i++)
            {
                var go = Instantiate(FireflyPrefab, transform);
                go.GetComponent<Firefly>().FireffliesHold = this;
                _firefliesList.Add(go);
            }
        }
    }
}
