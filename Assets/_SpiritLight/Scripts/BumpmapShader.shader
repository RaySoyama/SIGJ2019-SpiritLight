// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//
// download from http://www.console-dev.de/bin/Unity_Shader_Fake_Interior.zip
//
// this shader is part of an answer to the following forum post:
// http://forum.unity3d.com/threads/bump-offset-parallax-mapping-window-shader.407091/
//
// I believe this approach is rather unconventional.
// My idea was that I could use the clip space coordinates to offset the mesh texture coordinates.
// clip space is in range -1 to +1
// if the vertex is at the left screen edge, x would be -1 in clip space
// if the vertex is at the right screen edge, x would be +1 in clip space
// the sample principle applies for the y coordinate.
//
// if the interior quad is also mapped in this fashion (top-left = 0,0 and bottom-right=1,1)
// it should be possible to simply add the clip-space coordinates to the texture coordinates
// and we have the parallax effect already already working.
//
// however, if we look to the left we actually want to reveal more of the left part of the texture.
// just adding the clip-space coordinates would simple move the texture to the left, revealing more of
// the right texture part. so I simply changed the sign (subtract rather than add) to scroll it into
// the opposite direction.
//
// since we just modify texture coordinates, we can also do just everything in the vertex shader \o/
//
// in this file you can find 3 different tests, showing my hackery.
//

Shader "Rays Custom/Interior"
{
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}

	// The maximum parallax value should not be greater than
	// the uv border in the mesh. Usually you would uv map a quad from:
	// top-left=0,0 to bottom-right=1,1
	// however, the shader moves the uv's by the specified _Parallax value.
	// in order to never go outside the 0,0 .. 1,1 range, you "add a border to the uvs in the mesh".
	// To properly support a maximum parallax value of 0.25, you would uv map the quad as:
	// top-left=0.25,0.25 to bottom-right=0.75,0.75
	// if the shader adds the _Parallax value to the uvs, we have the full uv range again.
	_Parallax("Parallax", Range(0, 1)) = 0.25
	}

		CGINCLUDE
		sampler2D _MainTex;
	float4 _MainTex_ST;
	fixed4 _Color;
	float _Parallax;

	struct Input
	{
		float2 st_MainTex;
	};

	// test 2 seems to work well and I understand why it is working
#define TEST_2

#if defined(TEST_1)
	inline float2 InteriorUV(float4 vertex, float3 normal, float2 texcoord)
	{
		float4 clipSpace = UnityObjectToClipPos(vertex);
		clipSpace.y = -clipSpace.y;
		clipSpace = normalize(clipSpace);

		// applys texture scaling or displacement using the screen center as origin.
		// this results in undesired scretching at the screen borders.
		// you can still compensate by adjusting the texture tiling and offset in the material.
		// to get a _Parallax value of 0.25 to apply the scaling from the quad center, you would need
		// to modify the material settings as followed:
		//   Tiling X = 1.5    Y = 1.5
		//   Offset X = -0.25  Y = -0.25
		float2 offset = clipSpace.xy * _Parallax;
		return texcoord - offset;
	}
#endif

#if defined(TEST_2)
	inline float2 InteriorUV(float4 vertex, float3 normal, float2 texcoord)
	{
		float4 clipSpace = UnityObjectToClipPos(vertex);
		clipSpace.y = -clipSpace.y;
		clipSpace = normalize(clipSpace);

		// This is the same as TEST_1, but we modify the texcoord so that the
		// displacement occurs not using the screen center anymore, but the
		// quads center. this seems to look nice at all angles and positions.
		float2 offset = clipSpace.xy * _Parallax;
		return (texcoord + texcoord * _Parallax * 2) - _Parallax - offset;
	}
#endif

#if defined(TEST_3)
	// another approach that seems to work, figured out through trial&error.
	// I don't understand entirely why it works though :)
	// it seems to work really nice though.
	inline float2 InteriorUV(float4 vertex, float3 normal, float2 texcoord)
	{
		float3 distance = normalize(mul((float3x3)UNITY_MATRIX_MV, reflect(ObjSpaceViewDir(vertex), normal)));

		float2 offset = distance.xy * _Parallax;
		return texcoord - offset;
	}
#endif


	void vert(inout appdata_full v, out Input o)
	{
		UNITY_INITIALIZE_OUTPUT(Input, o);

		float2 texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.st_MainTex = InteriorUV(v.vertex, v.normal, texcoord);
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_MainTex, IN.st_MainTex) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
	}
	ENDCG


		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 500

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		#pragma target 3.0
		ENDCG
	}



		FallBack "Legacy Shaders/Diffuse"
}