#include "ofApp.h"
#include "ofxGui.h"
#include <vector>
#include "ofMain.h"
#include "ofPixels.h"
#include "ofxOpenCv.h"
#include "Illumination.h"

using namespace glm;



//--------------------------------------------------------------
void ofApp::setup() {
	ofSetFrameRate(60);
	setupSelectionForm();
	ImportButton.addListener(this, &ofApp::importImage);
	imageLoaded = false;
	isRGB = true;
	is3DMode = false;
	userInterface.setup();

	currentColorSpaceStroke = RGBSystemStroke;
	currentColorSpaceFill = RGBSystemFill;

    //Car* Voiture = new Car(0, 0, 0, true, currentColorSpaceFill, currentColorSpaceStroke, 2);
    //listShape.push_back(Voiture);
    //userInterface.ShapesScrollView->add("Voiture");

	surface = new Surface(150, currentColorSpaceFill, currentColorSpaceStroke);
	userInterface.setSurface(surface);
	listShape.push_back(surface);

    //Car* Voiture = new Car(0, 0, 0, true, currentColorSpaceFill, currentColorSpaceStroke, 2);
	//listShape.push_back(Voiture);

	ofDisableArbTex();
	texture = generateProceduralTexture(512, 512);

	shader_filtres.load("shader/filtres_vs.glsl", "shader/filtres_fs.glsl");
}

//--------------------------------------------------------------
void ofApp::update() {

    if (userInterface.get3DMode())
    {
        illumination_singleton.light[0].setGlobalPosition(
            ofMap(ofGetMouseX() / (float)ofGetWidth(), 0.0f, 1.0f, -ofGetWidth() / 2.0f, ofGetWidth() / 2.0f),
            ofMap(ofGetMouseY() / (float)ofGetHeight(), 0.0f, 1.0f, -ofGetHeight() / 2.0f, ofGetHeight() / 2.0f),
            -100 * 1.5f);
    }


		// transformer la lumière
	light.setGlobalPosition(
			ofMap(ofGetMouseX() / (float)ofGetWidth(), 0.0f, 1.0f, -ofGetWidth() / 2.0f, ofGetHeight() / 2.0f),
			ofMap(ofGetMouseY() / (float)ofGetHeight(), 0.0f, 1.0f, -ofGetHeight() / 2.0f, ofGetHeight() / 2.0f),
			-100.0f * 1.5f);

	for (int i = 0; i < listShape.size(); i++)
	{
		listShape[i]->update();
		if (listShape[i]->hasBeenDeleted == true) {
			userInterface.primitiveGui->setVisible(false);
			listShape.erase(listShape.begin() + i);
		}
	}

	userInterface.update();
	updateColor();
	
	changeColorFill();
	changeColorStroke();

	if(userInterface.getChosenImportTexture() == true){
		ofDisableArbTex();
		ofFileDialogResult result = ofSystemLoadDialog("Load File");
		if (result.bSuccess) {
			string path = result.getPath();
			cout << path;
			texture.load(path);
			imageLoaded = true;
		}
		userInterface.setChosenImportTexture(false);
	}
	if (userInterface.getChosenProceduralTexture() == true) {
		ofDisableArbTex();
		texture = generateProceduralTexture(512, 512);
		userInterface.setChosenProceduralTexture(false);
	}
}

