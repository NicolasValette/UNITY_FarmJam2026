using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    public class Field : MonoBehaviour, IField
    {
        
        private bool _isCropFull = false;
       
        public void PlantCrop(Mushroom mushroom)
        {
            if (!_isCropFull)
            {
                GameObject mushroomGO = GameObject.Instantiate(mushroom.gameObject, Vector2.zero, Quaternion.identity, transform);
                mushroomGO.transform.localPosition = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                _isCropFull = true;
            }
        }
    }
}
