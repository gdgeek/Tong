using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEditor.ShaderGraph
{
    internal class ShaderGraphTemplateLitSpriteGUI : ShaderGraphTemplateUnlitSpriteGUI
    {
        private MaterialProperty _normalMap, _normalScale, _mask;
        private bool _draw = false;

        readonly static GUIContent SpriteMask = new GUIContent("Mask",
            "Sprite Masks are used to either hide or reveal parts of a Sprite.");

        public override void FindProperties(MaterialProperty[] properties)
        {
            var material = materialEditor?.target as Material;
            if (material == null) return;

            baseMapProp = FindProperty("_MainTex", properties, false);
            baseColorProp = FindProperty("_Color", properties, false);

            _normalMap = FindProperty("_BumpMap", properties, false);
            _normalScale = FindProperty("_BumpScale", properties, false);
            _mask = FindProperty("_MaskTex", properties, false);

            List<MaterialProperty> graphProperties = new List<MaterialProperty>();
            List<MaterialProperty> clearProperties = new List<MaterialProperty>()
            {
                baseMapProp, baseColorProp, _normalMap, _normalScale, _mask
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

        public override void DrawSurfaceOptions(Material material)
        {
            if (!_draw) return;

            DrawBaseProperties(material);
            DrawNormalArea(materialEditor, _normalMap, _normalScale);
            materialEditor.TexturePropertySingleLine(SpriteMask, _mask);
            DrawTileOffset(materialEditor, baseMapProp);
        }
    }
}