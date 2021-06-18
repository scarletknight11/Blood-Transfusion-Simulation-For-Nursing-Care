/*
    ░█████╗░██╗░░░██╗████████╗██╗░░░░░██╗███╗░░██╗███████╗  ░██████╗██╗░░██╗░█████╗░██████╗░███████╗██████╗░
    ██╔══██╗██║░░░██║╚══██╔══╝██║░░░░░██║████╗░██║██╔════╝  ██╔════╝██║░░██║██╔══██╗██╔══██╗██╔════╝██╔══██╗
    ██║░░██║██║░░░██║░░░██║░░░██║░░░░░██║██╔██╗██║█████╗░░  ╚█████╗░███████║███████║██║░░██║█████╗░░██████╔╝
    ██║░░██║██║░░░██║░░░██║░░░██║░░░░░██║██║╚████║██╔══╝░░  ░╚═══██╗██╔══██║██╔══██║██║░░██║██╔══╝░░██╔══██╗
    ╚█████╔╝╚██████╔╝░░░██║░░░███████╗██║██║░╚███║███████╗  ██████╔╝██║░░██║██║░░██║██████╔╝███████╗██║░░██║
    ░╚════╝░░╚═════╝░░░░╚═╝░░░╚══════╝╚═╝╚═╝░░╚══╝╚══════╝  ╚═════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═════╝░╚══════╝╚═╝░░╚═╝

                █▀▀▄ █──█ 　 ▀▀█▀▀ █──█ █▀▀ 　 ░█▀▀▄ █▀▀ ▀█─█▀ █▀▀ █── █▀▀█ █▀▀█ █▀▀ █▀▀█ 
                █▀▀▄ █▄▄█ 　 ─░█── █▀▀█ █▀▀ 　 ░█─░█ █▀▀ ─█▄█─ █▀▀ █── █──█ █──█ █▀▀ █▄▄▀ 
                ▀▀▀─ ▄▄▄█ 　 ─░█── ▀──▀ ▀▀▀ 　 ░█▄▄▀ ▀▀▀ ──▀── ▀▀▀ ▀▀▀ ▀▀▀▀ █▀▀▀ ▀▀▀ ▀─▀▀
____________________________________________________________________________________________________________________________________________

        ▄▀█ █▀ █▀ █▀▀ ▀█▀ ▀   █░█ █░░ ▀█▀ █ █▀▄▀█ ▄▀█ ▀█▀ █▀▀   ▄█ █▀█ ▄█▄   █▀ █░█ ▄▀█ █▀▄ █▀▀ █▀█ █▀
        █▀█ ▄█ ▄█ ██▄ ░█░ ▄   █▄█ █▄▄ ░█░ █ █░▀░█ █▀█ ░█░ ██▄   ░█ █▄█ ░▀░   ▄█ █▀█ █▀█ █▄▀ ██▄ █▀▄ ▄█
____________________________________________________________________________________________________________________________________________
License:
    The license is ATTRIBUTION 3.0

    More license info here:
        https://creativecommons.org/licenses/by/3.0/
____________________________________________________________________________________________________________________________________________
This shader has NOT been tested on any other PC configuration except the following:
    CPU: Intel Core i5-6400
    GPU: NVidia GTX 750Ti
    RAM: 16GB
    Windows: 10 x64
    DirectX: 11
____________________________________________________________________________________________________________________________________________
*/

Shader "Ultimate 10+ Shaders/JustOutline"
{
    Properties
    {
//        _Color ("Color", Color) = (1,1,1,1)
//        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth ("Outline Width", Range(0, 4)) = 0.25
        
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Geometry" "Queue"="Transparent" }
        LOD 200
        Cull [_Cull]

        Pass{
            ZWrite Off
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f{
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
            };

            fixed4 _OutlineColor;
            half _OutlineWidth;

            v2f vert(appdata input){
                input.vertex += float4(input.normal * _OutlineWidth, 1);

                v2f output;

                output.pos = UnityObjectToClipPos(input.vertex);
                output.normal = mul(unity_ObjectToWorld, input.normal);

                return output;
            }

            fixed4 frag(v2f input) : SV_Target
            {
                return _OutlineColor;
            }

            ENDCG
        }

//        ZWrite On
        
    }
    FallBack "Diffuse"
}
