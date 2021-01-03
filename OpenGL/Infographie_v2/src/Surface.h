#pragma once
#include "ofMain.h"
#include <vector>
#include "Shape.h"
class Surface :public Shape{
public:
	Surface(int precision, ofColor colorShape, ofColor borderColor, int range=5);
	Surface(int precision, ofColor colorShape, ofColor borderColor, float a1, float b1, float c1, float d1, float e1, float f1, float a2, float b2, float c2, float d2, float e2, float f2, float a3, float b3, float c3, float d3, float e3, float f3);
	ofVec3f getPoint(float u, float v);
	vector<ofVec3f> getPoints();
	void generatePoints();
	void draw();
	void clearSurface();

	void setA1(float x);
	void setA2(float x);
	void setA3(float x);
	void setB1(float x);
	void setB2(float x);
	void setB3(float x);
	void setC1(float x);
	void setC2(float x);
	void setC3(float x);
	void setD1(float x);
	void setD2(float x);
	void setD3(float x);
	void setE1(float x);
	void setE2(float x);
	void setE3(float x);
	void setF1(float x);
	void setF2(float x);
	void setF3(float x);


	

private:
	float float_rand(float min, float max);
	float a1, a2, a3, b1, b2, b3, c1, c2, c3, d1, d2, d3,e1,e2,e3,f1,f2,f3;
	int precision;
	int range;
	ofPolyline polyline;
	vector<ofVec3f> points;
};