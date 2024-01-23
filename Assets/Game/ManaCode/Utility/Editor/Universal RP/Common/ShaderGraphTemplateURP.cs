using UnityEditor.ShaderGraph;

namespace ManaCode.UniversalRP.Utility
{
    public static class ShaderGraphTemplateURP
    {
        public static void Create(string shaderName)
            => ShaderGraphUniversalUtil.CreateShaderGraphTemplate(shaderName);
    }
}