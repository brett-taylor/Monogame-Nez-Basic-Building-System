sampler2D Texture : register(s0);
bool ShouldRedSwap = false;
bool ShouldGreenSwap = false;
bool ShouldBlueSwap = false;
float4 RedChannelSwap = float4(0, 255, 0, 1);
float4 GreenChannelSwap = float4(0, 255, 0, 1);
float4 BlueChannelSwap = float4(0, 255, 0, 1);
float Allowance;

float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4 Color;
	Color = tex2D(Texture, uv.xy);

	// Red Channel Swap
	if (ShouldRedSwap && Color.r >= Allowance && Color.g == 0 && Color.b == 0)
		return RedChannelSwap;

	// Green Channel Swap
	if (ShouldGreenSwap && Color.r == 0 && Color.g >= Allowance && Color.b == 0)
		return GreenChannelSwap;

	// Blue Channel Swap
	if (ShouldBlueSwap && Color.r == 0 && Color.g == 0 && Color.b >= Allowance)
		return BlueChannelSwap;

	return Color;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 main();
	}
}