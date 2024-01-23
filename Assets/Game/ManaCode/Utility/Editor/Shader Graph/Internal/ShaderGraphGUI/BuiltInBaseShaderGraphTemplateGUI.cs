using UnityEditor.Rendering;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;

namespace UnityEditor.ShaderGraph
{
    using static ShaderGraphTemplateGUI;

    internal class BuiltInBaseShaderGraphTemplateGUI : BuiltInBaseShaderGUI
    {
        private readonly MaterialHeaderScopeList _scopeList = new MaterialHeaderScopeList();
        private MaterialEditor _materialEditor = null;
        private MaterialProperty[] _properties = null;

        protected virtual uint materialFilter => uint.MaxValue;

        public override void OnOpenGUI(Material material, MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            _scopeList.RegisterHeaderScope(Styles.SurfaceOptions, (uint)ExpandableTemplate.SurfaceOptions, DrawSurfaceOptions);
            _scopeList.RegisterHeaderScope(Styles.SurfaceInputs, (uint)ExpandableTemplate.SurfaceInputs, DrawSurfaceInputs);
            _scopeList.RegisterHeaderScope(ShaderGraphInputs, (uint)ExpandableTemplate.ShaderGraphInputs, DrawShaderGraphInputs);
            _scopeList.RegisterHeaderScope(Styles.AdvancedLabel, (uint)ExpandableTemplate.Advanced, DrawAdvancedOptions);
        }

        protected override void DrawSurfaceInputs(Material material)
        {
            string[] clearCategoryName = new string[]
            {
                ""
            };

            DrawTemplateGUI(_materialEditor, _properties, clearCategoryName);
        }

        private void DrawShaderGraphInputs(Material material)
        {
            string[] clearCategoryName = new string[]
            {
                Styles.SurfaceOptions.text,
                Styles.SurfaceInputs.text,
                Styles.AdvancedLabel.text
            };

            DrawTemplateGUI(_materialEditor, _properties, clearCategoryName);
        }

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            _materialEditor = materialEditor;
            _properties = properties;

            Material material = materialEditor?.target as Material;
            _scopeList.DrawHeaders(materialEditor, material);
            base.OnGUI(materialEditor, properties);
        }
    }
}