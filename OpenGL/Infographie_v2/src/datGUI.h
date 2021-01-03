#pragma once
#include "ofxDatGui.h"
#include <Shape.h>
#include <math.h>
#include "Surface.h"

using namespace std;

class datGUI {

private:
    bool choosePoint;
    bool chooseLine;
    bool chooseCircle;
    bool chooseTriangle;
    bool chooseSquare;
    bool chooseTree;
    bool chooseHouse;
    bool chooseCatmull;

	float chosenFilter = 1.0f;

	bool chooseImportTexture = false;
	bool chooseProceduralTexture = false;

	ofColor RGBSystemStroke;
	ofColor RGBSystemFill;
	ofColor HSBSystemStroke;
	ofColor HSBSystemFill;

    const string radius_name = "radius";
    const string resolution_name = "resolution";
    const string dot_name = "Dot";
    const string brush_name = "Brush";
    const string square_name = "Square";
    const string circle_name = "Circle";
    const string triangle_name = "Triangle";
    const string tree_name = "Tree";
    const string house_name = "House";
    const string catmull_name = "Courbe Catmull-Rom";

    const string color_name = "Color";
    const string import_name = "Import";
    const string export_name = "Export";

	const string tint_name = "Tint";
	const string edgedetect_name = "Edge Detect";
	const string edgeenhance_name = "Edge Enhance";
	const string blur_name = "Blur";
	const string sharpen_name = "Sharpen";

	const string importer_texture = "Import Texture";
	const string procedural_texture = "Procedural Texture";

	void rotateShape();
	void importImage();
	void exportImage();
    const string illumination_lambert = "Lambert";
    const string illumination_Gouraud = "Gouraud";
    const string illumination_Phong = "Phong";
    const string illumination_BlinnPhong = "Blinn-Phong";

    const string lumiere_aucune = "Aucune";
    const string lumiere_ambiante = "Ambiante";
    const string lumiere_directionnelle = "Directionnelle";
    const string lumiere_ponctuelle = "Ponctuelle";
    const string lumiere_projecteur = "Projecteur";

    const string materiaux_materiau1 = "materiau1";
    const string materiaux_materiau2 = "materiau2";
    const string materiaux_materiau3 = "materiau3";


    /*Import Image*/

    ofImage image;
    bool imageLoaded;

    /*Export Image*/
    ofImage automaticScreenShot;
    ofPixels ssPixels = automaticScreenShot.getPixels();
    bool guiVisible = true;

    /*Surface*/
    float a1, a2, a3, b1, b2, b3, c1, c2, c3, d1, d2, d3, e1, e2, e3, f1, f2, f3;

    //Cubemap
    bool cubemap;

public:
    void setSurface(Surface* surface);
    bool isCubemapMode();


    ofxDatGui* gui;
    ofxDatGui* primitiveGui;
    ofxDatGui* threeDeeGui;

    ofxDatGui* surfaceGui;

    vector<string> shapesOptions = { dot_name, brush_name, square_name, circle_name, triangle_name, tree_name, house_name,catmull_name };


    vector<string> illuminationOptions = {illumination_lambert, illumination_Gouraud, illumination_Phong, illumination_BlinnPhong };
    vector<string> lumiereOptions = { lumiere_aucune, lumiere_ambiante, lumiere_directionnelle, lumiere_ponctuelle, lumiere_projecteur };
    vector<string> materiauOptions = {materiaux_materiau1, materiaux_materiau2, materiaux_materiau3};


    ofxDatGuiHeader* header;

    /**ColorScheme**/
    ofxDatGuiLabel* RGBLabel;

    ofxDatGuiLabel* contourLabel;
    ofxDatGuiSlider* redRGBstroke;
    ofxDatGuiSlider* greenRGBstroke;
    ofxDatGuiSlider* blueRGBstroke;
    ofxDatGuiLabel* fillLabel;
    ofxDatGuiSlider* redRGBfill;
    ofxDatGuiSlider* greenRGBfill;
    ofxDatGuiSlider* blueRGBfill;

    ofxDatGuiLabel* HSBLabel;

    ofxDatGuiLabel* contourLabelHSB;
    ofxDatGuiSlider* hueHSBstroke;
    ofxDatGuiSlider* saturationHSBstroke;
    ofxDatGuiSlider* brightnessHSBstroke;
    ofxDatGuiLabel* fillLabelHSB;
    ofxDatGuiSlider* hueHSBfill;
    ofxDatGuiSlider* saturationHSBfill;
    ofxDatGuiSlider* brightnessHSBfill;

    ofxDatGuiToggle* RGBorHSB;

    ofxDatGuiDropdown* listeDeroulanteFormes;

    ofxDatGuiSlider* epaisseurTrait;

    ofxDatGuiColorPicker* backgroundColorPicker;

    ofxDatGuiButton* boutonImporter;
    ofxDatGuiButton* boutonExporter;
    ofxDatGuiButton* boutonGenerate3D;

    /// Primitive GUI

    ofxDatGuiHeader* primitiveHeader;

    ofxDatGuiLabel* primitiveCurrentShape;

    ofxDatGuiSlider* primitiveR;
    ofxDatGuiSlider* primitiveAngle;
    ofxDatGuiSlider* primitiveProportion;
    ofxDatGuiSlider* primitiveRotation;

    ofxDatGuiLabel*  primitiveContourLaBel;
    ofxDatGuiSlider* primitiveRedRGBstroke;
    ofxDatGuiSlider* primitiveGreenRGBstroke;
    ofxDatGuiSlider* primitiveBlueRGBstroke;

    ofxDatGuiLabel*  primitiveFillLabel;
    ofxDatGuiSlider* primitiveRedRGBfill;
    ofxDatGuiSlider* primitiveGreenRGBfill;
    ofxDatGuiSlider* primitiveBlueRGBfill;

    ofxDatGuiLabel*  primitiveHSBLabel;

	ofxDatGuiDropdown* threeDeeFiltres;
	ofxDatGuiLabel* threeDeeLabelTeinte;
	ofxDatGuiColorPicker* threeDeeTeinteCP;
	ofxDatGuiSlider* threeDeeMixFactor;
	ofxDatGuiSlider* threeDeeConvolutionIntensity;
	ofxDatGuiSlider* threeDeeNoise;
	ofxDatGuiDropdown* threeDeeChoixTextures;
    ofxDatGuiLabel*  primitiveContourLabelHSB;
    ofxDatGuiSlider* primitiveHueHSBstroke;
    ofxDatGuiSlider* primitiveSaturationHSBstroke;
    ofxDatGuiSlider* primitiveBrightnessHSBstroke;
    ofxDatGuiLabel*  primitiveFillLabelHSB;
    ofxDatGuiSlider* primitiveHueHSBfill;
    ofxDatGuiSlider* primitiveSaturationHSBfill;
    ofxDatGuiSlider* primitiveBrightnessHSBfill;

    ofxDatGuiToggle* primitiveRGBorHSB;
	vector<string> filtresOptions = { tint_name, edgedetect_name, edgeenhance_name, blur_name, sharpen_name };

    ofxDatGuiToggle* threeDeeActivateTM;
	vector<string> textureOptions = { importer_texture, procedural_texture };

    ofxDatGuiButton* primitiveBoutonSupprimer;

    ofxDatGuiDropdown* primitiveShader;

    ofxDatGuiDropdown* primitiveMateriau;

    //3D GUI
    ofxDatGuiHeader* threeDeeHeader;
    ofxDatGuiLabel*  threeDeeLabelToneMapping;

    ofxDatGuiSlider* threeDeeExposure;
    ofxDatGuiSlider* threeDeeGamma;
    ofxDatGuiToggle* threeDeeToggle;
    ofxDatGuiButton* backto2DButton;

