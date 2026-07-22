using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    public class Field : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _mushroomPrefab;

        private bool _isCropFull = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnMouseDown()
        {
            Debug.Log("Hello");
            if (!_isCropFull)
            {
                GameObject mushroom = GameObject.Instantiate(_mushroomPrefab, Vector2.zero, Quaternion.identity, transform);
                mushroom.transform.localPosition = new Vector2 (Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                _isCropFull = true;
            }
        }
    }
}
