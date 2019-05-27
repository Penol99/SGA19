Shader "Custom/shdr_Diffuse"
{
	Properties{
		_Colour ("Color",Color) = (1,0,0,1)
	}

    SubShader
    {
        Tags { "LightMode" = "ForwardBase" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				float3 worldNormal : NORMAL;
            };

			fixed4 _Colour;

            v2f vert (appdata v)
            {
                v2f o;
				float3 worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldNormal = worldNormal;
                o.vertex = UnityObjectToClipPos(v.vertex);

				
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float3 normalizedNormal = normalize(i.worldNormal);
				float NdotL = max(0.0,dot(normalizedNormal, _WorldSpaceLightPos0.xyz));
				NdotL = smoothstep(0,0.01,NdotL);//NdotL > 0 ? 1 : 0;

				float4 fragColor = (_Colour * NdotL * _LightColor0);

				return fragColor;


            }
            ENDCG
        }
    }
}
