#pragma once
#include "ofxGui.h"
#include "ofMain.h"
#include "Tree.h"
#include "Square.h"
#include "Shape.h"
#include "Triangle.h"
#include "Line.h"
#include "Circle.h"
#include "Dot.h"
#include "House.h"
#include "CatmullRom.h"
#include "Car.h"
#include "ofxGrafica.h"
#include <datGUI.h>
#include "ofxGui.h"
#include "Illumination.h"

class ofApp : public ofBaseApp{

public:
	void setup();
	void update();
	void draw();
	void keyPressed(int key);
	void keyReleased(int key);
	void mouseMoved(int x, int y );
	void mouseDragged(int x, int y, int button);
	void mousePressed(int x, int y, int button);
	void mouseReleased(int x, int y, int button);
	void mouseEntered(int x, int y);
	void mouseExited(int x, int y);
	void windowResized(int w, int h);
	void dragEvent(ofDragInfo dragInfo);
	void gotMessage(ofMessage msg);


	private:

        Illumination illumination_singleton;

		std::vector<Shape*> listShape;
        std::map<string,Shape*> mapShape;

		datGUI userInterface;

		//Fin Interface GUI
		//Tone Mapping

		ofSoundPlayer ring;

		/**ColorScheme**/
		ofxLabel RGBLabel;
		ofxLabel contourLaBel;
		ofxIntSlider redRGB;
		ofxIntSlider greenRGB;
		ofxIntSlider blueRGB;
		ofxLabel fillLabel;
		ofxIntSlider redRGB1;
		ofxIntSlider greenRGB1;
		ofxIntSlider blueRGB1;

		ofxLabel HSBLabel;
		ofxLabel contourLaBel1;
		ofxIntSlider hueHSB;
		ofxIntSlider saturationHSB;
		ofxIntSlider brightnessHSB;
		ofxLabel fillLabel1;
		ofxIntSlider hueHSB1;
		ofxIntSlider saturationHSB1;
		ofxIntSlider brightnessHSB1;
		ofxToggle RGBorHSB;

		ofxIntSlider sizeStroke;

		ofColor currentColorSpaceStroke;
		ofColor currentColorSpaceFill;

		ofColor RGBSystemStroke;
		ofColor RGBSystemFill;
		ofColor HSBSystemStroke;
		ofColor HSBSystemFill;



		bool isRGB;

		void updateColor();
		void setupSelectionForm();
		void changeColorStroke();
		void changeColorFill();
		

		/**Vector form **/
		bool clikScreen;
		void drawPoint(int x,int y);
		void drawCircle(int x, int y,int size,bool fill,int sizeStroke);
		void drawTriangle(int x, int y,int size, bool fill,int sizeStroke); //Center of mass of the triangle
		void drawSquare(int x, int y,int size,bool fill,int sizeStroke); //Center of the square
		void drawTree(int x, int y, int size, bool fill);
		void drawHouse(int x, int y, int size);

		ofPolyline polyline;
		void drawLine();
		void dragLine(int x, int y);
		void PressLine(int x,int y);

		//Catmull
		void addPoint(int x, int y);
		vector<ofPoint> ctrlPoints;
		/****/

		/*Surface*/
		Surface * surface;

		bool checkIfCursorOutsideUI();

		/*ChooseFigure*/
		bool choosePoint;
		bool chooseLine;
		bool chooseCircle;
		bool chooseTriangle;
		bool chooseSquare;
		bool chooseTree;
		bool chooseHouse;
		/**/

		/*Histogram Builder*/
		//Made by dividing 255 by 4. Intervals [0,63],[64,127],[128,191],[192,255]

		float redCount[4];
		float greenCount[4];
		float blueCount[4];
		ofxGPlot plot;
		void setupHistogram();
		void fillCountRGB(ofImage im); //Count how many in the different intervals



		/*Import Image*/

		ofImage image;
		ofxButton ImportButton;
		bool imageLoaded;
		void importImage();
		/**/

		/* 3D Mode*/

		bool is3DMode;

		ofLight light;
		ofImage texture;
		ofShader shader_filtres;

		bool checkIntersectionCircle(float radius, float posCircleX, float posCircleY, float posMouseX, float posMouseY);
		ofImage generateProceduralTexture(int width, int height);

		//Verifier si on a click� � l'int�rieur d'un entit� :
        bool onSegment(glm::vec3 p, glm::vec3 q, glm::vec3 r);

        int orientation(glm::vec3 p, glm::vec3 q, glm::vec3 r);

        bool doIntersect(glm::vec3 p1, glm::vec3 q1, glm::vec3 p2, glm::vec3 q2);

        bool isInside(glm::vec3 polygon[], int n, glm::vec3 p);
};