//--------------------------------------------------------------
void ofApp::draw() {
	// activer l'occlusion en profondeur

	ofBackground(userInterface.backgroundColorPicker->getColor());

	is3DMode = userInterface.get3DMode();
	if (is3DMode)
	{
		illumination_singleton.enable();
        {
            ofEnableDepthTest();
            {
                if(userInterface.threeDeeActivateTM->getChecked())
                {
                    shader_filtres.begin();
                    {
                        shader_filtres.setUniformTexture("image", texture.getTexture(), 1);

                        shader_filtres.setUniform1f("tone_mapping_exposure", userInterface.threeDeeExposure->getValue());
                        shader_filtres.setUniform1f("tone_mapping_gamma", userInterface.threeDeeGamma->getValue());
                        shader_filtres.setUniform1i("tone_mapping_toggle", userInterface.threeDeeToggle->getChecked());

                        shader_filtres.setUniform1f("choix_filtre", userInterface.getChosenFilter());
                        shader_filtres.setUniform3f("tint", userInterface.threeDeeTeinteCP->getColor().r / 255.0f,
                            userInterface.threeDeeTeinteCP->getColor().g / 255.0f,
                            userInterface.threeDeeTeinteCP->getColor().b / 255.0f);
                        shader_filtres.setUniform1f("factor", userInterface.threeDeeMixFactor->getValue());
                        shader_filtres.setUniform1f("intensite_filtre", userInterface.threeDeeConvolutionIntensity->getValue());

                        shader_filtres.setUniform1f("width", texture.getWidth());
                        shader_filtres.setUniform1f("height", texture.getHeight());

                        texture.draw(400.0f, 400.0f);
                    }
                    shader_filtres.end();
                }

                for (int i = 0; i < listShape.size(); i++)
                {
                    listShape[i]->draw3D();
                }


            }
            ofDisableDepthTest();

        }
		illumination_singleton.disable();

        if(userInterface.threeDeeShowLight->getChecked())
        {
            illumination_singleton.drawLights();
        }

	}
	else
	{
		for (int i = 0; i < listShape.size(); i++)
		{
			listShape[i]->draw();
		}

        if (checkIfCursorOutsideUI()) {
            drawPoint(ofGetMouseX(), ofGetMouseY());
            drawLine();
            drawCircle(ofGetMouseX(), ofGetMouseY(), 50, true, userInterface.epaisseurTrait->getValue());
            drawTriangle(ofGetMouseX(), ofGetMouseY(), 25, true, userInterface.epaisseurTrait->getValue());
            drawSquare(ofGetMouseX(), ofGetMouseY(), 25, true, userInterface.epaisseurTrait->getValue());
            drawTree(ofGetMouseX(), ofGetMouseY(), 25, true);
            drawHouse(ofGetMouseX(), ofGetMouseY(), 50);
        }

		if (userInterface.getImageLoaded()) {
			userInterface.getImage().draw(0, 0);
			fillCountRGB(userInterface.getImage());
			setupHistogram();
			plot.beginDraw();
			plot.drawBackground();
			plot.drawBox();
			plot.drawYAxis();
			plot.drawTitle();
			plot.drawHistograms();
			plot.endDraw();
		}
	}

	if (userInterface.getchooseCatmull()) {
		for (int i = 0; i < ctrlPoints.size(); i++) {
			ofFill();
			ofDrawCircle(ctrlPoints[i].x, ctrlPoints[i].y,5);
		}
	}
	else {
		if (!ctrlPoints.empty() && ctrlPoints.size()>3) {
			CatmullRom* catmul = new CatmullRom(0, 0, 1, true, currentColorSpaceFill, currentColorSpaceStroke, 2,ctrlPoints);
			listShape.push_back(catmul);
			ctrlPoints.clear();
		}
		else if (!ctrlPoints.empty()) {
			ctrlPoints.clear();
		}
	}

	userInterface.draw();
}

bool ofApp::checkIfCursorOutsideUI() {

    vec3 uiArea[4] = { userInterface.gui->getPosition(),
						  vec3(userInterface.gui->getPosition().x + userInterface.gui->getWidth(), userInterface.gui->getPosition().y, 0.0f),
						  vec3(userInterface.gui->getPosition().x + userInterface.gui->getWidth(), userInterface.gui->getPosition().y + userInterface.gui->getHeight(), 0.0f),
						  vec3(userInterface.gui->getPosition().x, userInterface.gui->getPosition().y + userInterface.gui->getHeight(), 0.0f) };

    vec3 uiPrimitiveArea[4] = { userInterface.primitiveGui->getPosition(),
						  vec3(userInterface.primitiveGui->getPosition().x + userInterface.primitiveGui->getWidth(), userInterface.primitiveGui->getPosition().y, 0.0f),
						  vec3(userInterface.primitiveGui->getPosition().x + userInterface.primitiveGui->getWidth(), userInterface.primitiveGui->getPosition().y + userInterface.primitiveGui->getHeight(), 0.0f),
						  vec3(userInterface.primitiveGui->getPosition().x, userInterface.primitiveGui->getPosition().y + userInterface.primitiveGui->getHeight(), 0.0f) };

	vec3 uiSurfaceArea[4] = { userInterface.surfaceGui->getPosition(),
					  vec3(userInterface.surfaceGui->getPosition().x + userInterface.surfaceGui->getWidth(), userInterface.surfaceGui->getPosition().y, 0.0f),
					  vec3(userInterface.surfaceGui->getPosition().x + userInterface.surfaceGui->getWidth(), userInterface.surfaceGui->getPosition().y + userInterface.surfaceGui->getHeight(), 0.0f),
					  vec3(userInterface.surfaceGui->getPosition().x, userInterface.surfaceGui->getPosition().y + userInterface.surfaceGui->getHeight(), 0.0f) };


	ofPoint mousePosition;
	mousePosition.x = ofGetMouseX();
	mousePosition.y = ofGetMouseY();

	return !(isInside(uiArea, 4, mousePosition) && userInterface.gui->getVisible() == true || isInside(uiPrimitiveArea, 4, mousePosition) && userInterface.primitiveGui->getVisible() == true || isInside(uiSurfaceArea, 4, mousePosition) && userInterface.gui->getVisible() == true);
}

