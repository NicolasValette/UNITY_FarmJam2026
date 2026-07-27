using System;
using System.Linq;
using UnityEngine;

namespace FarmJam2026
{
    public class BlenderBlend : MonoBehaviour, IBlenderButton
    {
        private Blender Parent => gameObject.GetComponentInParent<Blender>();

        public void PressTheButton(Player player)
        {
            Debug.Log("BLEND!");
            var hybrid = CreateHybrid(Parent.Content.First());
            Parent.Content.Clear();
            EventManager.TriggerEvent(EventManager.Events.OnBlend, hybrid);
        }

        private GenomeData CreateHybrid(GenomeData genome)
        {
            var copy = ScriptableObject.CreateInstance<GenomeData>();
            foreach (var gene in genome.Genes)
            {
                var newGene = (IGene)Activator.CreateInstance(gene.GetType());
                newGene.PerformHybridization(Parent.Content);
                copy.Genes.Add(newGene);
            }
            return copy;
        }
    }
}
