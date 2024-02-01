Shader "Unlit/Flash"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Flash("Flash", COLOR) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Flash;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 flash = (0, 0, 0, 0);
                flash.rgb = _Flash.rgb * _Flash.a;
                fixed4 col = tex2D(_MainTex, i.uv) + flash;
                return col;
            }
            ENDCG
        }
    }
}
