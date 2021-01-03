#include "Square.h"

Square::Square(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder) : Shape(x, y, size, fill, shapeColor, borderColor, thicknessBorder)
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

	verticles = new glm::vec3[4];

	p.x = xObject - dimObject / 2;
	p.y = yObject - dimObject / 2;

	verticles[0].x = p.x;
	verticles[0].y = p.y;

	verticles[1].x = p.x + dimObject;
	verticles[1].y = p.y;

	verticles[2].x = p.x + dimObject;
	verticles[2].y = p.y + dimObject;

	verticles[3].x = p.x;
	verticles[3].y = p.y + dimObject;
}

bool Square::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

bool Square::changeSelection()
{
	selected = !selected;
	return selected;
}

void Square::setup()
{
}

void Square::update()
{
	r = sqrt((xObject * xObject) + (yObject * yObject));
	angleOrigine = (atan2(yObject, xObject) * (180 / PI));
	angleRotation = 0.0f;

	p.x = xObject - dimObject / 2;
	p.y = yObject - dimObject / 2;

	verticles[0].x = p.x;
	verticles[0].y = p.y;

	verticles[1].x = p.x + dimObject;
	verticles[1].y = p.y;

	verticles[2].x = p.x + dimObject;
	verticles[2].y = p.y + dimObject;

	verticles[3].x = p.x;
	verticles[3].y = p.y + dimObject;
}

void Square::draw()
{
	ofSetColor(shapeColorObject);

	p.x = verticles[0].x;
	p.y = verticles[0].y;
	if (fillObject) 
	{
		ofFill();
		ofSetColor(shapeColorObject);
		ofDrawRectangle(p, dimObject, dimObject);
	}
	ofNoFill();
	ofSetLineWidth(thicknessBorderObject);
	ofSetColor(borderColorObject);
	ofDrawRectangle(p, dimObject, dimObject);

}


void Square::draw3D()
{
    Illumination::getInstance()->shaderBegin(m_shader, m_material);

    {
        box.set(dimObject);
        box.setPosition(xObject, yObject, 0);
        vector<ofMeshFace> triangles = box.getMesh().getUniqueFaces();
        box.draw();
    }

    Illumination::getInstance()->shaderEnd(m_shader);
}
