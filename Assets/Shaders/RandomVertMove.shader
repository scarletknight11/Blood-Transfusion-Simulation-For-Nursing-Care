  Shader "Example/RandomVertExtrude" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _Amount ("Extrusion Amount", Range(-1,1)) = 0.5
      _Noise ("Noise Amount", Range(0,1)) = 0.0
      		_Scale ("Noise scale", Vector) = (1.0, 1.0, 1.0)
		_Offset ("Noise offset", Vector) = (1.0, 1.0, 1.0)
		_VertexScale ("Vertex Scale", Range(0,100)) = 1.0

    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert vertex:vert
      struct Input {
          float2 uv_MainTex;
      };
      float _Amount;
      float _Noise;
      float3 _Scale;
	  float3 _Offset;
      float _VertexScale;


			float hash( float n )
			{
			    return frac(sin(n)*43758.5453);
			}

			float noise( float3 x )
			{
			    // The noise function returns a value in the range -1.0f -> 1.0f

			    float3 p = floor(x);
			    float3 f = frac(x);

			    f       = f*f*(3.0-2.0*f);
			    float n = p.x + p.y*57.0 + 113.0*p.z;

			    return lerp(lerp(lerp( hash(n+0.0), hash(n+1.0),f.x),
			                   lerp( hash(n+57.0), hash(n+58.0),f.x),f.y),
			               lerp(lerp( hash(n+113.0), hash(n+114.0),f.x),
			                   lerp( hash(n+170.0), hash(n+171.0),f.x),f.y),f.z);
			}
    float rand(float3 vec){
        float random = dot(vec, float3(12.9898, 78.233, 37.719));
        random = frac(random) * sign(random);
        return random;
    }

      void vert (inout appdata_full v) {
        //float4 vPos = v.vertex * _VertexScale;  //local position
        float4 vPos = mul (unity_ObjectToWorld, v.vertex); //global position

        //float r = _Noise * rand(vPos);  //white noise
        //float r = _Noise * rand(vPos);
        float r = _Noise * noise(_Scale*(_Offset+vPos));  //simplex (Perlin) noise
        v.vertex.xyz += (v.normal * (_Amount+r)); //extrude by normal with noise
      }

      sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }