using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    [CustomEditor(typeof(LightManager))]
    public class LightManagerEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            LightManager lightManager = (LightManager)target;
            DrawDefaultInspector();
        

            if (GUILayout.Button("Day"))
            {
                Debug.Log("Day");
                lightManager.ChangeLightingSetting(true);
            }

            if (GUILayout.Button("Night"))
            {
                Debug.Log("Night");
                lightManager.ChangeLightingSetting(false);
            }
        }
    }

}
