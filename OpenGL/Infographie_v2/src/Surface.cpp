#include "Surface.h"
#include <algorithm>
#include <iostream>

Surface::Surface(int precision,ofColor colorShape,ofColor borderColor,int range):Shape(0,0,0,true,colorShape,borderColor,5),precision(precision)
{
	this->a1 = float_rand(-range, range);
	this->b1 = float_rand(-range, range);
	this->c1 = float_rand(-range, range);
	this->d1 = float_rand(-range, range);
	this->e1 = float_rand(-range, range);
	this->f1 = float_rand(-range, range);
	this->a2 = float_rand(-range, range);
	this->b2 = float_rand(-range, range);
	this->c2 = float_rand(-range, range);
	this->d2 = float_rand(-range, range);
	this->e2 = float_rand(-range, range);
	this->f2 = float_rand(-range, range);
	this->a3 = float_rand(-range, range);
	this->b3 = float_rand(-range, range);
	this->c3 = float_rand(-range, range);
	this->d3 = float_rand(-range, range);
	this->e3 = float_rand(-range, range);
	this->f3 = float_rand(-range, range);

}

Surface::Surface(int precision,ofColor colorShape,ofColor borderColor,float a1, float b1, float c1,float d1, float e1,float f1,float a2, float b2, float c2, float d2, float e2, float f2, float a3, float b3, float c3, float d3, float e3, float f3 ):Shape(0,0,0,true,colorShape,borderColor,5)
{
	this->precision = precision;
	this->a1 = a1;
	this->b1 = b1;
	this->c1 = c1;
	this->d1 = d1;
	this->e1 = e1;
	this->f1 = f1;
	this->a2 = a2;
	this->b2 = b2;
	this->c2 = c2;
	this->d2 = d2;
	this->e2 = e2;
	this->f2 = f2;
	this->a3 = a3;
	this->b3 = b3;
	this->c3 = c3;
	this->d3 = d3;
	this->e3 = e3;
	this->f3 = f3;
}



ofVec3f Surface::getPoint(float u, float v)
{
	float x = a1 * u*u + b1 * u*v + c1 * v*v + d1 * u + e1 * v + f1;
	float y = a2 * u*u + b2 * u*v + c2 * v*v + d2 * u + e2 * v + f2;
	float z = a3 * u*u + b3 * u*v + c3 * v*v + d3 * u + e3 * v + f3;
	return ofVec3f(x,y,z);
}

vector<ofVec3f> Surface::getPoints()
{
	return points;
	
}

// compare for a vector of floats, sorting from lowest to highest
bool my_compare(ofVec3f a, ofVec3f b) {
	return a.x < b.x;
}

// compare for a vector of floats, sorting from lowest to highest
bool my_compare1(ofVec3f a, ofVec3f b) {
	return  a.y <  b.y;
}

void Surface::generatePoints()
{
	points.clear();
	for (int i = 0; i < ofGetWidth(); i+=precision) {
		for (int j = 0; j < ofGetHeight();j+=precision) {
			ofVec3f pointant = getPoint(i, j);
			points.push_back(pointant);
		

			
		}
	}
	float minX, minY, minZ, maxX, maxY, maxZ;
	minX = points[0].x;
	minY = points[0].y;
	minZ = points[0].z;
	maxX = points[0].x;
	maxY = points[0].y;
	maxZ = points[0].z;
	for (int i = 1; i < points.size(); i++) {
		float xCurrent = points[i].x;
		float yCurrent = points[i].y;
		float zCurrent = points[i].z;
		if (xCurrent < minX) {
			minX = xCurrent;
		}
		if (xCurrent > maxX) {
			maxX = xCurrent;
		}
		if (yCurrent < minY) {
			minY = yCurrent;
		}
		if (yCurrent > maxY) {
			maxY = yCurrent;
		}
		if (zCurrent < minZ) {
			minZ = zCurrent;
		}
		if (zCurrent > maxZ) {
			maxZ = zCurrent;
		}
	}

	float deltaX = maxX - minX;
	float deltaY = maxY - minY;
	float deltaZ = maxZ - minZ;

	for (int i = 0; i < points.size(); i++) {
		float tmpX = points[i].x;
		float tmpY = points[i].y;
		float tmpZ = points[i].z;

		points[i].x = (int)(((tmpX - minX) / deltaX)*ofGetWidth());
		points[i].y = (int)(((tmpY - minY) / deltaY)*ofGetHeight());
		points[i].z = (int)(((tmpZ - minZ) / deltaZ)*10);

	}




}



void Surface::draw()
{
	ofFill();
	for (int i = 0; i < points.size(); i++) {
	//	cout << "point[" << i << "]=(" << points[i].x << "," << points[i].y << "," << points[i].z << ")" << endl;
		ofDrawCircle(points[i].x,points[i].y, points[i].z);
	}
	for (int i = 0; i < points.size(); i++) {
			for (int j = 0; j < points.size(); j++) {
				ofDrawLine(points[j], points[i]);
			}
	}

	polyline.draw();
}

void Surface::clearSurface()
{
	points.clear();
}

void Surface::setA1(float x)
{
	a1 = x;
}

void Surface::setA2(float x)
{
	a2 = x;
}

void Surface::setA3(float x)
{
	a3 = x;
}

void Surface::setB1(float x)
{
	b1 = x;

}

void Surface::setB2(float x)
{
	b2 = x;
}

void Surface::setB3(float x)
{
	b3 = x;
}

void Surface::setC1(float x)
{
	c1 = x;
}

void Surface::setC2(float x)
{
	c2 = x;
}

void Surface::setC3(float x)
{
	c3 = x;
}

void Surface::setD1(float x)
{
	d1 = x;
}

void Surface::setD2(float x)
{
	d2 = x;
}

void Surface::setD3(float x)
{
	d3 = x;
}

void Surface::setE1(float x)
{
	e1 = x;
}

void Surface::setE2(float x)
{
	e2 = x;
}

void Surface::setE3(float x)
{
	e3 = x;
}

void Surface::setF1(float x)
{
	f1 = x;
}

void Surface::setF2(float x)
{
	f2 = x;
}

void Surface::setF3(float x)
{
	f3 = x;
}

float Surface::float_rand(float min, float max)
{
	float scale = rand() / (float)RAND_MAX; /* [0, 1.0] */
	return min + scale * (max - min);      /* [min, max] */
}
