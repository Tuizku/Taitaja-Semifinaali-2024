// UI TEMPLATE: https://github.com/TwoTailsGames/Unity-Built-in-Shaders/blob/master/DefaultResourcesExtra/UI/UI-Default.shader
// BLUR BASED ON: https://www.shadertoy.com/view/Xltfzj

Shader "UI/Blurui"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _Dirs("Directions", Float) = 8
        _Quality("Quality", Float) = 4
        _Size("Size", Float) = 8

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Stencil
            {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }

            Cull Off
            Lighting Off
            ZWrite Off
            ZTest[unity_GUIZTestMode]
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask[_ColorMask]

            GrabPass
            {
                "_BackgroundTexture"
            }
            Pass
            {
                Name "Default"
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0

                #include "UnityCG.cginc"
                #include "UnityUI.cginc"

                #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
                #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
                    float4 worldPosition : TEXCOORD1;
                    float4 grabPassUV : TEXCOORD2;
                    UNITY_VERTEX_OUTPUT_STEREO
                };

                sampler2D _MainTex;
                sampler2D _BackgroundTexture;
                fixed4 _Color;
                fixed4 _TextureSampleAdd;
                float4 _ClipRect;
                float4 _MainTex_ST;

                float _Dirs;
                float _Quality;
                float _Size;

                v2f vert(appdata_t v)
                {
                    v2f OUT;
                    UNITY_SETUP_INSTANCE_ID(v);
                    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                    OUT.worldPosition = v.vertex;
                    OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                    OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                    OUT.color = v.color * _Color;
                    OUT.grabPassUV = ComputeGrabScreenPos(OUT.vertex);
                    return OUT;
                }

                fixed4 frag(v2f IN) : SV_Target
                {
                    const float pi2 = 6.28318530718;

                    float2 radius = _Size / _ScreenParams.zw / 1080;
                    half4 color = tex2Dproj(_BackgroundTexture, IN.grabPassUV);

                    int sum;
                    for (float d = 0.0; d < pi2; d += pi2 / _Dirs)
                    {
                        for (float i = 1.0 / _Quality; i <= 1.0; i += 1.0 / _Quality) {

                            float2 offsetPos = float2(cos(d), sin(d)) * radius * i;
                            IN.grabPassUV.xy += offsetPos;
                            color += tex2Dproj(_BackgroundTexture, IN.grabPassUV);
                            IN.grabPassUV.xy -= offsetPos;
                            sum += 1;
                        }
                    }
                    color /= sum;
                    half4 base = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
                    color *= base;

                    #ifdef UNITY_UI_CLIP_RECT
                    color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                    #endif

                    #ifdef UNITY_UI_ALPHACLIP
                    clip(color.a - 0.001);
                    #endif

                    return color;
                }
            ENDCG
            }
        }
}