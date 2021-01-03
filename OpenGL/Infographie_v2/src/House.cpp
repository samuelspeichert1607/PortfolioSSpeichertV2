#include "House.h"
House::House(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder) : Shape(x, y, size, fill, shapeColor, borderColor, thicknessBorder)
{
	xObject = x;        // position
	yObject = y;
	dimObject = size;
	fillObject = fill;
	visibleObject = true;
	shapeColorObject = shapeColor;  // color using ofColor type
	borderColorObject = borderColor;
	thicknessBorderObject = thicknessBorder;
	HouseModel.loadModel("House.obj", 10);
	selected = false;

	verticles = nullptr;
}

bool House::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

bool House::changeSelection()
{
	selected = !selected;
	return selected;
}

void House::setup()
{
}

void House::update()
{
	r = sqrt((xObject * xObject) + (yObject * yObject));
	angleOrigine = (atan2(yObject, xObject) * (180 / PI));
}

void House::draw()
{
	glm::vec3 p;
	p.x = xObject - dimObject / 2;
	p.y = yObject - dimObject / 2;

	ofSetColor(ofColor(255, 255, 255));
	ofDrawRectangle(p, dimObject, dimObject);

	/* Partie Triangle*/
	/* Test */
	ofSetColor(ofColor(255, 0, 0));
	ofDrawTriangle(p.x, p.y, p.x + dimObject / 2, p.y - dimObject / 2, p.x + dimObject, p.y);
}

void House::draw3D()
{
    //IlluminationSingleton::getInstance()->shaderBegin(m_shader);
    {
        ofSetColor(ofColor(255, 255, 255));
        ofSetLogLevel(OF_LOG_VERBOSE);

        ofDisableArbTex();
        //some model / light stuff
        ofEnableDepthTest();
        HouseModel.setScale(0.1, 0.1, 0.1);
        HouseModel.setRotation(0, 90, 1, 0, 0);
        HouseModel.setPosition(xObject, yObject, 0);
        HouseModel.enableTextures();

        //model.setLoopStateForAllAnimations(OF_LOOP_NORMAL);
        //model.playAllAnimations();

        ofEnableBlendMode(OF_BLENDMODE_ALPHA);
        ofEnableDepthTest();
        glShadeModel(GL_SMOOTH);

        ofEnableSeparateSpecularLight();
        HouseModel.drawFaces();
    }
   // IlluminationSingleton::getInstance()->shaderEnd(m_shader);

}
