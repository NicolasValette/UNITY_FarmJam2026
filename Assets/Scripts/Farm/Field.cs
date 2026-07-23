using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    public class Field : MonoBehaviour, IField
    {
        [SerializeField] 
        private GameObject _mushroomPrefab;

        private bool _isCropFull = false;
       
        public void PlantCrop()
        {
            if (!_isCropFull)
            {
                GameObject mushroom = GameObject.Instantiate(_mushroomPrefab, Vector2.zero, Quaternion.identity, transform);
                mushroom.transform.localPosition = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                _isCropFull = true;
            }
        }
    }
}
