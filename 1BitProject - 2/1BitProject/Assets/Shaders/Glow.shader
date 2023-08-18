Shader"Unlit/Glow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _intensity ("Light level", Float) = 1
        _pixel_size ("Size of pixels in effect", Integer) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _intensity;
            float _pixel_scale;
uniform float2 _res;
uniform uint _pixel_size;
uniform float4 _center;
uniform float4x4 _ordered_bayer;

float is_lit(int band_x, int band_y, float distance)
{
    int distance_marker = int(distance * 20);
    bool lit = (band_x % 2 == 0) && (band_y % 2 == 0) && distance_marker < 5;
    lit = lit | ((band_x + band_y) % 4 == 0) && distance_marker <= 7;
    lit = lit | ((band_x + band_y) % 8 == 0 && distance_marker > 7);
    //lit = lit & distance_marker < 5;
    return pow(lit, 2.2);

}

bool lit_crosshairs(int band_x, int band_y, int distance)
{
    bool light = 0.0;
    
    light = (band_x % 2 == 0) && (band_y % 2 == 0) && distance < 24;
    light = light | ((band_x + band_y) % 16 == 0) && distance <= 32;
    light = light | ((band_x % 64 == 0) | (band_y % 64 == 0)) && distance <= 64;
    
    return light;
}

bool lit_2(uint band_x, uint band_y, uint distance)
{
    bool light = 0.0;
    
    light = (band_x % 2 == 0) && (band_y % 2 == 0) && distance < 24;
    light = light | ((band_x + band_y) % 16 == 0) && distance <= 32;
    light = light | ((band_x * band_y + 2 * band_x + 2 * band_y + 4) % 3 == 0) && distance <= 64;
    
    return light;
}

float inverse_square(float distance)
{
    return 0.01 / pow(distance, 2.0);
}

float linear_light(float distance)
{
    return 0.00001*(-distance + 1);
}

float light_falloff(float distance)
{
    return linear_light(distance);
}

bool dither(uint band_x, uint band_y, float light_level)
{   
    return light_level > _ordered_bayer[band_x % 4][band_y % 4] - 0.5;
}

fixed4 frag(v2f_img i) : SV_TARGET
{
    float4 c;
    
    // Calculate the distance from the center of the glow
    // VS shortcut: shift+alt+'.' adds next occurrence to selection
    
    // Eventual goal: create a floor shader that incorporates all sources of light on the stage
    // Lights will combine for greater luminance
    float cxy = 0.5;
    float distance = sqrt(pow(i.uv.x - 0.5, 2.0) + pow(i.uv.y - 0.5, 2.0));
    float hmd = 0.5 - distance;
    
    float grid_size = float(1/_pixel_size);
    float new_u = i.uv.x - (i.uv.x % grid_size);
    float new_v = i.uv.y - (i.uv.y % grid_size);
    float bands = new_u % 0.1 <= 0.03;
    
    
    int band_x = i.pos.x - (i.pos.x % _pixel_size);
    int band_y = i.pos.y - (i.pos.y % _pixel_size);
    
    int2 norm_center = int2(_center.x - (_center.x % _pixel_size), _center.y - (_center.y % _pixel_size));
    
    // Get the distance in (pixel_size) pixels to the center of the light source
    int norm_distance_x = abs(band_x - (_center.x - (_center.x % _pixel_size)));
    int norm_distance_y = abs(band_y - (_center.y - (_center.y % _pixel_size)));
    int norm_distance_xy = sqrt(pow(norm_distance_x, 2.0) + pow(norm_distance_y, 2.0));
    //norm_distance_xy = min(norm_distance_x, norm_distance_y);
    
    float light_level = lit_2(band_x, band_y, norm_distance_xy);

    light_level = dither(band_x, band_y, light_falloff(distance));
    
    light_level = light_level * _intensity;
    
    c = float4(light_level, light_level, light_level, 1.0);
    
    return c;
    
    
    
    
    //// sample the texture
    //fixed4 col = tex2D(_MainTex, i.uv);
                
    //return col;
}
            ENDCG
        }
    }
}
