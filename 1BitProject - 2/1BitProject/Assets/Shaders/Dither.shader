Shader"Unlit/NewUnlitShader"
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _bwBlend ("Black & White blend", Range (0, 1)) = 0
        _lumCutoff ("Black-White lum threshold", Range(0, 1)) = 0
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
uniform float4x4 _bayer;
uniform int _pixel_size = 1;
uniform float4 _color1;
uniform float4 _color2;

bool dither(uint band_x, uint band_y, float color)
{
    return color > _bayer[band_x % 4][band_y % 4] + 0.1;// - 0.5;
}

float4 frag(v2f_img i) : COLOR
{
    //float4 c = tex2D(_MainTex, i.uv);    
    //float2 u0v0 = float2(max(i.u - _halfPixel, 0.0), max(i.v - _halfPixel, 0.0));
    
    // heres what i want to do:
    // every pixel calculates its closest integer multiple of pixelate / res.x,y
    // what happens if i take uv.x % (pixelate / res.x)
    // that didn't work, but i found an equation that did: uv.x - (uv.x % (pixelate / res.x))
    
    float nearest_pixel_u = (i.uv.x % (_adjustAmount / _res.x));
    float pixelate_u = i.uv.x - nearest_pixel_u;
    pixelate_u = max(min(pixelate_u, i.uv.x), 0.0);
    float nearest_pixel_v = (i.uv.y % (_adjustAmount / _res.y));
    float pixelate_v = i.uv.y - nearest_pixel_v;
    pixelate_v = max(min(pixelate_v, i.uv.y), 0.0);
    
    //float nearest_pixel_u = (i.uv.x % (_adjustAmount / _res.x));
    //float pixelate_u = i.uv.x - nearest_pixel_u + (_adjustAmount / _res.x);
    //pixelate_u = min(max(pixelate_u, i.uv.x), 1.0);
    //float nearest_pixel_v = (i.uv.y % (_adjustAmount / _res.y));
    //float pixelate_v = i.uv.y - nearest_pixel_v + (_adjustAmount / _res.y);
    //pixelate_v = min(max(pixelate_v, i.uv.y), 1.0);
    _pixel_size = int(_adjustAmount);
    int band_x = i.pos.x - (i.pos.x % _pixel_size);
    int band_y = i.pos.y - (i.pos.y % _pixel_size);
    
    //float4 c = tex2D(_MainTex, float2(pixelate_u, pixelate_v));
    float4 c = tex2D(_MainTex, i.uv);
    
    float lum = c.r * .3 + c.g * .59 + c.b * .11;
    //lum = dither(pixelate_u, pixelate_v, lum);
    lum = dither(band_x, band_y, lum);
    
    float3 bw = float3(lum, lum, lum);
                
    float4 result = c;
    result.rgb = lerp(c.rgb, bw, _bwBlend);
    result.rgb = lerp(_color2.rgb, _color1.rgb, result.r);
    
    return result;
}
ENDCG
        }
    }
}