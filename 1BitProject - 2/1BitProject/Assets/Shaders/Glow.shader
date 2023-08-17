Shader"Unlit/Glow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _intensity ("Light level", Range(0,1)) = 0
        _pixel_scale ("Size of pixels in effect", float) = 1
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

float is_lit(int band_x, int band_y, float distance)
{
    int distance_marker = int(distance * 20);
    bool lit = (band_x % 2 == 0) && (band_y % 2 == 0) && distance_marker < 5;
    lit = lit | ((band_x + band_y) % 4 == 0) && distance_marker <= 7;
    lit = lit | ((band_x + band_y) % 8 == 0 && distance_marker > 7);
    //lit = lit & distance_marker < 5;
    return pow(lit, 2.2);

}

            float4 frag (v2f_img i) : COLOR
            {
    float4 c;
    
    // Calculate the distance from the center of the glow
    // VS shortcut: shift+alt+'.' adds next occurrence to selection
    
    // Eventual goal: create a floor shader that incorporates all sources of light on the stage
    // Lights will combine for greater luminance
    float cxy = 0.5;
    float distance = sqrt(pow(i.uv.x - 0.5, 2.0) + pow(i.uv.y - 0.5, 2.0));
    float hmd = 0.5 - distance;
    
    float grid_size = 0.01;
    float new_u = i.uv.x - (i.uv.x % grid_size);
    float new_v = i.uv.y - (i.uv.y % grid_size);
    float bands = new_u % 0.1 <= 0.03;
    
    
    int band_x = int(i.uv.x / grid_size);
    int band_y = int(i.uv.y / grid_size);
    
    float light_level = is_lit(band_x, band_y, distance);
    
    
    c = float4(light_level, light_level, light_level, light_level);
    
    return c;
    
    
    
    
                //// sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                
                //return col;
            }
            ENDCG
        }
    }
}
