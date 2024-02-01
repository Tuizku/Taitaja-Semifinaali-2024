Shader "Hidden/Play"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Prog ("Transition Progress", Float) = 0.5
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Prog;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = lerp(
                    tex2D(_MainTex, i.uv),
                    float4(0, 0, 0, 1),
                    floor(min(distance(i.uv, float2(0.5, 0.5)) / (1.0f - _Prog), 1))
                );
                return col;
            }
            ENDCG
        }
    }
}
