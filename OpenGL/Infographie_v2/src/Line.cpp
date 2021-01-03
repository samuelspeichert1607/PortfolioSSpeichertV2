#include "Line.h"
Line::Line(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder, ofPolyline polyline) : Shape(x, y, size, fill, shapeColor, borderColor, thicknessBorder)
{
	xObject = x;        // position
	yObject = y;
	polylineObject = polyline;
	dimObject = size;
	fillObject = fill;
	visibleObject = true;
	shapeColorObject = shapeColor;  // color using ofColor type
	borderColorObject = borderColor;
	thicknessBorderObject = thicknessBorder;
	selected = false;
	verticles = nullptr;
}

bool Line::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

bool Line::changeSelection()
{
	selected = !selected;
	return selected;
}


void Line::setup()
{
}

void Line::update()
{


}

void Line::draw()
{
	ofSetColor(shapeColorObject);
	polylineObject.draw();
}