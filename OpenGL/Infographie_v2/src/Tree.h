#include "ofMain.h"
#include "ofxGui.h"
#include "Shape.h"
#include "ofxAssimpModelLoader.h"
#pragma once
class Tree : public Shape
{
	public: // place public functions or variables declarations here

	// methods, equivalent to specific functions of your class objects
		virtual void setup();	// setup method, use this to setup your object's initial state
		virtual void update();  // update method, used to refresh your objects properties
		virtual void draw();    // draw method, this where you'll do the object's drawing
		void draw3D();

		ofxAssimpModelLoader TreeModel;
		bool changeVisibility();
		bool changeSelection();
		// variables
		bool selected;
		Tree(int x, int y, int size, bool fill, ofColor shapeColor, ofColor borderColor, int thicknessBorder);  // constructor - used to initialize an object, if no properties are passed the program sets them to the default value
	private:
};


