/*
 *  Pass-Through.frag
 *  Hexels 2!
 *
 *  Created by Kenneth Kopecky in May 2016
 *  Copyright 2016 Marmoset Co. All rights reserved.
 *
 */

#define PFX_SHADER		//lets Hexels know this is a post-effect shader as opposed to a blend shader
//#define FULLSCREEN
#define CATEGORY Distort

UI_Uniform_Checkbox(Pixel_Perfect, 0);		//change this to randomize the noise.  Be cautious of animating it.

UI_Uniform_Float(Granularity, 0.01, 0.99, 0.6); //Affects the prominece of fine details
UI_Uniform_Float(Scaling, 0.01, 10, 6);			//Controls the overall size of the noise pattern
UI_Uniform_Float(Amount, 0.00, 8, 3);			//Controls the amount of distortion
UI_Uniform_Float(Phase, 0, 1, 0);				//Use to manually animate the distortion
UI_Uniform_Integral(Speed, 0, 10, 1);			//Use to automatically animate the distortion
//UI_Uniform_Int(Seed, 0, 1000, 0);		//change this to randomize the noise.  Be cautious of animating it.

PADDING_CANVAS(Scaling)

//Description:  Uses Perlin noise as a basis for refracting the image.

vec2 pixelAlign(vec2 texCoord) 
{
	vec2 pixCoord = texCoord * _BufferSize;
	pixCoord = floor(pixCoord + vec2(0.5,0.5)) + vec2(0.5,0.5);
	pixCoord /= _BufferSize;
	return mix(texCoord, pixCoord, float(Pixel_Perfect));
}

float deltaSeed;
float noise(vec2 co)
{
    return fract(sin(dot(co ,vec2(12.9898,78.233))) * (43758.5453 + deltaSeed));

}

vec2 wrap = vec2(64.0);

float rand(vec2 co, float scale)
{
    vec2 pos = co;
    ivec2 ipos = ivec2(co / scale);
    pos = vec2(ipos) * scale;
    float r = noise(pos);
    return r;
}

float cubicInterpolate(vec4 p, float x)
{
	return p.y + 0.5 * x*(p.z - p.x + x*(2.0*p.x - 5.0*p.y + 4.0*p.z - p.w + x*(3.0*(p.y - p.z) + p.w - p.x)));
}   


float bicubicNoise(vec2 co, float scale)
{
     //16!! points to interpolate across
    mat4 m;
	ivec2 ipos = ivec2(co/scale);
	vec2 qpos = vec2(ipos) * scale;
	vec2 t = (co-qpos) / scale;
    for(int x = 0; x < 4; x++)
    {
    	for(int y = 0; y < 4; y++)
        {
			vec2 np = vec2(ipos + ivec2(x, y) - vec2(1.5, 1.5)) * scale - 0.0;
			np = mod(np, wrap);
         	m[x][y] = noise(np);
        }
    }
	
	//find our t-values by subtracting pos from quantized pos
    float t1 = t.x;
    t1 = t.y;
    //do cubic interpolation four times, and then one more time
    vec4 cubix = vec4(
        cubicInterpolate(m[0], t1),
        cubicInterpolate(m[1], t1),
        cubicInterpolate(m[2], t1),
        cubicInterpolate(m[3], t1));
   	return cubicInterpolate(cubix, t.x);
	
}


float totalAmplitude;
float perlinNoise(vec2 co)
{
	float rez = 4.0;
	float n = 0.0;
	float amplitude = 1.0;
	totalAmplitude = 0.0;
	float theta = 0.0;
	float octaves = clamp(log(0.05) / log(Granularity), 1.0, 12.0);
	float rezOctaves = clamp(log(0.25 / _Zoom/rez) / log(0.5), 1.0, 12.0);	//how many octaves till our resolution becomes sub-pixel?
//	octaves = min(octaves, rezOctaves);
	for(int i = 0; i < octaves; i++)
	{
//		vec2 lco = vec2(co.x * sin(theta) + co.y * cos(theta), co.x * cos(theta) + co.y * sin(theta));
//		theta += 0.8;
		float mult = min(octaves-i, 1.0);
		float binoise = bicubicNoise(co, rez);
		binoise = 0.5 + 0.5 * sin((binoise + Phase + Speed / (max(float(_NumFrames)-1.0, 1.0)) + float(i) * 0.1) * 3.14159 * 2.0);
		n += binoise * amplitude * mult;
		totalAmplitude += amplitude * mult;
		amplitude *= Granularity;
		rez *= 0.5;
	
	}
	
//	n /= totalAmplitude;
//	n = (0.5 + 0.5 * sin((n * 1.25 + Phase) * 3.14159 * 2.0));
	return n;
}

float noise21(vec2 co)
{
//	return 0.0;
    return fract(sin(co) * 0.25).x;
}


void main()
{
	deltaSeed = 0;
	vec2 co = gl_TexCoord[1].xy;
	vec2 wrapCanvas = floor(_CanvasSize / 16.0) * 16.0;		//need a very round number for wrapping
	wrap = wrapCanvas;
	co = mod(co / Scaling, wrap);


	//grab four samples to determine the normal at this location
	float dC = 1.0 / 50.0;	//sampling interval for the noise
	float Scale = 1.0;
	float rx0 = perlinNoise((co + vec2(-dC, 0.0)) / Scale) * 2.0 - totalAmplitude;
	float rx1 = perlinNoise((co + vec2(dC, 0.0)) / Scale) * 2.0 - totalAmplitude;
	float ry0 = perlinNoise((co + vec2(0, -dC)) / Scale) * 2.0 - totalAmplitude;
	float ry1 = perlinNoise((co + vec2(0, dC)) / Scale) * 2.0 - totalAmplitude;
	vec2 distort;
//	distort = vec2(r, g);
	vec2 texCoord = gl_TexCoord[0].xy;
	vec2 delta;// = vec2(dFdx(r), dFdy(r)) * Scale;
	delta = vec2(rx1-rx0, ry1-ry0) * Scaling * Amount * 0.80 * _EffectStrength;
	distort = delta;
	texCoord += distort / _BufferSize * _Zoom;

	//pixel perfect texture lookup
	texCoord = pixelAlign(texCoord);
	//use PMASample here.  It's a tiny bit slower, but a regular sample will get some artifacts
	vec4 incoming = PMASample(_Layer, texCoord, vec2(1.0) / _BufferSize);

	gl_FragColor = incoming;

}

