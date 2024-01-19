using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEditor.ShaderGraph
{
    using Drawing;

    internal static class ShaderGraphTemplateGUI
    {
        readonly internal static GUIContent ShaderGraphInputs = new GUIContent("Shader Graph Inputs",
            "These settings are used to determine the look and feel of the shader graph itself.");

        internal static void DrawTemplateGUI(MaterialEditor materialEditor, IEnumerable<MaterialProperty> properties, IEnumerable<string> clearCategoryName = null)
        {
            if (properties == null) return;

            if (clearCategoryName == null) 
                clearCategoryName = new string[] { "" };

            Material material = materialEditor.target as Material;
            List<MinimalCategoryData> clearCategoryData = new List<MinimalCategoryData>();
            string path = AssetDatabase.GetAssetPath(material.shader);
            ShaderGraphMetadata metadata = null;

            foreach (var obj in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                if (obj is ShaderGraphMetadata meta)
                {
                    metadata = meta;
                    break;
                }
            }
            foreach (var category in metadata.categoryDatas)
            {
                bool clear = false;

                foreach (var clearName in clearCategoryName)
                {
                    if (category.categoryName == clearName)
                    {
                        clear = true;
                        break;
                    }
                }

                if (!clear) clearCategoryData.Add(category);
            }

            if (metadata != null)
            {
                ShaderGraphPropertyDrawers.DrawShaderGraphGUI(materialEditor, properties, clearCategoryData);
            }
            else
            {
                MethodInfo info = typeof(ShaderGraphPropertyDrawers).GetMethod("PropertiesDefaultGUI", ~BindingFlags.Default);
                info?.Invoke(null, new object[] { materialEditor, properties });
            }
        }
    }
}