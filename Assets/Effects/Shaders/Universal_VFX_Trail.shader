Shader "VFX/Trail"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
		_MainTex2("Texture 2", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (1,1,1,1)
		
		//Блендинг текстур, умножение или брать минимальное значение
		[Toggle(_MIN_BLENDING)] _Min_Blending("Texture Blending Min",Int) = 0

		//Режимы смешивания
		[Header(Blend and Offset)]
		[Enum(One,1,SrcAlpha,5)] _Blend1("Blend1 mode subset", Float) = 1
		[Enum(One,1,OneMinusSrcAlpha,10)] _Blend2("Blend2 mode subset", Float) = 1
		[Toggle(_USE_BLENDADD)] _Use_BlendAdd("Use Blend Add",Int) = 0
		_Blend ("Add/Blend", Range(0,1)) = 1
		//Кулинг
		[Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 2
		//Офсет камеры
		[KeywordEnum(Off, On)] _Use_Camera_Offset("Use Camera offset",Int) = 0
		_CamOffset("Camera offset", Range(-100,100)) = 0

		//Постоянная скорость
		[Header(Add Constant Speed)]
		_USpeed1("U Speed Map 1", Range(-5,5)) = 0
		_VSpeed1("V Speed Map 1", Range(-5,5)) = 0
		_USpeed2("U Speed Map 2", Range(-5,5)) = 0
		_VSpeed2("V Speed Map 2", Range(-5,5)) = 0

		//Используем смутстеп
		[Header(ColorCorrect)]
		[KeywordEnum(Off, On)] _Use_Smoothstep("Use Smoothstep",Int) = 0 
		_Min("Min", Range(0,1)) = 0
		_Max("Max", Range(0,1)) = 1
		//Используем Лерп
		[KeywordEnum(Off, Lerp1, Lerp2)] _Use("Use Lerp/Lerp2",Int) = 0
		_Lerp("Lerp", Range(0,2)) = 1
		//Простой множитель
		_Mult("Multiply", Range(0,10)) = 1

		//Обрезка по Y
		[Header(World Clamp)]
		[KeywordEnum(Off, On)] _Use_Clamp("Use Clamp Y",Int) = 0
		_Grad("Gradient", Range(0,100)) = 0
		[Toggle(_USE_COLOR)] _Use_Color("Color Multiply",Int) = 0

		//Прозрачность
		[HideInInspector]
		_Transparent("Transparency Factor", Range(0,1)) = 1
    }
    SubShader
    {
		Tags{ "IgnoreProjector" = "True" "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend [_Blend1][_Blend2]
		ZWrite Off
		Cull[_Cull]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

			#pragma shader_feature _USE_CAMERA_OFFSET_ON _USE_CAMERA_OFFSET_OFF
			#pragma shader_feature _USE_SMOOTHSTEP_ON _USE_SMOOTHSTEP_OFF
			#pragma shader_feature _USE_OFF _USE_LERP1 _USE_LERP2
			#pragma shader_feature _USE_CLAMP_ON _USE_CLAMP_OFF
			#pragma shader_feature _ _USE_COLOR
			#pragma shader_feature _ _MIN_BLENDING
			#pragma shader_feature _ _USE_BLENDADD

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
				float2 uv     : TEXCOORD0;
				float4 color  : COLOR;
            };

            struct v2f
            {
                float2 uv	  : TEXCOORD0;
				float4 color2 : TEXCOORD1;
				float2 uv2    : TEXCOORD2;
				float2 uvMask : TEXCOORD3;
                float4 vertex : SV_POSITION;
				float4 color  : COLOR;
				//Если используем отсечку по Y
				#ifdef _USE_CLAMP_ON
				float3 worldPos : TEXCOORD4;
				#endif
            };

            sampler2D _MainTex, _MainTex2, _Mask;
			float4 _MainTex_ST, _MainTex2_ST, _Mask_ST, _Color, _Color2;
			half _CamOffset, _Min, _Max, _Lerp, _Mult, _Grad, _CustomPos, _Transparent, _USpeed1, _VSpeed1, _USpeed2, _VSpeed2, _Blend;

            v2f vert (appdata v)
            {
                v2f o;
				//Сдвиг относительно камеры
				#ifdef _USE_CAMERA_OFFSET_ON
					float3 camVtx = UnityObjectToViewPos(v.vertex);
					camVtx.xyz -= normalize(camVtx.xyz) * _CamOffset;
					o.vertex = mul(UNITY_MATRIX_P, float4(camVtx, 1.0));
				#else
					o.vertex = UnityObjectToClipPos(v.vertex);
				#endif 

				float2 timeShift1 = float2(frac(_Time.y * _USpeed1), frac(_Time.y * _VSpeed1));
				float2 timeShift2 = float2(frac(_Time.y * _USpeed2), frac(_Time.y * _VSpeed2));

                o.uv = TRANSFORM_TEX(v.uv, _MainTex) + timeShift1;
				o.uv2 = TRANSFORM_TEX(v.uv, _MainTex2) + timeShift2;
				o.uvMask = TRANSFORM_TEX(v.uv, _Mask);

				o.color = v.color * _Color * _Transparent;
				o.color2 = _Color2;

				//расчет мировой позиции, для отсечки по Y
				#ifdef _USE_CLAMP_ON
					o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				#endif

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col2 = tex2D(_MainTex2, i.uv2);
				fixed mask = tex2D(_Mask, i.uvMask);

				#ifdef _MIN_BLENDING
					col = min(col, col2) * mask;
				#else
					col *= col2 * mask;
				#endif

				//кастомный смутстеп
				#ifdef _USE_SMOOTHSTEP_ON
					col = clamp((col - _Min) / (_Max - _Min), 0.0, 1.0);
				#endif

				col *= _Mult;
				col.a = col.r;

				//глов
				#ifdef _USE_LERP1
					col = lerp(i.color, i.color2, col.r * _Lerp) * col.r * i.color.a;
				#endif
				
				//лерп2
				#ifdef _USE_LERP2
					fixed lum = Luminance(col.rgb);
					col = lerp(i.color, i.color2, saturate(lum * _Lerp)) * lum * _Mult;
					col *= i.color;
				#endif

				//если глов и лерп не используется
				#ifdef _USE_OFF
					col *= i.color * i.color.a;
				#endif

				//отсечка по Y
				#ifdef _USE_CLAMP_ON
					float comp = smoothstep(_CustomPos, _CustomPos + _Grad, i.worldPos.y);
					#ifdef _USE_COLOR
						col *= comp;
					#else
						col.a *= comp;
					#endif
				#endif

				//домножение цвета на альфу при режиме One OneMinusSrcAlpha
				//и режиме One One
				#ifdef _USE_BLENDADD
					col.a *= _Blend;
					col.rgb *= i.color.a;
				#endif

                return saturate(col);
            }
            ENDCG
        }
    }
}
