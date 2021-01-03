#include "Dot.h"
Dot::Dot(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder) : Shape(x, y, size, fill, shapeColor, borderColor, thicknessBorder)
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

bool Dot::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

bool Dot::changeSelection()
{
	selected = !selected;
	return selected;
}

void Dot::setup()
{
}

void Dot::update()
{

}

void Dot::draw()
{
	ofFill();
	ofSetColor(shapeColorObject);
	ofDrawCircle(xObject, yObject, 2);
}