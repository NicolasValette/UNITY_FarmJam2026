using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    [CustomEditor(typeof(Mushroom))]
    public class MushroomEditor : Editor
    {
        private bool genomeIsExpanded = true;
        private Dictionary<IGene, bool> geneIsExpanded = new Dictionary<IGene, bool>();

        override public void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space(10);

            genomeIsExpanded = EditorGUILayout.BeginFoldoutHeaderGroup(genomeIsExpanded, "Genome");
            if (genomeIsExpanded)
            {
                EditorGUI.indentLevel++;
                {
                    Mushroom mushroom = (Mushroom)target;
                    if (mushroom == null)
                    {
                        EditorGUILayout.LabelField("Mushroom is null..?");
                        return;
                    }

                    if (GUILayout.Button("Add Gene", GUILayout.Width(100)))
                    {
                        ShowAddGeneMenu(mushroom);
                    }
                    EditorGUILayout.Space(5);

                    var genes = mushroom.Genome.Genes;
                    if (genes == null || genes.Count == 0)
                    {
                        EditorGUILayout.LabelField("No gene.");
                        return;
                    }

                    for (int i = 0; i < genes.Count; i++)
                    {
                        EditorGUI.indentLevel++;
                        {
                            IGene gene = genes[i];
                            if (gene == null)
                            {
                                EditorGUILayout.LabelField("Gene is null..?");
                                return;
                            }

                            if (!geneIsExpanded.ContainsKey(gene))
                            {
                                geneIsExpanded[gene] = true;
                            }

                            geneIsExpanded[gene] = EditorGUILayout.BeginFoldoutHeaderGroup(geneIsExpanded[gene], gene.GetType().Name);
                            {
                                EditorGUILayout.LabelField("Gene Type: " + gene.GetType().Name);
                            }
                            EditorGUILayout.EndFoldoutHeaderGroup();
                        }
                        EditorGUI.indentLevel--;
                    }
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void ShowAddGeneMenu(Mushroom mushroom)
        {
            var geneTypes = TypeCache.GetTypesDerivedFrom<IGene>()
                                     .Where(t => !t.IsAbstract && !t.IsInterface);

            GenericMenu menu = new GenericMenu();
            foreach (Type type in geneTypes)
            {
                menu.AddItem(new GUIContent(type.Name), false, () =>
                {
                    Undo.RecordObject(mushroom, "Add Gene");
                    IGene newGene = (IGene)Activator.CreateInstance(type);
                    mushroom.Genome.Genes.Add(newGene);
                    EditorUtility.SetDirty(mushroom);
                });
            }

            if (geneTypes.Count() == 0)
            {
                menu.AddDisabledItem(new GUIContent("No IGene implementation found"));
            }

            menu.ShowAsContext();
        }
    }
}
