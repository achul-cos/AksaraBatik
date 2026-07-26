Shader "Custom/2D/Sprite Hover Outline"
{
    Properties
    {
        [PerRendererData]
        _MainTex ("Sprite Texture", 2D) = "white" {}

        _Color ("Tint", Color) = (1,1,1,1)

        _OutlineColor ("Outline Color", Color) = (1,1,0,1)

        _OutlineWidth ("Outline Width (Pixels)", Range(0, 10)) = 1

        _OutlineEnabled ("Outline Enabled", Range(0, 1)) = 0

        _AlphaThreshold ("Alpha Threshold", Range(0, 1)) = 0.1
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off

        // Standard alpha blending untuk Sprite PNG biasa
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"


            // =========================================================
            // STRUCT
            // =========================================================

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };


            // =========================================================
            // TEXTURE
            // =========================================================

            TEXTURE2D(_MainTex);

            SAMPLER(sampler_MainTex);


            // =========================================================
            // MATERIAL PROPERTIES
            // =========================================================

            CBUFFER_START(UnityPerMaterial)

                float4 _Color;

                float4 _OutlineColor;

                float _OutlineWidth;

                float _OutlineEnabled;

                float _AlphaThreshold;

            CBUFFER_END


            // =========================================================
            // UNITY AUTO-GENERATED TEXTURE TEXEL SIZE
            //
            // _MainTex_TexelSize:
            //
            // x = 1 / texture width
            // y = 1 / texture height
            // z = texture width
            // w = texture height
            // =========================================================

            float4 _MainTex_TexelSize;


            // =========================================================
            // VERTEX
            // =========================================================

            Varyings vert(Attributes input)
            {
                Varyings output;

                output.positionHCS =
                    TransformObjectToHClip(
                        input.positionOS.xyz
                    );

                output.uv = input.uv;

                output.color =
                    input.color *
                    _Color;

                return output;
            }


            // =========================================================
            // SAMPLE ALPHA
            // =========================================================

            float SampleAlpha(float2 uv)
            {
                // Clamp UV agar tidak mengambil sample
                // dari luar texture.
                //
                // Ini penting untuk mencegah pixel putih
                // yang bocor dari edge texture.

                uv = clamp(
                    uv,
                    0.0,
                    1.0
                );

                float alpha =
                    SAMPLE_TEXTURE2D(
                        _MainTex,
                        sampler_MainTex,
                        uv
                    ).a;

                // Alpha kecil dianggap transparan penuh.
                //
                // Contoh:
                //
                // Alpha = 0.01
                // Alpha = 0.05
                //
                // Tidak akan dianggap sebagai bagian sprite
                // jika threshold = 0.1.

                return step(
                    _AlphaThreshold,
                    alpha
                );
            }


            // =========================================================
            // FRAGMENT
            // =========================================================

            half4 frag(Varyings input) : SV_Target
            {
                // =====================================================
                // MAIN SPRITE SAMPLE
                // =====================================================

                float4 mainColor =
                    SAMPLE_TEXTURE2D(
                        _MainTex,
                        sampler_MainTex,
                        input.uv
                    );


                // Terapkan SpriteRenderer Color

                mainColor *= input.color;


                // =====================================================
                // CURRENT PIXEL ALPHA
                // =====================================================

                float currentAlpha =
                    mainColor.a;


                // Tentukan apakah pixel saat ini
                // dianggap sebagai pixel solid sprite.

                float currentSolid =
                    step(
                        _AlphaThreshold,
                        currentAlpha
                    );


                // =====================================================
                // TEXEL SIZE
                // =====================================================

                // _MainTex_TexelSize.xy:
                //
                // x = 1 / texture width
                // y = 1 / texture height
                //
                // Jadi satu texel texture =

                float2 texelSize =
                    _MainTex_TexelSize.xy;


                // =====================================================
                // OUTLINE OFFSET
                // =====================================================

                float2 offsetX =
                    float2(
                        texelSize.x *
                        _OutlineWidth,

                        0.0
                    );


                float2 offsetY =
                    float2(
                        0.0,

                        texelSize.y *
                        _OutlineWidth
                    );


                // =====================================================
                // SAMPLE 8 DIRECTIONS
                // =====================================================

                float alphaUp =
                    SampleAlpha(
                        input.uv +
                        offsetY
                    );


                float alphaDown =
                    SampleAlpha(
                        input.uv -
                        offsetY
                    );


                float alphaLeft =
                    SampleAlpha(
                        input.uv -
                        offsetX
                    );


                float alphaRight =
                    SampleAlpha(
                        input.uv +
                        offsetX
                    );


                float alphaUpLeft =
                    SampleAlpha(
                        input.uv -
                        offsetX +
                        offsetY
                    );


                float alphaUpRight =
                    SampleAlpha(
                        input.uv +
                        offsetX +
                        offsetY
                    );


                float alphaDownLeft =
                    SampleAlpha(
                        input.uv -
                        offsetX -
                        offsetY
                    );


                float alphaDownRight =
                    SampleAlpha(
                        input.uv +
                        offsetX -
                        offsetY
                    );


                // =====================================================
                // FIND NEIGHBOR SOLID PIXEL
                // =====================================================

                float surroundingAlpha =
                    max(
                        max(
                            alphaUp,
                            alphaDown
                        ),

                        max(
                            alphaLeft,
                            alphaRight
                        )
                    );


                surroundingAlpha =
                    max(
                        surroundingAlpha,

                        max(
                            alphaUpLeft,
                            alphaUpRight
                        )
                    );


                surroundingAlpha =
                    max(
                        surroundingAlpha,

                        max(
                            alphaDownLeft,
                            alphaDownRight
                        )
                    );


                // =====================================================
                // CREATE OUTLINE
                // =====================================================

                // Outline hanya muncul jika:
                //
                // surroundingAlpha = 1
                // currentSolid     = 0
                //
                // Artinya:
                //
                // Pixel sekarang transparan
                // tetapi pixel tetangganya adalah sprite.

                float outline =
                    surroundingAlpha *
                    (1.0 - currentSolid);


                // Aktif/nonaktif outline

                outline *=
                    _OutlineEnabled;


                // =====================================================
                // OUTLINE COLOR
                // =====================================================

                float4 finalOutlineColor =
                    _OutlineColor;


                finalOutlineColor.a *=
                    outline;


                // =====================================================
                // FINAL COLOR
                // =====================================================

                float4 finalColor =
                    mainColor;


                // Tambahkan warna outline
                // hanya di area transparan.

                finalColor.rgb =
                    lerp(
                        finalColor.rgb,
                        finalOutlineColor.rgb,
                        outline
                    );


                // Gunakan alpha terbesar
                // antara sprite dan outline.

                finalColor.a =
                    max(
                        mainColor.a,
                        finalOutlineColor.a
                    );


                return finalColor;
            }

            ENDHLSL
        }
    }
}