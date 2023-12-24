Shader "VFX/VFX_DistortUV"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _MainTex2("Texture 2", 2D) = "white" {}
        _Mask("Mask", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        
        //Режимы смешивания
        [Header(Blend and Offset)]
        [Enum(One,1,SrcAlpha,5)] _Blend1("Blend1 mode subset", Float) = 1
        [Enum(One,1,OneMinusSrcAlpha,10)] _Blend2("Blend2 mode subset", Float) = 1
        [Toggle(_USE_BLENDADD)] _Use_BlendAdd("Use Blend Add",Int) = 0
        _Blend ("Add/Blend", Range(0,1)) = 1

        [Space(15)]
        [Header(Distortion settings)]
        [Space(5)]
        [KeywordEnum(Scroll, FlowMap)] _Distortion_Variant("Distortion variant",Int) = 0
        _DistortPower ("Distort Power", Float ) = 0
        _Speed("Speed", Float) = 1
        
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

    }

    SubShader
    {
        Tags {"IgnoreProjector"="True" "RenderType"="Transparent" "Queue"="Transparent" "PreviewType" = "Plane"} 

        Blend [_Blend1] [_Blend2]
        ZWrite Off 
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            #pragma shader_feature _USE_SMOOTHSTEP_ON _USE_SMOOTHSTEP_OFF
            #pragma shader_feature _USE_OFF _USE_LERP1 _USE_LERP2
            #pragma shader_feature _ _USE_BLENDADD
            #pragma shader_feature _DISTORTION_VARIANT_SCROLL _DISTORTION_VARIANT_FLOWMAP


            struct appdata
            {
                float4 vertex       : POSITION;
                fixed4 color        : COLOR;
                float2 uv           : TEXCOORD0; 
                fixed4 color2       : TEXCOORD1;
                float4 customScroll : TEXCOORD2;
                float4 random       : TEXCOORD3;
                float4 tangent      : TANGENT;
            };

            struct v2f
            {
                float4 vertex       : SV_POSITION;
                float4 color        : COLOR;
                float4 uv           : TEXCOORD0;
                float2 uvMask       : TEXCOORD1;
                fixed4 color2       : TEXCOORD2;
                float4 preCalc      : TEXCOORD3;

            };

            sampler2D _MainTex, _MainTex2, _Mask;; 
            float4 _MainTex_ST, _MainTex2_ST, _Mask_ST;
            float _CustomPos;
            fixed4 _Color;
            half _Mult, _Lerp, _Blend, _Min, _Max, _Grad;
            half _USpeed1, _VSpeed1, _USpeed2, _VSpeed2, _DistortPower, _Speed;

            v2f vert (appdata v)
            {
                v2f o = (v2f)0;

                 o.vertex = UnityObjectToClipPos(v.vertex);


                float2 timeShift1 = float2(frac(_Time.y * _USpeed1), frac(_Time.y * _VSpeed1));
                float2 timeShift2 = float2(frac(_Time.y * _USpeed2), frac(_Time.y * _VSpeed2));

                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex) + (v.customScroll.xy + v.random.xy) * _MainTex_ST.xy + timeShift1;
                o.uv.zw = TRANSFORM_TEX(v.uv, _MainTex2) + (v.customScroll.zw + v.random.zw) * _MainTex2_ST.xy + timeShift2;
                o.uvMask = TRANSFORM_TEX(v.uv, _Mask);
                o.color = v.color * v.tangent.w * _Color;
                o.color2 = v.color2;

                #ifdef _DISTORTION_VARIANT_FLOWMAP
                    float timeStep = _Time.x * _Speed;
                    o.preCalc.xy = float2(frac(timeStep * 0.5 + 0.5), frac(timeStep * 0.5 + 1));
                    o.preCalc.z = abs((0.5 - o.preCalc.x) / 0.5);
                #endif 

                return o;
            }


            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = 1;

                #ifdef _DISTORTION_VARIANT_SCROLL
                    fixed noise = tex2D(_MainTex, i.uv.xy).r;
                    noise *= tex2D(_MainTex2, i.uv.zw).r;
                    col = tex2D(_MainTex, i.uv.xy  + (noise * _DistortPower));
                    col *= tex2D(_Mask, i.uvMask).r;
                #endif 

                #ifdef _DISTORTION_VARIANT_FLOWMAP
                    float2 flowDir = (tex2D(_MainTex2, i.uv.xy ).rg * 2 - 1) * _DistortPower;
                    fixed noise1 = tex2D(_MainTex, i.uv.zw + flowDir * i.preCalc.x).r;
                    fixed noise2 = tex2D(_MainTex, i.uv.zw + flowDir * i.preCalc.y).r;
                    fixed flowResult = lerp(noise1, noise2, i.preCalc.z);
                    col = flowResult;
                    col *= tex2D(_Mask, i.uvMask).r;
                #endif 

                //кастомный смутстеп
                #ifdef _USE_SMOOTHSTEP_ON
                    col = clamp((col - _Min) / (_Max - _Min), 0.0, 1.0);
                #endif

                //лерп1
                #ifdef _USE_LERP1
                    col = lerp(i.color, i.color2, col.r * _Lerp) * col.r * _Mult * i.color.a;
                #endif
                
                //лерп2
                #ifdef _USE_LERP2
                    fixed lum = Luminance(col.rgb);
                    col = lerp(i.color, i.color2, saturate(lum * _Lerp)) * lum * _Mult;
                    col *= i.color;
                #endif

                //если лерп не используется
                #ifdef _USE_OFF
                    col *= i.color * _Mult;
                #endif
                

                //домножение цвета на альфу при режиме One OneMinusSrcAlpha
                //и режиме One One
                #ifdef _USE_BLENDADD
                    col.a *= _Blend;
                    col.rgb *= i.color.a;
                #endif

                return col;
            }
            ENDCG
        }
    }
}