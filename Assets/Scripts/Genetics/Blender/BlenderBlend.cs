using UnityEngine;

namespace FarmJam2026
{
    public class BlenderBlend : MonoBehaviour, IBlenderButton
    {
        private Blender Parent => gameObject.GetComponentInParent<Blender>();

        public void PressTheButton(Player player)
        {
            if (Parent.Content.Count < 2)
            {
                Debug.Log("Not enough mycellium in blender, nothing happens!");
                return;
            }

            Debug.Log("BLEND!");
            var hybrid = Genome.CreateHybrid(Parent.Content);
            SoundManager.Instance.PlaySFX(ESoundSFX.BlenderMix);
            Parent.Content.Clear();
            EventManager.TriggerEvent(EventManager.Events.OnBlend, hybrid);
        }
    }
}