//--------------------------------------------------------------
void ofApp::keyPressed(int key){
	userInterface.setChoosePoint(false);
	userInterface.setChooseLine(false);
	userInterface.setChooseCircle(false);
	userInterface.setChooseTriangle(false);
	userInterface.setChooseSquare(false);
	userInterface.setChooseTree(false);
	userInterface.setChooseHouse(false);
	userInterface.setChooseCatmull(false);

	switch (key) {
	case OF_KEY_F1:
		userInterface.setChoosePoint(true);
		break;
	case OF_KEY_F2:
		userInterface.setChooseLine(true);
		break;
	case OF_KEY_F3:
		userInterface.setChooseCircle(true);
		break;
	case OF_KEY_F4:
		userInterface.setChooseTriangle(true);
		break;
	case OF_KEY_F5:
		userInterface.setChooseSquare(true);
		break;
	case OF_KEY_F6:
		userInterface.setChooseTree(true);
		break;
	case OF_KEY_F7:
		userInterface.setChooseHouse(true);
		break;
	case OF_KEY_F8:
		userInterface.setChooseCatmull(true);
		break;
	}
	
}

//--------------------------------------------------------------
void ofApp::keyReleased(int key) {

}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y) {

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){
	if (checkIfCursorOutsideUI()) {
		dragLine(x, y);
	}

}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button) {
	glm::vec3 clic;
	clic.x = x;
	clic.y = y;

	if (button == 0) //Clic Gauche de la souris
	{
		if (checkIfCursorOutsideUI()) {
			clikScreen = true;
			PressLine(x, y);
			if (userInterface.getchooseCatmull()) {
				addPoint(x, y);
			}
		}

	}
	else if (button == 2) //Clic Droit de la souris
	{

		for (int i = 0; i < listShape.size(); i++) {
			if ((dynamic_cast<Surface*>(listShape[i]) == nullptr)) {

				if (dynamic_cast<Circle*>(listShape[i]) == nullptr) {
					if (listShape[i]->verticles != nullptr) {
						if (isInside(listShape[i]->verticles, sizeof(listShape[i]->verticles), clic)) {
							listShape[i]->selected = true;
							userInterface.currentSelectedShape = listShape[i];
							userInterface.updatePrimitiveGUI();
						}

					}
				}
				else {
					if (checkIntersectionCircle(listShape[i]->dimObject, listShape[i]->xObject, listShape[i]->yObject, x, y)) {
						listShape[i]->selected = true;
						userInterface.currentSelectedShape = listShape[i];
						userInterface.updatePrimitiveGUI();
					}
				}
			}
		}
	}
}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button) {

}

//--------------------------------------------------------------
void ofApp::mouseEntered(int x, int y) {

}

//--------------------------------------------------------------
void ofApp::mouseExited(int x, int y) {

}

//--------------------------------------------------------------
void ofApp::windowResized(int w, int h) {

}

//--------------------------------------------------------------
void ofApp::gotMessage(ofMessage msg) {

}


void ofApp::updateColor()
{
	RGBSystemStroke = ofColor(userInterface.redRGBstroke->getValue(), userInterface.greenRGBstroke->getValue(), userInterface.blueRGBstroke->getValue());
	RGBSystemFill = ofColor(userInterface.redRGBfill->getValue(), userInterface.greenRGBfill->getValue(), userInterface.blueRGBfill->getValue());
	HSBSystemStroke = ofColor::fromHsb(userInterface.hueHSBstroke->getValue(), userInterface.saturationHSBstroke->getValue(), userInterface.brightnessHSBstroke->getValue());
	HSBSystemFill = ofColor::fromHsb(userInterface.hueHSBfill->getValue(), userInterface.saturationHSBfill->getValue(), userInterface.brightnessHSBfill->getValue());
}

