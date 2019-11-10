Shader "Rays Custom/ParallaxShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Offset ("Offset Range UV Space", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

			float _Offset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float3 viewDir = UNITY_MATRIX_IT_MV[2].xyz;

				float3 normal = normalize(i.worldNormal);
				
				//floor
				//float xDot = dot(viewDir.xy, normal.xy) * _Offset;
				//float yDot = dot(viewDir.xy,normal.yz) * _Offset;
				
				//wall
				float xDot = dot(viewDir.yz	, normal.xz) * _Offset;
				float yDot = dot(viewDir.yz	, normal.zx) * _Offset;

                fixed4 col = tex2D(_MainTex, float2(i.uv.x + yDot, i.uv.y + xDot));
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
