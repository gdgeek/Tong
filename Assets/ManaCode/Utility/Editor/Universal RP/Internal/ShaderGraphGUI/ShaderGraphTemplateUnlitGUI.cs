using UnityEngine;
using UnityEditor.Rendering;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEditor.ShaderGraph
{
    using static ShaderGraphTemplateGUI;

    internal class ShaderGraphTemplateUnlitGUI : ShaderGraphUnlitGUI
    {
        private readonly MaterialHeaderScopeList _scopeList = new MaterialHeaderScopeList();
        private MaterialProperty[] _graphProperties = null;

        public override void FindProperties(MaterialProperty[] properties)
        {
            base.FindProperties(properties);

            List<MaterialProperty> graphProperties = new List<MaterialProperty>();
            PropertyInfo[] baseProperties = typeof(BaseShaderGUI).GetProperties(~BindingFlags.Default);

            foreach (var property in properties)
            {
                bool clear = false;

                foreach (PropertyInfo baseProperty in baseProperties)
                {
                    if (baseProperty?.GetValue(this) is MaterialProperty baseMaterialProperty)
                    {
                        if (property.name == baseMaterialProperty.name)
                        {
                            clear = true;
                            break;
                        }
                    }
                }

                if (!clear) graphProperties.Add(property);
            }

            _graphProperties = graphProperties.ToArray();
        }

        public override void OnOpenGUI(Material material, MaterialEditor materialEditor)
        {
            var filter = (ExpandableTemplate)materialFilter;

            if (filter.HasFlag(ExpandableTemplate.SurfaceOptions))
            {
                _scopeList.RegisterHeaderScope(Styles.SurfaceOptions, (uint)ExpandableTemplate.SurfaceOptions, DrawSurfaceOptions);
            }
            if (filter.HasFlag(ExpandableTemplate.SurfaceInputs))
            {
                _scopeList.RegisterHeaderScope(Styles.SurfaceInputs, (uint)ExpandableTemplate.SurfaceInputs, DrawSurfaceInputs);
            }
            if (filter.HasFlag(ExpandableTemplate.ShaderGraphInputs))
            {
                _scopeList.RegisterHeaderScope(ShaderGraphInputs, (uint)ExpandableTemplate.ShaderGraphInputs, DrawShaderGraphInputs);
            }
            if (filter.HasFlag(ExpandableTemplate.Advanced))
            {
                _scopeList.RegisterHeaderScope(Styles.AdvancedLabel, (uint)ExpandableTemplate.Advanced, DrawAdvancedOptions);
            }
        }

        public override void DrawSurfaceInputs(Material material)
        {
            DrawBaseProperties(material);
            DrawTileOffset(materialEditor, baseMapProp);
        }
        private void DrawShaderGraphInputs(Material material)
        {
            string[] clearCategoryName = new string[]
            {
                Styles.SurfaceOptions.text,
                Styles.SurfaceInputs.text,
                Styles.AdvancedLabel.text
            };

            DrawTemplateGUI(materialEditor, _graphProperties, clearCategoryName);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material material = materialEditor?.target as Material;
            base.OnGUI(materialEditor, properties);
            _scopeList.DrawHeaders(materialEditor, material);
        }
    }
}