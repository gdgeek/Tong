using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using System;

using static UnityEditor.Rendering.BuiltIn.ShaderUtils;

namespace UnityEditor.ShaderGraph
{
    public static class ShaderGraphUtil
    {
        internal readonly static string FirstNameShaderGraph = $"New Shader Graph Template.{ShaderGraphImporter.Extension}";

        internal static GraphData GetGraphData(Shader shader)
        {
            if (!GraphUtil.IsShaderGraphAsset(shader))
                throw new Exception("This shader is not a shader graph");

            string pathShader = AssetDatabase.GetAssetPath(shader);
            string textShader = FileUtilities.SafeReadAllText(pathShader);

            GraphData graphData = new GraphData();
            MultiJson.Deserialize(graphData, textShader);

            return graphData;
        }

        public static void CreateShaderGraphTemplate(string shaderName)
            => CreateShaderGraphTemplate(Shader.Find(shaderName));
        public static void CreateShaderGraphTemplate(Shader shader)
        {
            GraphData graphData = GetGraphData(shader);

            foreach (var target in graphData.activeTargets)
            {
                if (target is BuiltInTarget builtInTarget)
                {
                    builtInTarget.customEditorGUI = GetCustomEditorGUI(shader);
                    break;
                }
            }

            CreateShaderGraph(graphData);
        }

        internal static void CreateShaderGraph(GraphData graphData)
        {
            ShaderGraphTemplate shaderGraph = ScriptableObject.CreateInstance<ShaderGraphTemplate>();
            shaderGraph.GraphData = graphData;

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, shaderGraph, FirstNameShaderGraph, null, null);
        }

        private static string GetCustomEditorGUI(Shader shader)
        {
            ShaderID shaderID = GetShaderID(shader);

            switch (shaderID)
            {
                case ShaderID.SG_Lit:
                    return typeof(BuiltInBaseShaderGraphTemplateGUI).FullName;
                case ShaderID.SG_Unlit:
                    return typeof(BuiltInBaseShaderGraphTemplateGUI).FullName;
                default:
                    return "";
            }
        }
    }
}