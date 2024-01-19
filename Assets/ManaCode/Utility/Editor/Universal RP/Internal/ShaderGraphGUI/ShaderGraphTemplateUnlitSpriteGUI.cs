using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

namespace UnityEditor.ShaderGraph
{
    using static ShaderGraphTemplateGUI;

    internal class ShaderGraphTemplateUnlitSpriteGUI : BaseShaderGUI
    {
        protected MaterialProperty[] _graphProperties = null;

        private readonly MaterialHeaderScopeList _scopeList = new MaterialHeaderScopeList();
        private bool _draw = false;

        public override void FindProperties(MaterialProperty[] properties)
        {
            var material = materialEditor?.target as Material;
            if (material == null) return;

            baseMapProp = FindProperty("_MainTex", properties, false);
            baseColorProp = FindProperty("_Color", properties, false);

            List<MaterialProperty> graphProperties = new List<MaterialProperty>();
            List<MaterialProperty> clearProperties = new List<MaterialProperty>()
            {
                baseMapProp, baseColorProp
            };

            if (clearProperties.Any(property => property == null))
            {
                _draw = false;
                return;
            }
            else _draw = true;

            foreach (var property in properties)
            {
                bool clear = false;

                foreach (MaterialProperty clearProperty in clearProperties)
                {
                    if (property.name == clearProperty.name)
                    {
                        clear = true;
                        break;
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
            if (filter.HasFlag(ExpandableTemplate.ShaderGraphInputs))
            {
                _scopeList.RegisterHeaderScope(ShaderGraphInputs, (uint)ExpandableTemplate.ShaderGraphInputs, DrawShaderGraphInputs);
            }
        }

        public override void DrawSurfaceOptions(Material material)
        {
            if (!_draw) return;

            DrawBaseProperties(material);
            DrawTileOffset(materialEditor, baseMapProp);
        }
        private void DrawShaderGraphInputs(Material material)
        {
            DrawShaderGraphProperties(material, _graphProperties);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material material = materialEditor?.target as Material;
            _scopeList.DrawHeaders(materialEditor, material);
            base.OnGUI(materialEditor, properties);
        }
    }
}