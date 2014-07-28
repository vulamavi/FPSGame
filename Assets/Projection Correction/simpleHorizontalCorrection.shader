Shader "Custom/lensCorrection/HorizontalSimple" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	
	CGINCLUDE
	
	#include "UnityCG.cginc"
	#include "ProjectionCorrection.cginc"
	
	sampler2D _MainTex;
	
	half4 frag(v2f i) : COLOR
	{
		i.uv.x = SimpleCorrection(i.uv.x) ;
		
		//if (i.uv.x>1 || i.uv.x<0) return half4(0,0,0,0);
		//else return tex2D(_MainTex, i.uv);

		float check = saturate(sign(i.uv.x)); //i.uv.x<0
		check *= saturate(-sign(i.uv.x-1)); //i.uv.x>1
		return tex2D(_MainTex, i.uv) * check;
	}

	ENDCG 
	
	Subshader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog
			{
				Mode off
			}

			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest 
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}

	Fallback off
}
