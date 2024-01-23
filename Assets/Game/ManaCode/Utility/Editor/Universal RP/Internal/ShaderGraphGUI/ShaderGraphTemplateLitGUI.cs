using UnityEngine;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEditor.Rendering;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace UnityEditor.ShaderGraph
{
    using static ShaderGraphTemplateGUI;

    internal class ShaderGraphTemplateLitGUI : ShaderGraphLitGUI
    {
        private readonly MaterialHeaderScopeList _scopeList = new MaterialHeaderScopeList();
        private LitGUI.LitProperties _litProperties;
        private MaterialProperty[] _graphProperties = null;
        private bool _drawSurfaceInputs = false;

        public override void FindProperties(MaterialProperty[] properties)
        {
            base.FindProperties(properties);

            try
            {
                _litProperties = new LitGUI.LitProperties(properties);
                _drawSurfaceInputs = true;
            }
            catch 
            {
                Debug.LogError("Not found shader properties");
                _drawSurfaceInputs = false;
            }

            List<MaterialProperty> graphProperties = new List<MaterialProperty>();
            FieldInfo[] litFields = _litProperties.GetType().GetFields(~BindingFlags.Default);
            PropertyInfo[] baseProperties = typeof(BaseShaderGUI).GetProperties(~BindingFlags.Default);

            foreach (var property in properties)
            {
                bool clear = false;

                foreach (FieldInfo field in litFields)
                {
                    if (field?.GetValue(_litProperties) is MaterialProperty litMaterialProperty)
                    {
                        if (property.name == litMaterialProperty.name)
                        {
                            clear = true;
                            break;
                        }
                    }
                }
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
            if (filter.HasFlag(ExpandableTemplate.SurfaceInputs) && _drawSurfaceInputs)
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
            LitGUI.Inputs(_litProperties, materialEditor, material);
            DrawEmissionProperties(material, true);
            DrawTileOffset(materialEditor, baseMapProp);
        }
        protected override void DrawEmissionProperties(Material material, bool keyword)
        {
            if (emissionMapProp != null && emissionColorProp != null)
                base.DrawEmissionProperties(material, keyword);
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

        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
        {
            if (material == null)
                throw new ArgumentNullException("material");

            if (material.HasProperty("_Emission"))
            {
                material.SetColor("_EmissionColor", material.GetColor("_Emission"));
            }

            base.AssignNewShaderToMaterial(material, oldShader, newShader);

            if (oldShader == null || !oldShader.name.Contains("Legacy Shaders/"))
            {
                SetupMaterialBlendMode(material);
                return;
            }

            SurfaceType surfaceType = SurfaceType.Opaque;
            BlendMode blendMode = BlendMode.Alpha;
            if (oldShader.name.Contains("/Transparent/Cutout/"))
            {
                surfaceType = SurfaceType.Opaque;
                material.SetFloat("_AlphaClip", 1);
            }
            else if (oldShader.name.Contains("/Transparent/"))
            {
                surfaceType = SurfaceType.Transparent;
                blendMode = BlendMode.Alpha;
            }
            material.SetFloat("_Blend", (float)blendMode);

            material.SetFloat("_Surface", (float)surfaceType);
            if (surfaceType == SurfaceType.Opaque)
            {
                material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
            }
            else
            {
                material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            }

            if (oldShader.name.Equals("Standard (Specular setup)"))
            {
                material.SetFloat("_WorkflowMode", (float)LitGUI.WorkflowMode.Specular);
                Texture texture = material.GetTexture("_SpecGlossMap");
                if (texture != null)
                    material.SetTexture("_MetallicSpecGlossMap", texture);
            }
            else
            {
                material.SetFloat("_WorkflowMode", (float)LitGUI.WorkflowMode.Metallic);
                Texture texture = material.GetTexture("_MetallicGlossMap");
                if (texture != null)
                    material.SetTexture("_MetallicSpecGlossMap", texture);
            }
        }
    }
}