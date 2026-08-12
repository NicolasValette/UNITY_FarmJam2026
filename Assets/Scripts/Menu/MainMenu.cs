using System;
using UnityEngine;

namespace FarmJam2026
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _continueButton;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (SaveGame.Instance.IsSaveExist)
                _continueButton.SetActive(true);
            else 
                _continueButton.SetActive(false);
        }

        
    }
}
