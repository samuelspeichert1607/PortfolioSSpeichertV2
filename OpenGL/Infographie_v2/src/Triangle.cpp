#include "Triangle.h"
Triangle::Triangle(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder) : Shape(x, y, size, fill, shapeColor, borderColor, thicknessBorder)
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

	verticles = new glm::vec3[3];

	verticles[0].x = xObject - dimObject;
	verticles[0].y = yObject + dimObject;
	verticles[1].x = xObject + dimObject;
	verticles[1].y = yObject + dimObject;
	verticles[2].x = xObject;
	verticles[2].y = yObject - dimObject * 2;
}

bool Triangle::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

bool Triangle::changeSelection()
{
	selected = !selected;
	return selected;
}
void Triangle::setup()
{
}

void Triangle::update()
{
	r = sqrt((xObject * xObject) + (yObject * yObject));
	angleOrigine = (atan2(yObject, xObject) * (180 / PI));
	angleRotation = 0.0f;

	verticles[0].x = xObject - dimObject;
	verticles[0].y = yObject + dimObject;
	verticles[1].x = xObject + dimObject;
	verticles[1].y = yObject + dimObject;
	verticles[2].x = xObject;
	verticles[2].y = yObject - dimObject * 2;
}

void Triangle::draw()
{
	ofSetColor(shapeColorObject);
	if (fillObject) 
	{
		ofFill();
		ofSetColor(shapeColorObject);
		ofDrawTriangle(verticles[0].x, verticles[0].y, verticles[1].x, verticles[1].y, verticles[2].x, verticles[2].y);
	}
	ofNoFill();
	ofSetColor(borderColorObject);
	ofSetLineWidth(thicknessBorderObject);
	ofDrawTriangle(verticles[0].x, verticles[0].y, verticles[1].x, verticles[1].y, verticles[2].x, verticles[2].y);
}

void Triangle::draw3D()
{
    Illumination::getInstance()->shaderBegin(m_shader, m_material);
    {
        cone.set(dimObject/3, dimObject, 25, 75);
        cone.setPosition(xObject, yObject, 0);
        vector<ofMeshFace> triangles = cone.getMesh().getUniqueFaces();
        cone.draw();
    }
    Illumination::getInstance()->shaderEnd(m_shader);
}
