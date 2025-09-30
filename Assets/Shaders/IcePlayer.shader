//Estructura fundamental
Shader "Universal Render Pipeline/IcePlayer"
{
    Properties
    {
       m_color ("Color", Color) = (1,1,1,1)
       m_birghtness ("Brillo", Float) = 1
    }

    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
            // "Queue"="Transparent"
        }

        Pass
        {
            Tags { "LightMode"="SRPDefaultUnlit" }
            //Blend SrcAlpha OneMinusSrcAlpha (Para permitir ransparencias)
            
            HLSLPROGRAM

            cbuffer UnityPerMaterial
            {
                float4 m_color;
                float m_birghtness;
            };

            #pragma vertex Vert
            #pragma fragment Frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            //Mis shaders
            #include "Assets\Shaders\IcePlayer.cginc" 

            struct Attributes
            {
                float4 positionOS : POSITION;
                // Solo agrega lo que necesites:
                // float2 uv : TEXCOORD0;          // Para texturas
                // float3 normalOS : NORMAL;       // Para iluminación  
                // float4 color : COLOR;           // Para vertex colors
            };
            
            struct Varyings
            {
                float4 positionHCS : SV_POSITION;  // Obligatorio
                // Solo agrega lo que uses:
                // float2 uv : TEXCOORD0;             // UVs interpoladas
                // float3 normalWS : TEXCOORD1;       // Normales
                // float3 worldPos : TEXCOORD2;       // Posición mundial
            };

            Varyings Vert(Attributes input)
            {
                Varyings output;
                
                // Transformación básica (siempre necesaria)
                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                
                // Agrega solo lo que necesites:
                // output.uv = input.uv;                                    
                // output.normalWS = TransformObjectToWorldNormal(input.normalOS); 
                // output.worldPos = TransformObjectToWorld(input.positionOS.xyz); 
                
                return output;
            }

            float4 Frag(Varyings input) : SV_TARGET
            {
                return Brightness(m_color, m_birghtness);
            }
            
            ENDHLSL
        }
    }

}