#include "Tree.h"
#include "Shape.h"

Tree::Tree(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder) : Shape(x, y, size, fill, shapeColor, borderColor, thicknessBorder)
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
	TreeModel.loadModel("Tree.obj", 10);

	verticles = nullptr;
}

bool Tree::changeVisibility()
{
	visibleObject = !visibleObject;
	return visibleObject;
}

void Tree::draw3D()
{
	ofSetColor(ofColor(255, 255, 255));
	ofSetLogLevel(OF_LOG_VERBOSE);

	ofDisableArbTex();
	//some model / light stuff  
	ofEnableDepthTest();
	TreeModel.setScale(0.1, 0.1, 0.1);
	TreeModel.setPosition(xObject, yObject, 0);
	TreeModel.enableTextures();

	//model.setLoopStateForAllAnimations(OF_LOOP_NORMAL);  
	//model.playAllAnimations();  

	ofEnableBlendMode(OF_BLENDMODE_ALPHA);
	ofEnableDepthTest();
	glShadeModel(GL_SMOOTH);

	ofEnableSeparateSpecularLight();
	TreeModel.drawFaces();
}

bool Tree::changeSelection()
{
	selected = !selected;
	return selected;
}

void Tree::setup() 
{
}

void Tree::update() 
{
	r = sqrt((xObject * xObject) + (yObject * yObject));
	angleOrigine = (atan2(yObject, xObject) * (180 / PI));
}

void Tree::draw() 
{
	/* Partie Triangle*/
	glm::vec3 p;
	p.x = xObject - dimObject / 4;
	p.y = yObject + 20;

	ofFill();
	ofSetColor(ofColor(139, 69, 19));
	ofDrawRectangle(p, dimObject / 2, dimObject);
	ofSetColor(ofColor(34, 139, 34));
	ofDrawTriangle(xObject - dimObject, yObject + dimObject, xObject + dimObject, yObject + dimObject, xObject, yObject - dimObject * 2);
}