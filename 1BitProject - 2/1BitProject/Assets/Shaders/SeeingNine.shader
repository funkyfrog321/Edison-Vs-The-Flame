Shader "Unlit/NewUnlitShader"
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _bwBlend ("Black & White blend", Range (0, 1)) = 0
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform float _bwBlend;
uniform float _lumCutoff;
uniform float _adjustAmount;
uniform float2 _res;

float4 frag(v2f_img i) : COLOR
{
    //float4 c = tex2D(_MainTex, i.uv);
    
    
    //float2 u0v0 = float2(max(i.u - _halfPixel, 0.0), max(i.v - _halfPixel, 0.0));
    float adjust_x = _adjustAmount / _res.x;
    float adjust_y = _adjustAmount / _res.y;
    float u0 = max(i.uv.x - adjust_x, 0.0);
    float u2 = min(i.uv.x + adjust_x, 1.0);
    float v0 = max(i.uv.y - adjust_y, 0.0);
    float v2 = min(i.uv.y + adjust_y, 1.0);
    
    float4 c0 = tex2D(_MainTex, float2(u0, v0));
    float4 c1 = tex2D(_MainTex, float2(u0, i.uv.y));
    float4 c2 = tex2D(_MainTex, float2(u0, v2));
    float4 c3 = tex2D(_MainTex, float2(i.uv.x, v0));
    float4 c4 = tex2D(_MainTex, float2(i.uv.x, i.uv.y));
    float4 c5 = tex2D(_MainTex, float2(i.uv.x, v2));
    float4 c6 = tex2D(_MainTex, float2(u2, v0));
    float4 c7 = tex2D(_MainTex, float2(u2, i.uv.y));
    float4 c8 = tex2D(_MainTex, float2(u2, v2));
    
    float4 c = (c0 + c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8) / 9.0;
                
    float lum = c.r * .3 + c.g * .59 + c.b * .11;
    lum = lum < _lumCutoff ? 0.0 : 1.0;
    float3 bw = float3(lum, lum, lum);
                
    float4 result = c;
    result.rgb = lerp(c.rgb, bw, _bwBlend);
    return result;
}
ENDCG
        }
    }
}