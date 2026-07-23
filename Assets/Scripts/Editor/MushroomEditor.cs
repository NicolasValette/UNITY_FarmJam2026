using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    [CustomEditor(typeof(Mushroom))]
    public class MushroomEditor : Editor
    {
        private Dictionary<IGene, bool> geneIsExpanded = new Dictionary<IGene, bool>();

        override public void OnInspectorGUI()
        {
            Mushroom mushroom = (Mushroom)target;
            if (mushroom == null)
            {
                EditorGUILayout.LabelField("Mushroom is null..?");
                return;
            }

            DrawDefaultInspector();

            EditorGUILayout.Space(10);
            DrawGeneExpressionValues(mushroom);

            EditorGUILayout.Space(10);
            DrawGenome(mushroom);
        }

        private void DrawGenome(Mushroom mushroom)
        {
            EditorGUILayout.LabelField("GENOME", EditorStyles.boldLabel);
            {
                EditorGUI.indentLevel++;
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Add Gene", GUILayout.Width(100)))
                        {
                            ShowAddGeneMenu(mushroom);
                        }
                        if (GUILayout.Button("Clear Genome", GUILayout.Width(100)))
                        {
                            Undo.RecordObject(mushroom, "Clear genes");
                            mushroom.Genome.Genes.Clear();
                            EditorUtility.SetDirty(mushroom);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
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
                                if (GUILayout.Button("Delete Gene", GUILayout.Width(100)))
                                {
                                    Undo.RecordObject(mushroom, "Delete Gene");
                                    mushroom.Genome.Genes.Remove(gene);
                                    EditorUtility.SetDirty(mushroom);
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
                    var newGene = (IGene)Activator.CreateInstance(type);
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
            if (typeof(UnityEngine.Object).IsAssignableFrom(type))
                return EditorGUILayout.ObjectField(label, (UnityEngine.Object)value, type, true);

            EditorGUILayout.LabelField(label, value?.ToString() ?? "null");
            return value;
        }

        private void DrawGeneExpressionValues(Mushroom mushroom)
        {
            var properties = mushroom.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            bool headerDrawn = false;
            foreach (var prop in properties)
            {
                if (Attribute.IsDefined(prop, typeof(MushroomGeneExpressionAttribute)))
                {
                    if (!headerDrawn)
                    {
                        EditorGUILayout.Space(10);
                        EditorGUILayout.LabelField("Gene Expression", EditorStyles.boldLabel);
                        headerDrawn = true;
                    }

                    EditorGUI.BeginDisabledGroup(true);

                    object val = prop.GetValue(mushroom, null);
                    string label = ObjectNames.NicifyVariableName(prop.Name);

                    if (prop.PropertyType == typeof(float))
                        EditorGUILayout.FloatField(label, val != null ? (float)val : 0f);
                    else if (prop.PropertyType == typeof(int))
                        EditorGUILayout.IntField(label, val != null ? (int)val : 0);
                    else
                        EditorGUILayout.TextField(label, val?.ToString() ?? "null");

                    EditorGUI.EndDisabledGroup();
                }
            }
        }
    }
}
