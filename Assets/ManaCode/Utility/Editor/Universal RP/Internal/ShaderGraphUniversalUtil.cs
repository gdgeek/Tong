using UnityEditor.Rendering.Universal.ShaderGraph;
using Unity.Rendering.Universal;
using UnityEngine;

namespace UnityEditor.ShaderGraph
{
    using static ShaderGraphUtil;
    using static ShaderUtils;

    public static class ShaderGraphUniversalUtil
    {
        public static void CreateShaderGraphTemplate(string shaderName)
            => CreateShaderGraphTemplate(Shader.Find(shaderName));
        public static void CreateShaderGraphTemplate(Shader shader)
        {
            GraphData graphData = GetGraphData(shader);

            foreach (var target in graphData.activeTargets)
            {
                if (target is UniversalTarget universalTarget)
                {
                    universalTarget.customEditorGUI = GetCustomEditorGUI(shader);
                    break;
                }
            }

            CreateShaderGraph(graphData);
        }

        private static string GetCustomEditorGUI(Shader shader)
        {
            ShaderID shaderID = GetShaderID(shader);

            switch (shaderID)
            {
                case ShaderID.SG_Lit:
                    return typeof(ShaderGraphTemplateLitGUI).FullName;
                case ShaderID.SG_Unlit:
                    return typeof(ShaderGraphTemplateUnlitGUI).FullName;
#if UNITY_2022_3_OR_NEWER
                case ShaderID.SG_SpriteLit:
                    return typeof(ShaderGraphTemplateLitSpriteGUI).FullName;
                case ShaderID.SG_SpriteUnlit:
                    return typeof(ShaderGraphTemplateUnlitSpriteGUI).FullName;
                case ShaderID.SG_SpriteCustomLit:
                    return typeof(ShaderGraphTemplateLitSpriteGUI).FullName;
                case ShaderID.SG_Decal:
                    return "";
#endif
                default:
                    return "";
            }
        }
    }
}