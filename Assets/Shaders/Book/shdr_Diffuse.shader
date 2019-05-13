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
				float3 worldNormal : TEXCOOD0;
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
                // sample the texture
				float3 normalDirection = normalize(i.worldNormal);
				float nl = max(0.0,dot(normalDirection, _WorldSpaceLightPos0.xyz));

                float4 diffuseTerm = nl * _Colour * _LightColor0;

                return diffuseTerm;
            }
            ENDCG
        }
    }
}
