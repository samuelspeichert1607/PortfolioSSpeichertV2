#include "Shape.h"
Shape::Shape(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder)
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
    m_shader = Illumination::ShaderType::none;
    m_material = Illumination::MaterialType::materiau1;

}

Shape::~Shape()
{
}

bool Shape::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

bool Shape::changeSelection()
{
	selected = !selected;
	return selected;
}



void Shape::setup()
{
}

void Shape::update()
{


}

void Shape::draw()
{
	if (fillObject)
	{
		ofFill();
		ofDrawCircle(xObject, yObject, dimObject);
	}
	ofNoFill();
	ofDrawCircle(xObject, yObject, dimObject);
}

void Shape::draw3D()
{

}
