// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/Sprites/ClampDiffuse"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
        _DisableRange ("Disable Range", Vector) = (0,24,0,16)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting On
        ZWrite Off
        Blend One OneMinusSrcAlpha

        CGPROGRAM
        #pragma target 3.0
        #pragma surface surf Lambert finalcolor:unlit_test vertex:vert nofog nolightmap nodynlightmap keepalpha noinstancing noforwardadd 
        #pragma multi_compile_local _ PIXELSNAP_ON
        #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
        #include "UnitySprites.cginc"

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 normal;
            fixed4 color;
        };

        float4 _DisableRange;

        void vert (inout appdata_full v, out Input o)
        {
            v.vertex = UnityFlipSprite(v.vertex, _Flip);

            #if defined(PIXELSNAP_ON)
            v.vertex = UnityPixelSnap (v.vertex);
            #endif

            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.color = v.color * _Color * _RendererColor;
            o.normal = v.normal;
        }

        void unlit_test(Input IN, SurfaceOutput o, inout fixed4 color)
        {
            const bool unlit = IN.worldPos.x >= _DisableRange.x && IN.worldPos.x <= _DisableRange.y
                            && IN.worldPos.y >= _DisableRange.z && IN.worldPos.y <= _DisableRange.w;
            if(unlit){
                color.rgb = o.Albedo;
            }
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = SampleSpriteTexture (IN.uv_MainTex) * IN.color;
            o.Albedo = c.rgb * c.a;
            o.Alpha = c.a;
            o.Normal = IN.normal;
        }
        ENDCG
    }

Fallback "Transparent/VertexLit"
}