void ofApp::setupSelectionForm()
{
	userInterface.setChoosePoint(true);
	userInterface.setChooseLine(false);
	userInterface.setChooseCircle(false);
	userInterface.setChooseTriangle(false);
	userInterface.setChooseSquare(false);
	userInterface.setChooseTree(false);
	userInterface.setChooseHouse(false);
}

void ofApp::changeColorStroke()
{
	if (userInterface.RGBorHSB->getChecked()) {
		currentColorSpaceStroke = RGBSystemStroke;
	}
	else {
		currentColorSpaceStroke = HSBSystemStroke;
	}
}

void ofApp::changeColorFill()
{
	if (userInterface.RGBorHSB->getChecked()) {
		currentColorSpaceFill = RGBSystemFill;
	}
	else {
		currentColorSpaceFill = HSBSystemFill;
	}
}

void ofApp::drawPoint(int x, int y)
{
	if (userInterface.getChoosePoint()) 
	{
		ofFill();
		ofDrawCircle(x, y, 2);
		if (clikScreen) 
		{
			Dot* Point = new Dot(x, y, 1, true, currentColorSpaceFill, currentColorSpaceStroke, 2);
			listShape.push_back(Point);

			clikScreen = false;
		}

	}

}

void ofApp::drawCircle(int x, int y,int size,bool fill,int sizeStroke) {
	if (userInterface.getChooseCircle()) 
	{
		ofDrawCircle(x, y, size);
		if (clikScreen) 
		{
			Circle* Cercle = new Circle(x, y, size, true, currentColorSpaceFill, currentColorSpaceStroke, sizeStroke);
			listShape.push_back(Cercle);
			clikScreen = false;
		}
	}
	
}

void ofApp::drawHouse(int x, int y, int size) {
	if (userInterface.getChooseHouse()) 
	{
		/* Partie Rectangle */
		glm::vec3 p;
		p.x = x - size / 2;
		p.y = y - size / 2;

		ofSetColor(ofColor(255, 255, 255));
		ofDrawRectangle(p, size, size);

		/* Partie Triangle*/
		/* Test */
		ofSetColor(ofColor(255, 0, 0));
		ofDrawTriangle(p.x, p.y, p.x + size / 2, p.y - size / 2, p.x + size, p.y);

			if (clikScreen)
			{
				House* Maison = new House(x, y, size, true, currentColorSpaceFill, currentColorSpaceStroke, 2);
				listShape.push_back(Maison);
				clikScreen = false;
			}

	}

}

void ofApp::drawTriangle(int x, int y, int size, bool fill,int sizeStroke)
{
	if (userInterface.getChooseTriangle()) 
	{
		ofDrawTriangle(x - size, y + size, x + size, y + size, x, y - size * 2);

		if (clikScreen)
		{
			Triangle* Tri = new Triangle(x, y, size, true, currentColorSpaceFill, currentColorSpaceStroke, sizeStroke);

			listShape.push_back(Tri);
			clikScreen = false;
		}
	}

}

void ofApp::drawSquare(int x, int y, int size, bool fill, int sizeStroke)
{
	if (userInterface.getChooseSquare()) 
	{
		glm::vec3 p;
		p.x = x - size / 2;
		p.y = y - size / 2;
		ofDrawRectangle(p, size, size);

		if (clikScreen) 
		{
			Square* Carre = new Square(x, y, size, true, currentColorSpaceFill, currentColorSpaceStroke, sizeStroke);
			listShape.push_back(Carre);
			clikScreen = false;
		}
	}

}

void ofApp::drawTree(int x, int y, int size, bool fill)
{
	if (userInterface.getChooseTree()) 
	{
		glm::vec3 p;
		p.x = x - size / 4;
		p.y = y + 20;

		ofFill();
		ofSetColor(ofColor(139, 69, 19));
		ofDrawRectangle(p, size / 2, size);
		ofSetColor(ofColor(34, 139, 34));
		ofDrawTriangle(x - size, y + size, x + size, y + size, x, y- size * 2);


		if (clikScreen) 
		{
			Tree* Arbre = new Tree(x, y, size, true, currentColorSpaceFill, currentColorSpaceStroke, 2);
			listShape.push_back(Arbre);
			clikScreen = false;
		}

	}
}

