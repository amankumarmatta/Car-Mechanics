Shader "Custom/SkidMarks" {
    Properties {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NormalMap("NormalMap", 2D) = "bump" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _NormFactor ("Normal Strength", Range (0,1)) = 1
    }
    SubShader {
        Tags { 
            "Queue"="Transparent" 
            "RenderType"="Transparent"
        }
        ZWrite Off
        Blend OneMinusDstColor One
        LOD 200
        HLSLPROGRAM
        // Required to compile gles 2.0 with standard srp library
        #pragma prefer_hlslcc gles
        #pragma exclude_renderers d3d11_9x
        #pragma target 3.0

        #pragma vertex vert
        #pragma fragment frag

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        struct Attributes {
            float4 positionOS   : POSITION;
            float2 uv           : TEXCOORD0;
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        struct Varyings {
            float2 uv           : TEXCOORD0;
            UNITY_VERTEX_OUTPUT_STEREO
        };

        sampler2D _MainTex;
        sampler2D _NormalMap;
        half _NormFactor;
        half _Glossiness;
        half _Metallic;

        Varyings vert(Attributes input) {
            Varyings output = (Varyings)0;
            UNITY_SETUP_INSTANCE_ID(input);
            UNITY_TRANSFER_INSTANCE_ID(input, output);
            output.uv = TRANSFORM_TEX(input.uv, _MainTex);
            output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
            return output;
        }

        half4 frag(Varyings input) : SV_Target {
            half4 c = tex2D (_MainTex, input.uv);
            half3 normal = UnpackNormal(tex2D (_NormalMap, input.uv));
            half3 viewDir = normalize(_WorldSpaceCameraPos - input.positionWS.xyz);
            half3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
            half3 reflectDir = reflect(-lightDir, normal);
            half spec = pow(max(dot(viewDir, reflectDir), 0.0), _Glossiness) * _Metallic;
            return half4(c.rgb + spec, c.a);
        }
        ENDHLSL
    }
    FallBack "Diffuse"
}
