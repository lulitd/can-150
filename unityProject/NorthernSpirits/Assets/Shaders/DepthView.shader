// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DepthGrayscale" {
	SubShader{
		Tags{ "RenderType" = "Opaque" }

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		sampler2D _CameraDepthTexture;

	struct v2f {
		float4 pos : SV_POSITION;
		float4 scrPos:TEXCOORD1;
	};

	//Vertex Shader
	v2f vert(appdata_base v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.scrPos = ComputeScreenPos(o.pos);

		//#if defined(UNITY_UV_STARTS_AT_TOP)
		//o.scrPos.y = 1 - o.scrPos.y;
		//#endif

		return o;
	}

	//Fragment Shader
	half4 frag(v2f i) : COLOR{
		float depthValue = LinearEyeDepth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrPos)).r);
	half4 depth;

	depth.rgb = 1-depthValue;
	depth.a = 1-depthValue; 

	return depth;
	}
		ENDCG
	}
	}
		FallBack "Diffuse"
}