void ofApp::drawLine()
{
	if (userInterface.getChooseLine()) {
		polyline.draw();
		Line* Ligne = new Line(0, 0, 0, true, currentColorSpaceFill, currentColorSpaceStroke, 2, polyline);
		listShape.push_back(Ligne);
	}
	
}

void ofApp::dragLine(int x,int y)
{
	if (clikScreen) 
	{
		polyline.addVertex(ofPoint(x, y));
	}
	
}

void ofApp::PressLine(int x,int y)
{
	if (userInterface.getChooseLine()) 
	{
		polyline.clear();
		polyline.addVertex(ofPoint(x, y));
	}
	
}


void ofApp::addPoint(int x, int y)
{
	ctrlPoints.push_back(ofPoint(x, y));
}

void ofApp::importImage()
{
	ofFileDialogResult result = ofSystemLoadDialog("Load file");
	if (result.bSuccess) {
		string path = result.getPath();
		cout << path;
		image.load(path);
		imageLoaded = true;
	}
}


void ofApp::setupHistogram()
{
	vector<ofxGPoint> points;

	for (int i = 0; i < 4; ++i) {
		string name;
		switch (i) {
		case 0: name = "[0,63]"; break;
		case 1: name = "[64,127]"; break;
		case 2: name = "[128,191]"; break;
		case 3: name = "[192,255]"; break;
		}
			
		points.emplace_back(i + 0.5 - 4 / 2.0, redCount[i], name);
	}
	// Setup for the third plot
	plot.setPos(0, 300);
	plot.setDim(250, 250);
	plot.setYLim(-0.02, 1);
	plot.setXLim(-5, 5);
	plot.getTitle().setText("Proportion composante rouge");
	plot.getTitle().setTextAlignment(GRAFICA_LEFT_ALIGN);
	plot.getTitle().setRelativePos(0);
	plot.getYAxis().getAxisLabel().setText("Proportion");
	plot.getYAxis().getAxisLabel().setTextAlignment(GRAFICA_RIGHT_ALIGN);
	plot.getYAxis().getAxisLabel().setRelativePos(1);
	plot.setPoints(points);
	plot.startHistograms(GRAFICA_VERTICAL_HISTOGRAM);
	plot.getHistogram().setDrawLabels(true);
	plot.getHistogram().setRotateLabels(true);
	plot.getHistogram().setBgColors({ ofColor(255, 0,0, 50), ofColor(255, 0, 0, 100), ofColor(255, 0,0, 150),
			ofColor(255, 0, 0, 200) });

	plot.beginDraw();
	plot.drawBackground();
	plot.drawBox();
	plot.drawYAxis();
	plot.drawTitle();
	plot.drawHistograms();
	plot.endDraw();

	vector<ofxGPoint> points1;

	for (int i = 0; i < 4; ++i) {
		string name;
		switch (i) {
		case 0: name = "[0,63]"; break;
		case 1: name = "[64,127]"; break;
		case 2: name = "[128,191]"; break;
		case 3: name = "[192,255]"; break;
		}

		points1.emplace_back(i + 0.5 - 4 / 2.0, greenCount[i], name);
	}
	// Setup for the third plot
	plot.setPos(300, 300);
	plot.setDim(250, 250);
	plot.setYLim(-0.02, 1);
	plot.setXLim(-5, 5);
	plot.getTitle().setText("Proportion composante verte");
	plot.getTitle().setTextAlignment(GRAFICA_LEFT_ALIGN);
	plot.getTitle().setRelativePos(0);
	plot.getYAxis().getAxisLabel().setText("Proportion");
	plot.getYAxis().getAxisLabel().setTextAlignment(GRAFICA_RIGHT_ALIGN);
	plot.getYAxis().getAxisLabel().setRelativePos(1);
	plot.setPoints(points1);
	plot.startHistograms(GRAFICA_VERTICAL_HISTOGRAM);
	plot.getHistogram().setDrawLabels(true);
	plot.getHistogram().setRotateLabels(true);
	plot.getHistogram().setBgColors({ ofColor(0, 255,0, 50), ofColor(0, 255, 0, 100), ofColor(0, 255,0, 150),
			ofColor(0, 255, 0, 200) });

	plot.beginDraw();
	plot.drawBackground();
	plot.drawBox();
	plot.drawYAxis();
	plot.drawTitle();
	plot.drawHistograms();
	plot.endDraw();

	vector<ofxGPoint> points2;

	for (int i = 0; i < 4; ++i) {
		string name;
		switch (i) {
		case 0: name = "[0,63]"; break;
		case 1: name = "[64,127]"; break;
		case 2: name = "[128,191]"; break;
		case 3: name = "[192,255]"; break;
		}

		points2.emplace_back(i + 0.5 - 4 / 2.0, blueCount[i], name);
	}
	// Setup for the third plot
	plot.setPos(600, 300);
	plot.setDim(250, 250);
	plot.setYLim(-0.02, 1);
	plot.setXLim(-5, 5);
	plot.getTitle().setText("Proportion composante verte");
	plot.getTitle().setTextAlignment(GRAFICA_LEFT_ALIGN);
	plot.getTitle().setRelativePos(0);
	plot.getYAxis().getAxisLabel().setText("Proportion");
	plot.getYAxis().getAxisLabel().setTextAlignment(GRAFICA_RIGHT_ALIGN);
	plot.getYAxis().getAxisLabel().setRelativePos(1);
	plot.setPoints(points2);
	plot.startHistograms(GRAFICA_VERTICAL_HISTOGRAM);
	plot.getHistogram().setDrawLabels(true);
	plot.getHistogram().setRotateLabels(true);
	plot.getHistogram().setBgColors({ ofColor(0, 0,255, 50), ofColor(0, 0, 255, 100), ofColor(0, 0,255, 150),
			ofColor(0, 0, 255, 200) });

	plot.beginDraw();
	plot.drawBackground();
	plot.drawBox();
	plot.drawYAxis();
	plot.drawTitle();
	plot.drawHistograms();
	plot.endDraw();


}

