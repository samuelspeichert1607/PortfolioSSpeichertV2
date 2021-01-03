#include "ofMain.h"
#include "ofxGui.h"
#include "Shape.h"
#include "ofxAssimpModelLoader.h"
#pragma once
class House : public Shape
{
public: // place public functions or variables declarations here

// methods, equivalent to specific functions of your class objects
	void setup();	// setup method, use this to setup your object's initial state
	void update();  // update method, used to refresh your objects properties
	void draw();    // draw method, this where you'll do the object's drawing
	void draw3D();

	ofxAssimpModelLoader HouseModel;
	bool changeVisibility();
	bool changeSelection();
	// variables
	bool selected;
	float xObject;        // position
	float yObject;
	int dimObject;
	bool fillObject;
	bool visibleObject;
	ofColor color;  // color using ofColor type

	House(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder);  // constructor - used to initialize an object, if no properties are passed the program sets them to the default value
private:
};


