﻿using UnityEditor.ShaderGraph.Internal;

namespace UnityEditor.ShaderGraph
{
    static class PreviewTargetResources
    {
        public static SubShaderDescriptor PreviewSubShader = new SubShaderDescriptor()
        {
            renderQueueOverride = "Geometry",
            renderTypeOverride = "Opaque",
            passes = new PassCollection { PreviewPass },
        };

        public static PassDescriptor PreviewPass = new PassDescriptor()
        {
            // Definition
            referenceName = "SHADERPASS_PREVIEW",
            useInPreview = true,

            // Collections
            structs = new StructCollection
            {
                { Structs.Attributes },
                { PreviewVaryings },
                { Structs.SurfaceDescriptionInputs },
                { Structs.VertexDescriptionInputs },
            },
            fieldDependencies = FieldDependencies.Default,
            pragmas = new PragmaCollection
            {
                { Pragma.Vertex("vert") },
                { Pragma.Fragment("frag") },
            },
            defines = new DefineCollection
            {
                { PreviewKeyword, 1 },
            },
            includes = new IncludeCollection
            {
                // Pre-graph
                { "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl", IncludeLocation.Pregraph },
                { "Packages/com.unity.render-pipelines.core/ShaderLibrary/Packing.hlsl", IncludeLocation.Pregraph },
                { "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl", IncludeLocation.Pregraph },
                { "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl", IncludeLocation.Pregraph },
                { "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl", IncludeLocation.Pregraph },
                { "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl", IncludeLocation.Pregraph },
                { "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariables.hlsl", IncludeLocation.Pregraph },
                { "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl", IncludeLocation.Pregraph },
                { "Packages/com.unity.shadergraph/ShaderGraphLibrary/Functions.hlsl", IncludeLocation.Pregraph },

                // Post-graph
                { "Packages/com.unity.shadergraph/ShaderGraphLibrary/PreviewVaryings.hlsl", IncludeLocation.Postgraph },
                { "Packages/com.unity.shadergraph/ShaderGraphLibrary/PreviewPass.hlsl", IncludeLocation.Postgraph },
            }
        };

        public static StructDescriptor PreviewVaryings = new StructDescriptor()
        {
            name = "Varyings",
            packFields = true,
            fields = new FieldDescriptor[]
            {
                StructFields.Varyings.positionCS,
                StructFields.Varyings.positionWS,
                StructFields.Varyings.normalWS,
                StructFields.Varyings.tangentWS,
                StructFields.Varyings.texCoord0,
                StructFields.Varyings.texCoord1,
                StructFields.Varyings.texCoord2,
                StructFields.Varyings.texCoord3,
                StructFields.Varyings.color,
                StructFields.Varyings.viewDirectionWS,
                StructFields.Varyings.bitangentWS,
                StructFields.Varyings.screenPosition,
                StructFields.Varyings.instanceID,
                StructFields.Varyings.cullFace,
            }
        };

        public static KeywordDescriptor PreviewKeyword = new KeywordDescriptor()
        {
            displayName = "Preview",
            referenceName = "SHADERGRAPH_PREVIEW",
            type = KeywordType.Boolean,
            definition = KeywordDefinition.MultiCompile,
            scope = KeywordScope.Global,
        };
    }
}