void ofApp::fillCountRGB(ofImage im)
{
	redCount[0] = 0;
	redCount[1] = 0;
	redCount[2] = 0;
	redCount[3] = 0;

	greenCount[0] = 0;
	greenCount[1] = 0;
	greenCount[2] = 0;
	greenCount[3] = 0;

	blueCount[0] = 0;
	blueCount[1] = 0;
	blueCount[2] = 0;
	blueCount[3] = 0;



	int w = im.getWidth();
	int h = im.getHeight();
	int bpp = 3;
	int numPixels = w * h;

	ofPixels  loadedPixels = im.getPixelsRef();
	int numChannels = loadedPixels.getNumChannels();

	ofxCvColorImage cvImage;
	cvImage.allocate(w, h);
	cvImage.setFromPixels(loadedPixels);
	for (int i = 0; i < w; i++) {
		for (int j = 0; j < h; j++) {
				int red = (int)cvImage.getPixels()[(j*w + i)*bpp + 0];
				int green = (int)cvImage.getPixels()[(j*w + i)*bpp + 1];
				int blue = (int)cvImage.getPixels()[(j*w + i)*bpp + 2];

				if (red >= 192) {
					redCount[3]++;
				}
				else if (red >= 128) {
					redCount[2]++;
				}
				else if (red >= 64) {
					redCount[1]++;
				}
				else {
					redCount[0]++;
				}

				if (green >= 192) {
					greenCount[3]++;
				}
				else if (green >= 128) {
					greenCount[2]++;
				}
				else if (green >= 64) {
					greenCount[1]++;
				}
				else {
					greenCount[0]++;
				}

				if (blue >= 192) {
					blueCount[3]++;
				}
				else if (blue >= 128) {
					blueCount[2]++;
				}
				else if (blue >= 64) {
					blueCount[1]++;
				}
				else {
					blueCount[0]++;
				}
		}
	}
	redCount[0] /= w * h;
	redCount[1] /= w * h;
	redCount[2] /= w * h;
	redCount[3] /= w * h;
	
	greenCount[0] /= w * h;
	greenCount[1] /= w * h;
	greenCount[2] /= w * h;
	greenCount[3] /= w * h;

	blueCount[0] /= w * h;
	blueCount[1] /= w * h;
	blueCount[2] /= w * h;
	blueCount[3] /= w * h;



}

