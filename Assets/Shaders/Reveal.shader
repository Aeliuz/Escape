Shader "Custom/URPStencilRevealWithAlbedo_VR"
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
            #pragma multi_compile_instancing
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID // For single-pass instancing
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO // For single-pass instancing
            };

            float4 _Color;
            sampler2D _MainTex;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN); // Setup instancing

                // Get the appropriate matrix for single-pass instanced rendering
                float4x4 modelMatrix = GetObjectToWorldMatrix();
                float4x4 viewProjMatrix = UNITY_MATRIX_VP;

                // Transform the position from object space to HClip space (HCS)
                OUT.positionHCS = mul(viewProjMatrix, mul(modelMatrix, IN.positionOS));

                // Pass UVs to fragment shader
                OUT.uv = IN.uv;

                UNITY_TRANSFER_INSTANCE_ID(IN, OUT); // Transfer instance ID for fragment shader

                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(IN); // Setup instancing for the fragment shader

                // Sample the albedo texture
                half4 albedo = tex2D(_MainTex, IN.uv);

                // Combine the albedo with the color
                return albedo * _Color;
            }

            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
