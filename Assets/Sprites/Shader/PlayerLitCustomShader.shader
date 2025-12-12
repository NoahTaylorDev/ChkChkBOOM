Shader "Custom/SpriteLit"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
        _SpecularMap("Specular", 2D) = "white" {}
        _EmissionMap("Emission", 2D) = "black" {}
        _GlossMap("Gloss", 2D) = "white" {}
        
        _SpecularStrength("Specular Strength", Range(0, 1)) = 0.2
        _Glossiness("Glossiness", Range(0, 1)) = 0.5
        _EmissionStrength("Emission Strength", Range(0, 3)) = 1.0
        _NormalStrength("Normal Strength", Range(0, 2)) = 1.0
        
        [Toggle] _UseNormalMap("Use Normal Map", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma shader_feature _USENORMALMAP_ON

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float3 positionWS : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
                float4 tangentWS : TEXCOORD3;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);
            TEXTURE2D(_SpecularMap);
            SAMPLER(sampler_SpecularMap);
            TEXTURE2D(_EmissionMap);
            SAMPLER(sampler_EmissionMap);
            TEXTURE2D(_GlossMap);
            SAMPLER(sampler_GlossMap);

            CBUFFER_START(UnityPerMaterial)
                float _SpecularStrength;
                float _Glossiness;
                float _EmissionStrength;
                float _NormalStrength;
            CBUFFER_END

            Varyings vert(Attributes input)
            {
                Varyings output;
                
                VertexPositionInputs posInputs = 
                    GetVertexPositionInputs(input.positionOS.xyz);
                output.positionCS = posInputs.positionCS;
                output.positionWS = posInputs.positionWS;
                
                output.uv = input.uv;
                output.color = input.color;
                
                // Calculate tangent space for 2D sprites
                VertexNormalInputs normInputs = 
                    GetVertexNormalInputs(input.normal, input.tangent);
                output.normalWS = normInputs.normalWS;
                output.tangentWS = float4(normInputs.tangentWS, 
                    input.tangent.w);
                
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                // Sample all textures
                half4 mainTex = SAMPLE_TEXTURE2D(_MainTex, 
                    sampler_MainTex, input.uv);
                half3 specular = SAMPLE_TEXTURE2D(_SpecularMap, 
                    sampler_SpecularMap, input.uv).rgb;
                half3 emission = SAMPLE_TEXTURE2D(_EmissionMap, 
                    sampler_EmissionMap, input.uv).rgb;
                half gloss = SAMPLE_TEXTURE2D(_GlossMap, 
                    sampler_GlossMap, input.uv).r;
                
                // Calculate normal
                float3 normalWS = normalize(input.normalWS);
                
                #ifdef _USENORMALMAP_ON
                    // Sample and unpack normal map
                    half3 normalTS = UnpackNormal(
                        SAMPLE_TEXTURE2D(_NormalMap, 
                        sampler_NormalMap, input.uv));
                    
                    // Apply normal strength
                    normalTS.xy *= _NormalStrength;
                    normalTS.z = sqrt(1.0 - 
                        saturate(dot(normalTS.xy, normalTS.xy)));
                    
                    // Build tangent-to-world matrix
                    float3 tangentWS = normalize(input.tangentWS.xyz);
                    float3 bitangentWS = normalize(
                        cross(normalWS, tangentWS) * input.tangentWS.w);
                    
                    float3x3 tangentToWorld = float3x3(
                        tangentWS,
                        bitangentWS,
                        normalWS
                    );
                    
                    // Transform normal to world space
                    normalWS = normalize(mul(normalTS, tangentToWorld));
                #endif
                
                // Get main light
                Light mainLight = GetMainLight();
                half3 lightDir = normalize(mainLight.direction);
                half3 lightColor = mainLight.color;
                
                // Diffuse lighting with normal
                half NdotL = max(0, dot(normalWS, lightDir));
                half3 diffuse = mainTex.rgb * lightColor * NdotL;
                
                // Ambient light
                half3 ambient = mainTex.rgb * 0.4;
                
                // Specular lighting
                half3 viewDir = normalize(
                    _WorldSpaceCameraPos - input.positionWS);
                half3 halfDir = normalize(lightDir + viewDir);
                half NdotH = max(0, dot(normalWS, halfDir));
                
                // Smoothness from gloss map
                half smoothness = gloss * _Glossiness;
                half specPower = exp2(smoothness * 8.0 + 2.0);
                
                half3 specularLight = specular * 
                    pow(NdotH, specPower) * 
                    _SpecularStrength * 
                    lightColor * 
                    NdotL;
                
                // Emission
                half3 emissive = emission * _EmissionStrength;
                
                // Combine
                half3 finalColor = ambient + diffuse + 
                    specularLight + emissive;
                    
                finalColor *= input.color.rgb;
                
                // Debug: Add red tint to confirm shader is running
                finalColor.r += 0.2;
                
                return half4(finalColor, mainTex.a * input.color.a);
            }
            ENDHLSL
        }
    }
    
    FallBack "Sprites/Default"
}