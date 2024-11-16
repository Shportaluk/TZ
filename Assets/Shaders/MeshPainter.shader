Shader "Custom/MeshPainter"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PaintPosition("PaintPosition", VECTOR) = (0,0,0,0)
		_PaintBrushSize("PaintBrushSize", Range(0,0.1) ) = 0.02
		_PaintBrushColour("PaintBrushColor", Color ) = (0,0,0,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
		
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _PaintBrushSize;
			float2 _PaintPosition;
			float4 _PaintBrushColour;
	
			

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			

			fixed4 frag (v2f i) : SV_Target
			{
				if ( distance( i.uv, _PaintPosition ) <= _PaintBrushSize )
					return float4( _PaintBrushColour );
				return tex2D( _MainTex, i.uv );
			}
			ENDCG
		}
	}
}
