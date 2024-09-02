Shader "Custom/URPStencilRevealDebug"
{
    Properties
    {
        _Color ("Color", Color) = (1, 0, 0, 1) // Red for debugging
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
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            float4 _Color;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                return _Color;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
