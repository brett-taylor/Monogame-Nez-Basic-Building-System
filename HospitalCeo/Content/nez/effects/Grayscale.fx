sampler s0;


float4 PixelShaderFunction( float2 texCoord:TEXCOORD0 ) : COLOR0
{
    float4 tex = tex2D( s0, texCoord );

    // convert it to greyscale. The constants 0.3, 0.59, and 0.11 are because the human eye is more sensitive to green light, and less to blue.
    float grayScale = dot( tex.rgb, float3( 0.3, 0.59, 0.11 ) );

    tex.rgb = grayScale;

    return tex;
}


technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}