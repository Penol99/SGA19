Shader "Custom/shdr_Toon"
{
    Properties
    {
		[HDR]
		_Color("Color",Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
		[HDR]
		_AmbientColor("Ambient Color",Color) = (0.4,0.4,0.4,1)
		[HDR]
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
		_Glossiness("Glossiness", float) = 32
		[HDR]
		_RimColor("Rim Color",Color) = (0.4,0.4,0.4,1)
		_RimAmount("Rim Amount",Range(0,1)) = 0.716
		_RimThreshold("Rim Threshold",Range(0,1)) = 0.1

    }
    SubShader
    {
        Tags { 
		"LightMode"="ForwardBase"
		"PassFlags"="OnlyDirectional"
		}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile_fwdbase
            // make fog work
            #pragma multi_compile_fog
			

            #include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

            struct appdata
            {
				float3 normal : NORMAL;
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
				float4 pos : SV_POSITION;
				float3 viewDir : TEXCOORD1;
                float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
                UNITY_FOG_COORDS(1)
				SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
				
                v2f o;
				
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
				TRANSFER_SHADOW(o)
				
                return o;
            }

			float _RimThreshold;
			float4 _RimColor;
			float _RimAmount;
			float4 _SpecularColor;
			float _Glossiness;
			float4 _AmbientColor;
			float4 _Color;

            fixed4 frag (v2f i) : SV_Target
            {
				
				float3 normal = normalize(i.worldNormal);
				float NdotL = dot(_WorldSpaceLightPos0.xyz,normal);
				float shadow = SHADOW_ATTENUATION(i);
				float lightIntensity = smoothstep(0,0.01,NdotL * shadow);
				float4 light = lightIntensity * _LightColor0;

				float3 viewDir = normalize(i.viewDir);
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(halfVector,normal);
				float specularIntensity = pow(NdotH * lightIntensity,_Glossiness * _Glossiness);
				float specularSmooth = smoothstep(0.005,0.01,specularIntensity);
				float4 specular = specularSmooth * _SpecularColor;

				float4 rimDot = 1 - dot(viewDir,normal);
				float rimIntensity = rimDot * pow(NdotL,_RimThreshold);
				rimIntensity = smoothstep(_RimAmount-0.01,_RimAmount+0.01,rimIntensity);
				float4 rim = rimIntensity * _RimColor;

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col * (_AmbientColor + light + specular + rim);
            }
            ENDCG
        }
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
