Shader "Unlit/GLlineZOn"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader{
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Cull Off
			BindChannels {
				Bind "vertex", vertex
				Bind "color", color
			}
		}
	}
}
