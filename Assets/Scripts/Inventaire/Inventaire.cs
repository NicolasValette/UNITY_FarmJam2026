using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    public class Inventaire : MonoBehaviour
    {
        public GameObject SporeInventaire;
        public GameObject GridInventaire;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            EventManager.m_harvest.AddListener(AddSporesToInv);
        }

        void AddSporesToInv(List<Spore> toAdd)
        {
            GameObject instanceSporeInventaire = Instantiate(SporeInventaire, GridInventaire.transform);
            instanceSporeInventaire.GetComponent<InventaireItem>().UpdateCount(toAdd.Count);
        }

        public void StartPlanting(Spore toPlant)
        {

        }
    }
}
