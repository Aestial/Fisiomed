// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/Dissolve"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
 
        _SliceGuide ("Slice Guide (RGB)", 2D) = "white" {}
          _SliceAmount ("Slice Amount", Range(0.0, 1.0)) = 0.5
        _BurnSize ("Burn Size", Range(0.0, 1.0)) = 0.15
        _BurnRamp ("Burn Ramp (RGB)", 2D) = "white" {}
   
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
 
        _ColorMask ("Color Mask", Float) = 15
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
   
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
 
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]
 
        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
       
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float4 texcoord : TEXCOORD0;
            };
 
            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float4 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
            };
       
            fixed4 _Color;
            fixed4 _TextureSampleAdd;
 
            bool _UseClipRect;
            float4 _ClipRect;
 
            bool _UseAlphaClip;
 
            sampler2D _SliceGuide;
           float _SliceAmount;
            sampler2D _BurnRamp;
            float _BurnSize;
            float4 uv_SliceGuide;
            float4 uv_MainTex;
 
            sampler2D _MainTex;
 
            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.worldPosition = IN.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
 
                OUT.texcoord = IN.texcoord;
           
                #ifdef UNITY_HALF_TEXEL_OFFSET
                OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
                #endif
 
                if(any(tex2Dlod (_SliceGuide, uv_SliceGuide).rgb - _SliceAmount) < 0){
                    OUT.color.a = 0;
                }
               OUT.color = tex2Dlod (_MainTex, IN.texcoord);
       
                half test = tex2Dlod (_SliceGuide, IN.texcoord).rgb - _SliceAmount;
                if(test < _BurnSize && _SliceAmount > 0 && _SliceAmount < 1){
                    OUT.color *= tex2Dlod(_BurnRamp, float4(test *(1/_BurnSize), 0,0,0));
                }
           
                //OUT.color = IN.color * _Color;
                return OUT;
            }
 
            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
 
                if (_UseClipRect)
                    color *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
           
                if (_UseAlphaClip)
                    clip (color.a - 0.001);
 
                return color;
            }
        ENDCG
        }
    }
}
