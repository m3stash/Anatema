Shader "Unlit/GLlineZOff"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader{
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			ZTest Always
			Cull Off
			BindChannels {
				Bind "vertex", vertex
				Bind "color", color
			}
		}
	}
}
