#include "CatmullRom.h"

void CatmullRom::setup()
{
}

void CatmullRom::update()
{
}

void CatmullRom::draw()
{
	for (int i = 0; i < ctrlPoints.size(); i++) {
		ofFill();
		ofDrawCircle(ctrlPoints[i], 5);
	}
	generateCurve();
	ofSetColor(shapeColorObject);
	polylineObject.draw();
}

bool CatmullRom::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

bool CatmullRom::changeSelection()
{
	selected = !selected;
	return selected;
}

CatmullRom::CatmullRom(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder, vector<ofPoint> ctrlPoints): Shape(x,y,size,fill,shapeColor,borderColor,thicknessBorder),ctrlPoints(ctrlPoints)
{
	xObject = x;        // position
	yObject = y;
	dimObject = size;
	fillObject = fill;
	visibleObject = true;
	shapeColorObject = shapeColor;  // color using ofColor type
	borderColorObject = borderColor;
	thicknessBorderObject = thicknessBorder;
	selected = false;
	verticles = nullptr;
}

void CatmullRom::generateCurve()
{
	vector<float> time;
	time.clear();
	time.push_back(0.0f);
	polylineObject.clear();

	for (int i = 1; i < ctrlPoints.size(); i++) {
		time.push_back(getT(time[i - 1], ctrlPoints[i - 1], ctrlPoints[i]));
	}

	int internalLoop = ctrlPoints.size() - 3;

	for (int i = 0; i < internalLoop; i++) {
		for (float t = time[i + 1]; t < time[i + 2]; t += ((time[i + 2] - time[i + 1]) / (float)numberOfPoints)) {
			ofPoint A1 = (time[i + 1] - t) / (time[i + 1] - time[i])*ctrlPoints[i] + (t - time[i]) / (time[i + 1] - time[i])*ctrlPoints[i + 1];
			ofPoint A2 = (time[i + 2] - t) / (time[i + 2] - time[i + 1])*ctrlPoints[i + 1] + (t - time[i + 1]) / (time[i + 2] - time[i + 1])*ctrlPoints[i + 2];
			ofPoint A3 = (time[i + 3] - t) / (time[i + 3] - time[i + 2])*ctrlPoints[i + 2] + (t - time[i + 2]) / (time[i + 3] - time[i + 2])*ctrlPoints[i + 3];

			ofPoint B1 = (time[i + 2] - t) / (time[i + 2] - time[i])*A1 + (t - time[i]) / (time[i + 2] - time[i])*A2;
			ofPoint B2 = (time[i + 3] - t) / (time[i + 3] - time[i + 1])*A2 + (t - time[i + 1]) / (time[i + 3] - time[i + 1])*A3;

			ofPoint C = (time[i + 2] - t) / (time[i + 2] - time[i + 1])*B1 + (t - time[i + 1]) / (time[i + 2] - time[i + 1])*B2;

			polylineObject.addVertex(C);
		}
		polylineObject.addVertex(ctrlPoints[i + 2]);
	}
}

float CatmullRom::getT(float t, ofPoint p0, ofPoint p1)
{
	float a = pow((p1.x - p0.x), 2.0f) + pow((p1.y - p0.y), 2.0f);
	float b = pow(a, 0.5f);
	float c = pow(b, alpha);
	return (c + t);
}
