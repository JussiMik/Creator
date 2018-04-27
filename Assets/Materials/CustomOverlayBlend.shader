// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/OverlayBlend"
{
	Properties{
		_MainTex("Texture", any) = "" {}
	_Color("Blend Color", Color) = (0.2, 0.3, 1 ,1)
	}

		SubShader{

		Tags{ "ForceSupported" = "True" "RenderType" = "Overlay" }

		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ZWrite Off
		Fog{ Mode Off }
		ZTest Always

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest

#include "UnityCG.cginc"

		struct appdata_t {
		float4 vertex : POSITION;
		fixed4 color : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f {
		float4 vertex : POSITION;
		fixed4 color : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	sampler2D _MainTex;

	uniform float4 _MainTex_ST;
	uniform float4 _Color;

	v2f vert(appdata_t v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.color = v.color;
		o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
		return o;
	}

	fixed4 frag(v2f i) : COLOR{
		// Get the raw texture value
		float4 texColor = tex2D(_MainTex, i.texcoord);

		fixed3 br = clamp(sign(texColor.rgb - fixed3(0.5,0.5,0.5)), fixed3(0.0,0.0,0.0), fixed3(1.0,1.0,1.0));
		fixed3 multiply = 2.0 * texColor.rgb *  _Color.rgb;
		fixed3 screen = fixed3(1.0,1.0,1.0) - 2.0 * (fixed3(1.0,1.0,1.0) - texColor.rgb)*(fixed3(1.0,1.0,1.0) - _Color.rgb);
		// If br is 0.0, overlay will be multiply (which translates to if (base < 0.5) { return multiply; }).
		// if bt is 1.0, overlay will be screen (which translates to if (base >= 0.5) { return screen; }).
		fixed4 output = fixed4(lerp(multiply, screen, br),1);
		output.a = texColor.a * _Color.a;
		return output;
	}

		ENDCG
	}
	}


		SubShader{

		Tags{ "ForceSupported" = "True" "RenderType" = "Overlay" }

		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ZWrite Off
		Fog{ Mode Off }
		ZTest Always

		BindChannels{
		Bind "vertex", vertex
		Bind "color", color
		Bind "TexCoord", texcoord
	}

		Pass{
		SetTexture[_MainTex]{
		combine primary * texture DOUBLE, primary * texture DOUBLE
	}
	}
	}

		Fallback off
}
