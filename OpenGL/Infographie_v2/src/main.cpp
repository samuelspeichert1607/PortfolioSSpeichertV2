#include "ofMain.h"
#include "ofApp.h"
#include "ofAppPBR.h"
#include <thread>
//========================================================================
int main( ){
	//ofSetupOpenGL(1366,768,OF_WINDOW);            // <-------- setup the GL context
	ofGLFWWindowSettings window_settings;
	// this kicks off the running of my app
	// can be OF_WINDOW or OF_FULLSCREEN
	// pass in width and height too:


	window_settings.setSize(1366, 768);
	window_settings.setGLVersion(3, 3);
	window_settings.numSamples = 4;
	window_settings.doubleBuffering = true;
	ofCreateWindow(window_settings);
	ofRunApp(new ofApp());


	ofGLWindowSettings settings;
	settings.setGLVersion(4, 1);
	settings.setSize(1280, 720);
	ofCreateWindow(settings);
	ofRunApp(new ofAppPBR());
}
