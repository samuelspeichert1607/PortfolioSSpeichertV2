#include "ofMain.h"
#include "ofxGui.h"
#include "Shape.h"
#pragma once
class Line : public Shape
{
public: // place public functions or variables declarations here

// methods, equivalent to specific functions of your class objects
	void setup();	// setup method, use this to setup your object's initial state
	void update();  // update method, used to refresh your objects properties
	void draw();    // draw method, this where you'll do the object's drawing

	bool changeVisibility();
	bool changeSelection();
	// variables
	bool selected;
	ofPolyline polylineObject;

	Line(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder, ofPolyline polyline);  // constructor - used to initialize an object, if no properties are passed the program sets them to the default value
private:
};


