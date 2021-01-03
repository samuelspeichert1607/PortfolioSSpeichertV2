#include "Circle.h"
Circle::Circle(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder) : Shape(x, y, size, fill, shapeColor, borderColor, thicknessBorder)
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

bool Circle::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

bool Circle::changeSelection()
{
	selected = !selected;
	return selected;
}

void Circle::setup()
{
}

void Circle::update()
{
	r = sqrt((xObject * xObject) + (yObject * yObject));
	angleOrigine = (atan2(yObject, xObject) * (180 / PI));
}

void Circle::draw()
{
	ofSetColor(shapeColorObject);
	if (fillObject) 
	{
		ofFill();
		ofSetColor(shapeColorObject);
		ofDrawCircle(xObject, yObject, dimObject);
	}
	ofNoFill();
	ofSetLineWidth(thicknessBorderObject);
	ofSetColor(borderColorObject);
	ofDrawCircle(xObject, yObject, dimObject);
}

void Circle::draw3D()
{
	sphere.setRadius(dimObject);
	sphere.setPosition(xObject, yObject, 0);
	vector<ofMeshFace> triangles = sphere.getMesh().getUniqueFaces();
	sphere.draw();
}
