using UnityEngine;

namespace FarmJam2026
{
    public class BlenderAdd : MonoBehaviour, IBlenderButton
    {
        private Blender Parent => gameObject.GetComponentInParent<Blender>();

        public void PressTheButton(Player player)
        {
            //Debug.Log("Add spore to blender");
            //var genome = player.SelectedSpore.Spore.Genome;
            //Parent.Content.Add(genome);
            //EventManager.TriggerEvent(EventManager.Events.OnAddToBlender, genome);
        }
        public void AddToBlender(Genome genome)
        {
            Debug.Log("Add spore to blender");
            Parent.Content.Add(genome);
            EventManager.TriggerEvent(EventManager.Events.OnAddToBlender, genome);
        }
    }
}
