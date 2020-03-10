Shader "TDC/Jelly"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Offset ("Offset", Float) = 1
		_Pow ("Pow", Float) = 1

		_ShowElasticity ("ShowElasticity", Range(0, 1)) = 0
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 elasticity : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4x4 _InsideMassMatrix;
			float _Offset;
			float _Pow;
			float _ShowElasticity;

            v2f vert (appdata v)
            {
                v2f o;

                float4 worldVertex = mul(UNITY_MATRIX_M, v.vertex);
				float4 worldInsideMassVertex = mul(_InsideMassMatrix, v.vertex);

				float distFactor = length(v.vertex) / _Offset;
				distFactor = pow(distFactor, _Pow);

				o.elasticity = float4(distFactor, distFactor, distFactor, 1);

				float4 worldResultVertex = lerp(worldVertex, worldInsideMassVertex, distFactor);

				o.vertex = mul(UNITY_MATRIX_VP, worldResultVertex);


                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 tex = tex2D(_MainTex, i.uv);

				float4 col = lerp(tex, i.elasticity, _ShowElasticity);

                return col;
				//return i.elasticity;
            }
            ENDCG
        }
    }
}
