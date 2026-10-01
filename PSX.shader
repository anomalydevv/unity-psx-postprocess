Shader "Hidden/PSX_PostProcess"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScreenResolution ("Resolution (X, Y)", Vector) = (320, 240, 0, 0)
        _ColorDepth ("Color Depth (Bits)", Float) = 16.0
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.15
    }
    SubShader
    {
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

            sampler2D _MainTex;
            float4 _ScreenResolution;
            float _ColorDepth;
            float _ScanlineIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 1. Düşük Çözünürlük (Pixelation)
                float2 uv = floor(i.uv * _ScreenResolution.xy) / _ScreenResolution.xy;

                // Ekrandan piksellenmiş rengi al
                fixed4 col = tex2D(_MainTex, uv);

                // 2. Renk Derinliği Kısıtlama (PS1 15-bit / 16-bit RGB Hissi)
                col.rgb = floor(col.rgb * _ColorDepth) / _ColorDepth;

                // 3. CRT Scanline (Tarama Çizgileri)
                float scanline = sin(i.uv.y * _ScreenResolution.y * 3.14159) * _ScanlineIntensity;
                col.rgb -= scanline;

                return col;
            }
            ENDCG
        }
    }
}