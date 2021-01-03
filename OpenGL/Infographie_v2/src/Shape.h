#include "ofMain.h"
#include "ofxGui.h"
#include "Illumination.h"


#pragma once
class Shape
{
public: // place public functions or variables declarations here

// methods, equivalent to specific functions of your class objects
	virtual void setup();	// setup method, use this to setup your object's initial state
	virtual void update();  // update method, used to refresh your objects properties
	virtual void draw();    // draw method, this where you'll do the object's drawing
    virtual void draw3D();

	bool changeVisibility();
	bool changeSelection();
	// variables

	bool selected;
	float xObject;        // position
	float yObject;
	int dimObject;
	bool fillObject;
	bool visibleObject;
	ofColor shapeColorObject;  // color using ofColor type
	ofColor borderColorObject;
	int thicknessBorderObject;
	bool hasBeenDeleted = false;

	float r;              // distance avec l'origine
	float angleOrigine;   // angle par rapport avec l'origine
	float angleRotation;  // angle de rotation sur lui-même

	glm::vec3* verticles;

	Shape(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder);  // constructor - used to initialize an object, if no properties are passed the program sets them to the default value
	~Shape();


    Illumination::ShaderType m_shader;
    Illumination::MaterialType m_material;
};
