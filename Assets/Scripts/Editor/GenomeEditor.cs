using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    [CustomEditor(typeof(GenomeData))]
    public class GenomeEditor : Editor
    {
        private Dictionary<IGene, bool> geneIsExpanded = new Dictionary<IGene, bool>();

        override public void OnInspectorGUI()
        {
            GenomeData genome = (GenomeData)target;
            if (genome == null)
            {
                EditorGUILayout.LabelField("Genome is null..?");
                return;
            }

            EditorGUILayout.Space(10);

            genome.GenomeName = EditorGUILayout.TextField("Name",genome.GenomeName);

            EditorGUILayout.LabelField("GENOME", EditorStyles.boldLabel);
            {
                EditorGUI.indentLevel++;
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Add Gene", GUILayout.Width(100)))
                        {
                            ShowAddGeneMenu(genome);
                        }
                        if (GUILayout.Button("Clear Genome", GUILayout.Width(100)))
                        {
                            Undo.RecordObject(genome, "Clear genes");
                            genome.Genes.Clear();
                            EditorUtility.SetDirty(genome);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(5);

                    var genes = genome.Genes;
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
                                if (GUILayout.Button("Delete Gene", GUILayout.Width(100)))
                                {
                                    Undo.RecordObject(genome, "Delete Gene");
                                    genome.Genes.Remove(gene);
                                    EditorUtility.SetDirty(genome);
                                }

                                var fields = gene.GetType()
                                    .GetFields(BindingFlags.Public | BindingFlags.Instance);
                                if (fields.Length == 0)
                                {
                                    EditorGUILayout.HelpBox("No field to display.", MessageType.Info);
                                    return;
                                }

                                EditorGUI.BeginChangeCheck();
                                foreach (var field in fields)
                                {
                                    object fieldValue = field.GetValue(gene);
                                    object newValue = DrawFieldForType(field.Name, field.FieldType, fieldValue);

                                    if (EditorGUI.EndChangeCheck())
                                    {
                                        Undo.RecordObject(target, "Edit Gene");
                                        field.SetValue(gene, newValue);
                                        EditorUtility.SetDirty(target);
                                    }
                                }
                            }
                            EditorGUILayout.EndFoldoutHeaderGroup();
                        }
                        EditorGUI.indentLevel--;
                    }
                }
                EditorGUI.indentLevel--;
            }
        }

        private void ShowAddGeneMenu(GenomeData genome)
        {
            var geneTypes = TypeCache.GetTypesDerivedFrom<IGene>()
                                     .Where(t => !t.IsAbstract && !t.IsInterface);

            GenericMenu menu = new GenericMenu();
            foreach (Type type in geneTypes)
            {
                menu.AddItem(new GUIContent(type.Name), false, () =>
                {
                    Undo.RecordObject(genome, "Add Gene");
                    var newGene = (IGene)Activator.CreateInstance(type);
                    genome.Genes.Add(newGene);
                    EditorUtility.SetDirty(genome);
                });
            }

            if (geneTypes.Count() == 0)
            {
                menu.AddDisabledItem(new GUIContent("No IGene implementation found"));
            }

            menu.ShowAsContext();
        }

        private object DrawFieldForType(string label, System.Type type, object value)
        {
            if (type == typeof(int))
                return EditorGUILayout.IntField(label, (int)(value ?? 0));
            if (type == typeof(float))
                return EditorGUILayout.FloatField(label, (float)(value ?? 0f));
            if (type == typeof(string))
                return EditorGUILayout.TextField(label, (string)value ?? "");
            if (type == typeof(bool))
                return EditorGUILayout.Toggle(label, (bool)(value ?? false));
            if (type == typeof(Color))
                return EditorGUILayout.ColorField(label, (Color)value);
            if (type == typeof(BodyType))
                return EditorGUILayout.EnumPopup(label, (BodyType)value);
            if (type == typeof(ColorName))
                return EditorGUILayout.EnumPopup(label, (ColorName)value);
            if (typeof(UnityEngine.Object).IsAssignableFrom(type))
                return EditorGUILayout.ObjectField(label, (UnityEngine.Object)value, type, true);

            EditorGUILayout.LabelField(label, value?.ToString() ?? "null");
            return value;
        }
    }
}
