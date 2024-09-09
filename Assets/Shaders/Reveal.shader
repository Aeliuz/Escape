Shader "Custom/URPStencilRevealWithAlbedo"
{
    Properties
    {
        _Color ("Color", Color) = (1, 0, 0, 1) // Red for debugging
        _MainTex ("Albedo (RGB)", 2D) = "white" {} // Albedo texture
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }

        Stencil
        {
            Ref 1
            Comp Equal
        }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            Cull Back
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0; // Add UVs for texture sampling
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color;
            sampler2D _MainTex;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv; // Pass UVs to fragment shader
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                half4 albedo = tex2D(_MainTex, IN.uv); // Sample the albedo texture
                return albedo * _Color; // Combine the albedo with the color
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
