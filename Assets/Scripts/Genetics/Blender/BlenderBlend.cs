using UnityEngine;

namespace FarmJam2026
{
    public class BlenderBlend : MonoBehaviour, IBlenderButton
    {
        private Blender Parent => gameObject.GetComponentInParent<Blender>();

        public void PressTheButton(Player player)
        {
            Debug.Log("BLEND!");
            var hybrid = Genome.CreateHybrid(Parent.Content);
            Parent.Content.Clear();
            EventManager.TriggerEvent(EventManager.Events.OnBlend, hybrid);
        }
    }
}
