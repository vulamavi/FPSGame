#ifndef PROJECTION_CORRECTION_INCLUDED
#define PROJECTION__CORRECTION_INCLUDED
	
struct v2f
{
	float4 pos : POSITION;
	float2 uv : TEXCOORD0;
};
	
float _Depth;
float _Width;
float _Intensity;
		
v2f vert(appdata_img v)
{
	v2f o;
	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	o.uv = v.texcoord.xy;
	return o;
}
	
inline float2 SimpleCorrection(float x) 
{
	float angle = atan(_Width * abs(x-0.5) / _Depth);
	float x1 = x;
	float x2 = angle * _Depth * sign(x-0.5) / _Width + 0.5;
	x = x1 + _Intensity*(x1 - x2);

	return x;
}

inline float2 FullCorrection(float2 uv) 
{
	float angle = abs(uv.x-0.5)*_Width / _Depth;
	float x2 = tan(angle)*_Depth * sign(uv.x-0.5)/_Width + 0.5;
	x2 = lerp(uv.x, x2, _Intensity);

	float y2 = uv.y - 0.5;
	y2 *= sqrt((x2-0.5)*(x2-0.5)*_Width*_Width+_Depth*_Depth)/_Depth;
	y2 += 0.5;
	y2 = lerp(uv.y, y2, _Intensity);

	return float2(x2,y2);
}

#endif