    ofxDatGuiToggle* threeDeeShowLight;
    ofxDatGuiDropdown* threeDeeLight1Type;
    ofxDatGuiDropdown* threeDeeLight2Type;
    ofxDatGuiDropdown* threeDeeLight3Type;
    ofxDatGuiDropdown* threeDeeLight4Type;

    /**Surface Gui**/
    ofxDatGuiHeader* surfaceHeader;
    ofxDatGuiLabel*  surfaceLabl;
    ofxDatGuiSlider* a1Slider;
    ofxDatGuiSlider* b1Slider;
    ofxDatGuiSlider* c1Slider;
    ofxDatGuiSlider* d1Slider;
    ofxDatGuiSlider* e1Slider;
    ofxDatGuiSlider* f1Slider;
    ofxDatGuiSlider* a2Slider;
    ofxDatGuiSlider* b2Slider;
    ofxDatGuiSlider* c2Slider;
    ofxDatGuiSlider* d2Slider;
    ofxDatGuiSlider* e2Slider;
    ofxDatGuiSlider* f2Slider;
    ofxDatGuiSlider* a3Slider;
    ofxDatGuiSlider* b3Slider;
    ofxDatGuiSlider* c3Slider;
    ofxDatGuiSlider* d3Slider;
    ofxDatGuiSlider* e3Slider;
    ofxDatGuiSlider* f3Slider;
    ofxDatGuiButton* generateSurface;
    ofxDatGuiButton* deleteSurface;
    Surface * surface;






    float toneMappingExposure = 5.0f;
    float toneMappingGamma = 2.2f;
    bool toneMappingToggle = false;



    Shape* currentSelectedShape;
    bool currentShapeHasBeenDeleted;

    float r;
    float angle;
    float proportion;
    float rotation;

    float redRGBFillValue;
    float greenRGBFillValue;
    float blueRGBFillValue;
	bool getChosenImportTexture();
	bool getChosenProceduralTexture();

	void setChoosePoint(bool choosePoint);
	void setChooseLine(bool chooseLine);
	void setChooseCircle(bool chooseCircle);
	void setChooseTriangle(bool chooseTriangle);
	void setChooseSquare(bool chooseSquare);
	void setChooseTree(bool chooseTree);
	void setChooseHouse(bool chooseHouse);
	void setChooseCatmull(bool chooseCatmull);
	void setChosenImportTexture(bool chooseImportTexture);
	void setChosenProceduralTexture(bool chooseProceduralTexture);
	void generate3D();
	bool getImageLoaded();
	ofImage getImage();

    float redRGBStrokeValue;
    float greenRGBStrokeValue;
    float blueRGBStrokeValue;

    float hueHSBFillValue;
    float brightnessHSBFillValue;
    float saturationHSBFillValue;

    float hueHSBStrokeValue;
    float brightnessHSBStrokeValue;
    float saturationHSBStrokeValue;

    datGUI();
    ~datGUI();

    void setup();
    void update();
    void draw();

    void updatePrimitiveGUI();

    void onButtonEvent(ofxDatGuiButtonEvent e);
    void onDropdownEvent(ofxDatGuiDropdownEvent e);

    void onLight1TypeDropDownEvent(ofxDatGuiDropdownEvent e);
    void onLight2TypeDropDownEvent(ofxDatGuiDropdownEvent e);
    void onLight3TypeDropDownEvent(ofxDatGuiDropdownEvent e);
    void onLight4TypeDropDownEvent(ofxDatGuiDropdownEvent e);
    void onMateriauDropDownEvent(ofxDatGuiDropdownEvent e);
    void onIlluminationShaderDropDownEvent(ofxDatGuiDropdownEvent e);

    void onSliderEvent(ofxDatGuiSliderEvent e);

    bool is3DMode;
    bool get3DMode();
    bool getChoosePoint();
    bool getChooseLine();
    bool getChooseCircle();
    bool getChooseTriangle();
    bool getChooseSquare();
    bool getChooseTree();
    bool getChooseHouse();
    bool getchooseCatmull();

    float getChosenFilter();

};

