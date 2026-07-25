using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    public class Inventaire : MonoBehaviour
    {
        public GameObject SporeInventaire;
        public GameObject GridInventaire;
        List<GameObject> SporeInInventaire;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            EventManager.m_harvest.AddListener(AddSporesToInv);
            SporeInInventaire = new List<GameObject>();
        }

        void AddSporesToInv(List<Spore> toAdd)
        {
            GameObject instanceSporeInventaire = Instantiate(SporeInventaire, GridInventaire.transform);
            instanceSporeInventaire.GetComponent<InventaireItem>().UpdateCount(toAdd.Count);
            SporeInInventaire.Add(instanceSporeInventaire);
        }

        public void StartPlanting(Spore toPlant)
        {

        }
    }
}
