#include "Car.h"
#include "Shape.h"

Car::Car(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder) : Shape(x, y, size, fill, shapeColor, borderColor, thicknessBorder)
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
	TreeModel.loadModel("Car.obj", 10);
}

bool Car::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

void Car::draw3D()
{
    Illumination::getInstance()->shaderBegin(m_shader, m_material);
    {
        ofSetLogLevel(OF_LOG_VERBOSE);

        ofDisableArbTex();
        //some model / light stuff
        ofEnableDepthTest();
        TreeModel.setScale(0.1, 0.1, 0.1);
        TreeModel.setRotation(0, 0, 0, 0, 0);
        TreeModel.setPosition(1366/2, 768/2, 0);
        TreeModel.enableTextures();

        //model.setLoopStateForAllAnimations(OF_LOOP_NORMAL);
        //model.playAllAnimations();

        ofEnableBlendMode(OF_BLENDMODE_ALPHA);
        ofEnableDepthTest();
        glShadeModel(GL_SMOOTH);

        ofEnableSeparateSpecularLight();
        TreeModel.drawFaces();
    }
    Illumination::getInstance()->shaderEnd(m_shader);

}

bool Car::changeSelection()
{
	selected = !selected;
	return selected;
}

void Car::setup()
{
}

void Car::update()
{


}

void Car::draw()
{
}
