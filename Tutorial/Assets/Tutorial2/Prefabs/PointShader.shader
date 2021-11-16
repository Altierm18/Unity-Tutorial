Shader "Custom/NewSurfaceShader"
{
    Properties {
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.5
    }

    SubShader {
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input{
            float3 worldPos;
        };

        float _Smoothness;
        float _Metallic;

        void surf (Input input, inout SurfaceOutputStandard output) {
            output.Albedo.rb = input.worldPos.xz * 0.5 + 0.5; //rgb goes from 0,1
            output.Smoothness = _Smoothness;
            output.Metallic = _Metallic;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