ofImage ofApp::generateProceduralTexture(int width, int height)
{
	ofImage texture;
	int x = 0;
	int y = 0;
	int index;
	ofPixels pixels;
	pixels.allocate(width, height, OF_PIXELS_RGB);

	for (y = 0; y < height; ++y) {
		for (x = 0; x < width; ++x)
		{
			pixels.setColor(x, y, ofColor((rand() % 256), (rand() % 256), (rand() % 256)));
		}
	}

	texture.allocate(width, height, OF_IMAGE_COLOR);
	texture.setFromPixels(pixels);

	return texture;
}

//--------------------------------------------------------------
void ofApp::dragEvent(ofDragInfo dragInfo) {

}

bool ofApp::checkIntersectionCircle(float radius, float posCircleX, float posCircleY, float posMouseX, float posMouseY) {
	return sqrt( pow((posCircleX - posMouseX), 2.0f) + pow((posCircleY - posMouseY), 2) ) < radius ? true: false;
}


///Vérifier si cet algorithme fonctionne : https://www.geeksforgeeks.org/how-to-check-if-a-given-point-lies-inside-a-polygon/
// Given three colinear points p, q, r, the function checks if 
// point q lies on line segment 'pr' 
bool ofApp::onSegment(vec3 p, vec3 q, vec3 r)
{
	if (q.x <= std::max(p.x, r.x) && q.x >= std::min(p.x, r.x) &&
		q.y <= std::max(p.y, r.y) && q.y >= std::min(p.y, r.y))
		return true;
	return false;
}

// To find orientation of ordered triplet (p, q, r). 
// The function returns following values 
// 0 --> p, q and r are colinear 
// 1 --> Clockwise 
// 2 --> Counterclockwise 
int ofApp::orientation(vec3 p, vec3 q, vec3 r)
{
	int val = (q.y - p.y) * (r.x - q.x) -
		(q.x - p.x) * (r.y - q.y);

	if (val == 0) return 0;  // colinear 
	return (val > 0) ? 1 : 2; // clock or counterclock wise 
}

// The function that returns true if line segment 'p1q1' 
// and 'p2q2' intersect. 
bool ofApp::doIntersect(vec3 p1, vec3 q1, vec3 p2, vec3 q2)
{
	// Find the four orientations needed for general and 
	// special cases 
	int o1 = orientation(p1, q1, p2);
	int o2 = orientation(p1, q1, q2);
	int o3 = orientation(p2, q2, p1);
	int o4 = orientation(p2, q2, q1);

	// General case 
	if (o1 != o2 && o3 != o4)
		return true;

	// Special Cases 
	// p1, q1 and p2 are colinear and p2 lies on segment p1q1 
	if (o1 == 0 && onSegment(p1, p2, q1)) return true;

	// p1, q1 and p2 are colinear and q2 lies on segment p1q1 
	if (o2 == 0 && onSegment(p1, q2, q1)) return true;

	// p2, q2 and p1 are colinear and p1 lies on segment p2q2 
	if (o3 == 0 && onSegment(p2, p1, q2)) return true;

	// p2, q2 and q1 are colinear and q1 lies on segment p2q2 
	if (o4 == 0 && onSegment(p2, q1, q2)) return true;

	return false; // Doesn't fall in any of the above cases 
}

// Returns true if the point p lies inside the polygon[] with n vertices 
bool ofApp::isInside(vec3 polygon[], int n, vec3 p)
{
	// There must be at least 3 vertices in polygon[] 
	if (n < 3)  return false;

	// Create a point for line segment from p to infinite 
	vec3 extreme;
	extreme.x = 10000;  //Sensé être infini
	extreme.y = p.y;

	// Count intersections of the above line with sides of polygon 
	int count = 0, i = 0;
	do
	{
		int next = (i + 1) % n;

		// Check if the line segment from 'p' to 'extreme' intersects 
		// with the line segment from 'polygon[i]' to 'polygon[next]' 
		if (doIntersect(polygon[i], polygon[next], p, extreme))
		{
			// If the point 'p' is colinear with line segment 'i-next', 
			// then check if it lies on segment. If it lies, return true, 
			// otherwise false 
			if (orientation(polygon[i], p, polygon[next]) == 0)
				return onSegment(polygon[i], p, polygon[next]);

			count++;
		}
		i = next;
	} while (i != 0);

	// Return true if count is odd, false otherwise 
	return count & 1;  // Same as (count%2 == 1) 
}
