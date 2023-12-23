Shader "VFX/Distort"
{
	Properties
	{
		_DistTex ("Distort Texture", 2D) = "bump" {}
		[Header(Cull and Offset)]
		//Кулинг
		[Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 2
		//Офсет камеры
		[KeywordEnum(Off, On)] _Use_Camera_Offset("Use Camera offset",Int) = 0
		_CamOffset("Camera offset", Range(-100,100)) = 0
		//Обрезка по Z
		[Header(Scroll and Distrot)]
		//Скролл текстуры
		[KeywordEnum(Off, On)] _Use_Scroll("Use Scroll",Int) = 0
		_ScrollX ("Scroll X", Float ) = 1
		_ScrollY ("Scroll Y", Float ) = 0
		//Искажение
		_Distortion ("Distortion", Float) = 0
		//Обрезка по Z
		[Header(World Clamp)]
		[KeywordEnum(Off, On)] _Use_Clamp("Use Clamp Z",Int) = 0
		_Grad ("Gradient", Range(0,100)) = 0
		//Прозрачность
		[HideInInspector]
		_Transparent ("Transparency Factor", Range(0,1)) = 1

	}
	SubShader{
		GrabPass {"_GrabTexture"}
		

		Pass {
			Tags {"IgnoreProjector"="True" "RenderType"="Transparent" "Queue"="Transparent"}
		ZWrite Off
		Cull[_Cull]

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#pragma shader_feature _USE_CAMERA_OFFSET_ON _USE_CAMERA_OFFSET_OFF
			#pragma shader_feature _USE_CLAMP_ON _USE_CLAMP_OFF
			#pragma shader_feature _USE_SCROLL_ON _USE_SCROLL_OFF

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex  : POSITION;
				float4 color   : COLOR;
				float4 tangent : TANGENT;
				float2 uv 	   : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color  : COLOR;
				float2 uv 	  : TEXCOORD0;
				float4 grab   : TEXCOORD1; 
				
				#ifdef _USE_CLAMP_ON
					float3 worldPos : TEXCOORD2;
				#endif
			};

			sampler2D _DistTex, _GrabTexture;
			float4 _DistTex_ST;
			half _CamOffset, _CustomPos, _Transparent;
			half _Distortion, _Grad,_ScrollX, _ScrollY;

			v2f vert (appdata v)
			{
				v2f o = (v2f)0;

				#ifdef _USE_CAMERA_OFFSET_ON
					float3 camVtx = UnityObjectToViewPos(v.vertex);
					camVtx.xyz -= normalize(camVtx.xyz) * _CamOffset;
					o.vertex = mul(UNITY_MATRIX_P, float4(camVtx, 1.0));
				#else
					o.vertex = UnityObjectToClipPos(v.vertex);
				#endif 

				o.uv = TRANSFORM_TEX (v.uv, _DistTex) - frac(float2 (_ScrollX, _ScrollY) * _Time.y);
				o.color = v.color * _Transparent * _Distortion * v.tangent;
				o.grab = ComputeGrabScreenPos(o.vertex);
				#ifdef _USE_CLAMP_ON
					o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				#endif

				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				#ifdef _USE_CLAMP_ON
					float comp = smoothstep(_CustomPos, _CustomPos + _Grad, i.worldPos.y);
					i.color.a *= comp;
				#endif

				half2 bump = UnpackNormal(tex2D(_DistTex, i.uv));
				i.grab.xy += bump * i.color.a;
				fixed4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.grab));
				return col;
			}
			ENDCG
		}
	}
}
