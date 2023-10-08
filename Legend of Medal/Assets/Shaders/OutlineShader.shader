Shader "Custom/Outline"
{
    Properties
    {
        _Color("Outline Color", Color) = (0,0,0,1)
        _Outline("Outline width", Range(0.002, 0.03)) = .005
        _MainTex("Base (RGB)", 2D) = "white" { }
    }

        CGINCLUDE
#include "UnityCG.cginc"

        float _Outline;
    float4 _OutlineColor;

    ENDCG

        Pass
    {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        struct v2f
        {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        sampler2D _MainTex;

        v2f vert(appdata v)
        {
            v2f o;
            o.uv = v.texcoord;
            o.vertex = UnityObjectToClipPos(v.vertex);
            return o;
        }

        half4 frag(v2f i) : SV_Target
        {
            // just make it black
            return half4(0,0,0,1);
        }
        ENDCG
    }
}
