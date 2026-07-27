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
        float gridSizeX, gridSizeY;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SporeInInventaire = new List<GameObject>();
            //c'est en dur le temps du merge, je repasse dessus des que possible parceque c'est moche a souhait mais il est 22h30 et je taf demain :c
            gridSizeX = -0.40f;
            gridSizeY = 0.40f ;
        }
        private void OnEnable()
        {
            EventManager.StartListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
        }

        private void OnDisable()
        {
            EventManager.StopListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
        }

        void AddSporesToInv(List<Spore> toAdd)
        {
            Vector2 newpos = new Vector2(gridSizeX + (SporeInInventaire.Count % 5) / 5f,gridSizeY- (SporeInInventaire.Count/5)/5f);
           
            GameObject instanceSporeInventaire = Instantiate(SporeInventaire, GridInventaire.transform);
            instanceSporeInventaire.transform.localPosition = newpos;
            instanceSporeInventaire.GetComponent<InventaireItem>().UpdateCount(toAdd.Count);
            SporeInInventaire.Add(instanceSporeInventaire);
        }

        public void StartPlanting(Spore toPlant)
        {

        }
    }
}
