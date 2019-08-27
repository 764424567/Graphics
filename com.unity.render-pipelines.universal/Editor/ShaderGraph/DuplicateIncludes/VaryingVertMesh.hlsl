﻿#if SHADERPASS == SHADOWCASTER
    float3 _LightDirection;
#endif

Varyings BuildVaryings(Attributes input)
{
    Varyings output = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);

#if defined(HAVE_MESH_MODIFICATION)
    input = ApplyMeshModification(input, _TimeParameters.xyz);
#endif

    // This return the camera relative position (if enable)
    float3 positionWS = TransformObjectToWorld(input.positionOS);

#ifdef ATTRIBUTES_NEED_NORMAL
    float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
#else
    float3 normalWS = float3(0.0, 0.0, 0.0); // We need this case to be able to compile ApplyVertexModification that doesn't use normal.
#endif

#ifdef ATTRIBUTES_NEED_TANGENT
    float4 tangentWS = float4(TransformObjectToWorldDir(input.tangentOS.xyz), input.tangentOS.w);
#endif

    //TODO: change to inline ifdef
     // Do vertex modification in camera relative space (if enable)
#if defined(HAVE_VERTEX_MODIFICATION)
    ApplyVertexModification(input, normalWS, positionWS, _TimeParameters.xyz);
#endif

#ifdef VARYINGS_NEED_POSITION_WS
    output.positionWS = positionWS;
#endif
    
#ifdef VARYINGS_NEED_NORMAL_WS
    #if !SHADER_HINT_NICE_QUALITY
        output.normalWS = normalize(normalWS);
    #else
        output.normalWS = normalWS;
    #endif    
#endif

#ifdef VARYINGS_NEED_TANGENT_WS
    output.tangentWS = normalize(tangentWS);
#endif

#if SHADERPASS == SHADOWCASTER
    //define shadow pass specific clip position for universal 
     output.positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));
    #if UNITY_REVERSED_Z
        output.positionCS.z = min(output.positionCS.z, output.positionCS.w * UNITY_NEAR_CLIP_VALUE);
    #else
        output.positionCS.z = max(output.positionCS.z, output.positionCS.w * UNITY_NEAR_CLIP_VALUE);
    #endif
#elif SHADERPASS == META
    output.positionCS = MetaVertexPosition(input.positionOS, input.uv1, input.uv1, unity_LightmapST, unity_DynamicLightmapST);
#else
    output.positionCS = TransformWorldToHClip(positionWS);
#endif

#if defined(VARYINGS_NEED_TEXCOORD0) || defined(VARYINGS_DS_NEED_TEXCOORD0)
    output.texCoord0 = input.uv0;
#endif
#if defined(VARYINGS_NEED_TEXCOORD1) || defined(VARYINGS_DS_NEED_TEXCOORD1)
    output.texCoord1 = input.uv1;
#endif
#if defined(VARYINGS_NEED_TEXCOORD2) || defined(VARYINGS_DS_NEED_TEXCOORD2)
    output.texCoord2 = input.uv2;
#endif
#if defined(VARYINGS_NEED_TEXCOORD3) || defined(VARYINGS_DS_NEED_TEXCOORD3)
    output.texCoord3 = input.uv3;
#endif

#if defined(VARYINGS_NEED_COLOR) || defined(VARYINGS_DS_NEED_COLOR)
    output.color = input.color;
#endif

#ifdef VARYINGS_NEED_VIEWDIRECTION_WS
    output.viewDirectionWS = _WorldSpaceCameraPos.xyz - positionWS;
#endif

#ifdef VARYINGS_NEED_BITANGENT_WS
    output.bitangentWS = cross(normalWS, tangentWS.xyz) * tangentWS.w;
#endif

#ifdef VARYINGS_NEED_SCREENPOSITION
    output.screenPosition = ComputeScreenPos(output.positionCS, _ProjectionParams.x);
#endif

#if defined(VARYINGS_NEED_LIGHTMAP_OR_SH)
    OUTPUT_LIGHTMAP_UV(input.uv1, unity_LightmapST, output.lightmapUV);
    OUTPUT_SH(normalWS, output.sh);
#endif

#ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
    half3 vertexLight = VertexLighting(positionWS, normalWS);
    half fogFactor = ComputeFogFactor(output.positionCS.z);
    output.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
#endif

#ifdef _MAIN_LIGHT_SHADOWS
    //TODO: delete vertex position inputs
    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
    output.shadowCoord = GetShadowCoord(vertexInput);
#endif


    return output;
}