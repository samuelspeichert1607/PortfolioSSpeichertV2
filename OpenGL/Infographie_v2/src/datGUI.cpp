#include "datGUI.h"
#include "ofApp.h"
#include "ofMain.h"


datGUI::datGUI()
{
    gui = new ofxDatGui(ofxDatGuiAnchor::TOP_LEFT);
    primitiveGui = new ofxDatGui(ofxDatGuiAnchor::TOP_RIGHT);
    primitiveGui->setVisible(false);

    threeDeeGui = new ofxDatGui(ofxDatGuiAnchor::TOP_LEFT);
    threeDeeGui->setVisible(false);


    surfaceGui = new ofxDatGui(ofxDatGuiAnchor::BOTTOM_RIGHT);
}

datGUI::~datGUI()
{
    delete primitiveGui;
    delete threeDeeGui;
    delete surfaceGui;
    delete gui;
}

void datGUI::setup()
{
    ofxDatGuiLog::quiet();

    //UI principal
    header = gui->addHeader(":: Interface Principale ::");

    RGBLabel = gui->addLabel("Espace de couleur : RGB");

    contourLabel = gui->addLabel("Couleur : Contour");

    redRGBstroke = gui->addSlider("Rouge", 0, 255, 100);
    redRGBstroke->setBackgroundColor(ofColor::red);
    redRGBstroke->setStripeColor(ofColor::darkRed);

    greenRGBstroke = gui->addSlider("Vert", 0, 255, 100);
    greenRGBstroke->setBackgroundColor(ofColor::green);
    greenRGBstroke->setStripeColor(ofColor::darkGreen);

    blueRGBstroke = gui->addSlider("Bleu", 0, 255, 100);
    blueRGBstroke->setBackgroundColor(ofColor::blue);
    blueRGBstroke->setStripeColor(ofColor::darkBlue);

    fillLabel = gui->addLabel("Couleur : Remplissage");

    redRGBfill = gui->addSlider("Rouge", 0, 255, 100);
    redRGBfill->setBackgroundColor(ofColor::red);
    redRGBfill->setStripeColor(ofColor::darkRed);

    greenRGBfill = gui->addSlider("Vert", 0, 255, 100);
    greenRGBfill->setBackgroundColor(ofColor::green);
    greenRGBfill->setStripeColor(ofColor::darkGreen);

    blueRGBfill = gui->addSlider("Bleu", 0, 255, 100);
    blueRGBfill->setBackgroundColor(ofColor::blue);
    blueRGBfill->setStripeColor(ofColor::darkBlue);

    HSBLabel = gui->addLabel("Espace de couleur : HSB");

    contourLabelHSB = gui->addLabel("Couleur : Contour");

    hueHSBstroke = gui->addSlider("Hue", 0, 255, 100);
    saturationHSBstroke = gui->addSlider("Saturation", 0, 255, 100);
    brightnessHSBstroke = gui->addSlider("Brightness", 0, 255, 100);


    fillLabelHSB = gui->addLabel("Couleur : Remplissage");

    hueHSBfill = gui->addSlider("Hue", 0, 255, 100);
    saturationHSBfill = gui->addSlider("Saturation", 0, 255, 100);
    brightnessHSBfill = gui->addSlider("Brightness", 0, 255, 100);
    RGBorHSB = gui->addToggle("RGB or HSB", true);

    listeDeroulanteFormes = gui->addDropdown("Formes", shapesOptions);

    epaisseurTrait = gui->addSlider("Epaisseur ligne",2,10,2);

    backgroundColorPicker = gui->addColorPicker("Background", ofColor::black);
    boutonImporter = gui->addButton("Importer une image");
    boutonExporter = gui->addButton("Exporter une image");



    boutonGenerate3D = gui->addButton("Generer le rendu 3D");

    gui->onButtonEvent(this, &datGUI::onButtonEvent);

    imageLoaded = false;

    listeDeroulanteFormes->onDropdownEvent(this, &datGUI::onDropdownEvent);


    /// Primitives GUI
    primitiveHeader = primitiveGui->addHeader(":: Figure courrante ::");

    primitiveR = primitiveGui->addSlider("R", 0, 2000, 0);

    primitiveAngle = primitiveGui->addSlider("Angle", 0, 90, 0);

    primitiveProportion = primitiveGui->addSlider("Proportion", 0, 200, 100);

    primitiveRotation = primitiveGui->addSlider("Rotation", 0, 360, 0);

    primitiveContourLaBel = primitiveGui->addLabel("Couleur : Contour");

    primitiveRedRGBstroke = primitiveGui->addSlider("Rouge", 0, 255, 100);
    primitiveRedRGBstroke->setBackgroundColor(ofColor::red);
    primitiveRedRGBstroke->setStripeColor(ofColor::darkRed);

    // RGB
    primitiveRedRGBstroke->onSliderEvent(this, &datGUI::onSliderEvent);

    primitiveGreenRGBstroke = primitiveGui->addSlider("Vert", 0, 255, 100);
    primitiveGreenRGBstroke->setBackgroundColor(ofColor::green);
    primitiveGreenRGBstroke->setStripeColor(ofColor::darkGreen);

    primitiveBlueRGBstroke = primitiveGui->addSlider("Bleu", 0, 255, 100);
    primitiveBlueRGBstroke->setBackgroundColor(ofColor::blue);
    primitiveBlueRGBstroke->setStripeColor(ofColor::darkBlue);

    primitiveFillLabel = primitiveGui->addLabel("Couleur : Remplissage");

    primitiveRedRGBfill = primitiveGui->addSlider("Rouge", 0, 255, 100);
    primitiveRedRGBfill->setBackgroundColor(ofColor::red);
    primitiveRedRGBfill->setStripeColor(ofColor::darkRed);

    primitiveGreenRGBfill = primitiveGui->addSlider("Vert", 0, 255, 100);
    primitiveGreenRGBfill->setBackgroundColor(ofColor::green);
    primitiveGreenRGBfill->setStripeColor(ofColor::darkGreen);

    primitiveBlueRGBfill = primitiveGui->addSlider("Bleu", 0, 255, 100);
    primitiveBlueRGBfill->setBackgroundColor(ofColor::blue);
    primitiveBlueRGBfill->setStripeColor(ofColor::darkBlue);

    // HSB
    primitiveHSBLabel = primitiveGui->addLabel("Espace de couleur : HSB");

    primitiveContourLabelHSB = primitiveGui->addLabel("Couleur : Contour");

    primitiveHueHSBstroke = primitiveGui->addSlider("Hue", 0, 255, 100);
    primitiveSaturationHSBstroke = primitiveGui->addSlider("Saturation", 0, 255, 100);
    primitiveBrightnessHSBstroke = primitiveGui->addSlider("Brightness", 0, 255, 100);

    primitiveFillLabelHSB = primitiveGui->addLabel("Couleur : Remplissage");

    primitiveHueHSBfill = primitiveGui->addSlider("Hue", 0, 255, 100);
    primitiveSaturationHSBfill = primitiveGui->addSlider("Saturation", 0, 255, 100);
    primitiveBrightnessHSBfill = primitiveGui->addSlider("Brightness", 0, 255, 100);
    primitiveRGBorHSB = primitiveGui->addToggle("RGB or HSB", true);

    primitiveBoutonSupprimer = primitiveGui->addButton("Supprimer");
    primitiveGui->onButtonEvent(this, &datGUI::onButtonEvent);

    //Binding
    primitiveR->bind(r);
    primitiveAngle->bind(angle);
    primitiveProportion->bind(proportion);
    primitiveRotation->bind(rotation);

    primitiveRedRGBfill->bind(redRGBFillValue);
    primitiveGreenRGBfill->bind(greenRGBFillValue);
    primitiveBlueRGBfill->bind(blueRGBFillValue);

    primitiveRedRGBstroke->bind(redRGBStrokeValue);
    primitiveGreenRGBstroke->bind(greenRGBStrokeValue);
    primitiveBlueRGBstroke->bind(blueRGBStrokeValue);


    primitiveHueHSBfill->bind(hueHSBFillValue);
    primitiveBrightnessHSBfill->bind(brightnessHSBFillValue);
    primitiveSaturationHSBfill->bind(saturationHSBFillValue);

    primitiveHueHSBstroke->bind(hueHSBStrokeValue);
    primitiveBrightnessHSBstroke->bind(brightnessHSBStrokeValue);
    primitiveSaturationHSBstroke->bind(saturationHSBStrokeValue);

    primitiveShader = primitiveGui->addDropdown("Shader Illumination",illuminationOptions);
    primitiveShader->onDropdownEvent(this, &datGUI::onIlluminationShaderDropDownEvent);

    primitiveMateriau = primitiveGui->addDropdown("Materiau", materiauOptions);
    primitiveMateriau->onDropdownEvent(this, &datGUI::onMateriauDropDownEvent);


	threeDeeChoixTextures = threeDeeGui->addDropdown("Choix de texture", textureOptions);
	threeDeeChoixTextures->onDropdownEvent(this, &datGUI::onDropdownEvent);
	
    currentSelectedShape = nullptr;
    is3DMode = false;


    // 3D GUI
    threeDeeHeader = threeDeeGui->addHeader(":: Interface 3D ::");
    threeDeeLabelToneMapping = threeDeeGui->addLabel("Mappage Tonal");
    threeDeeExposure = threeDeeGui->addSlider("Exposure", 0, 5, 1);
    threeDeeGamma = threeDeeGui->addSlider("Gamma", 0, 5, 2.2);
    threeDeeToggle = threeDeeGui->addToggle("Aces Filmic ou Reinhald",false);

    threeDeeFiltres = threeDeeGui->addDropdown("Filtres", filtresOptions);
    threeDeeFiltres->onDropdownEvent(this, &datGUI::onDropdownEvent);

    threeDeeTeinteCP = threeDeeGui->addColorPicker("Couleur Teinte", ofColor::black);
    threeDeeMixFactor = threeDeeGui->addSlider("Mix Factor", 0, 1, 0.0f);
    threeDeeConvolutionIntensity = threeDeeGui->addSlider("Intensité de convolution", 0.1f, 16, 8.0f);

    threeDeeExposure->bind(toneMappingExposure);
    threeDeeGamma->bind(toneMappingGamma);
    backto2DButton = threeDeeGui->addButton("Retour");

    threeDeeGui->onButtonEvent(this, &datGUI::onButtonEvent);

    threeDeeActivateTM = threeDeeGui->addToggle("Activate tonal mapping",false);

    threeDeeShowLight = threeDeeGui->addToggle("Show lights", true);

    threeDeeLight1Type = threeDeeGui->addDropdown("Type Lumière 1",lumiereOptions);
    threeDeeLight1Type->onDropdownEvent(this, &datGUI::onLight1TypeDropDownEvent);

    threeDeeLight2Type = threeDeeGui->addDropdown("Type Lumière 2",lumiereOptions);
    threeDeeLight2Type->onDropdownEvent(this, &datGUI::onLight2TypeDropDownEvent);

    threeDeeLight3Type = threeDeeGui->addDropdown("Type Lumière 3",lumiereOptions);
    threeDeeLight3Type->onDropdownEvent(this, &datGUI::onLight3TypeDropDownEvent);

    threeDeeLight4Type = threeDeeGui->addDropdown("Type Lumière 4",lumiereOptions);
    threeDeeLight4Type->onDropdownEvent(this, &datGUI::onLight4TypeDropDownEvent);

    //SurfaceGui

    a1 = -1.97;
    a2 = 3.85;
    a3 = 0.49;
    b1 = -3.44;
    b2 = -3.11;
    b3 = 4;
    c1 = -3.11;
    c2 = 1.15;
    c3 = -1.89;
    d1 = 4.26;
    d2 = 4;
    d3 = 4;
    e1 = -0.98;
    e2 = 4;
    e3 = -3.03;
    f1 = 4;
    f2 = -2.62;
    f3 = 4;


    surfaceHeader = surfaceGui->addHeader(":: Surface ::");
    surfaceLabl = surfaceGui->addLabel("S(u,v)=ai*u²+bi*u*v+ci*v²+di*u+ei*v+fi");
    a1Slider = surfaceGui->addSlider("a1",-5,5,4);
    b1Slider = surfaceGui->addSlider("b1", -5, 5, 4);
    c1Slider = surfaceGui->addSlider("c1", -5, 5, 4);
    d1Slider = surfaceGui->addSlider("d1", -5, 5, 4);
    e1Slider = surfaceGui->addSlider("e1", -5, 5, 4);
    f1Slider = surfaceGui->addSlider("f1", -5, 5, 4);
    a2Slider = surfaceGui->addSlider("a2", -5, 5, 4);
    b2Slider = surfaceGui->addSlider("b2", -5, 5, 4);
    c2Slider = surfaceGui->addSlider("c2", -5, 5, 4);
    d2Slider = surfaceGui->addSlider("d2", -5, 5, 4);
    e2Slider = surfaceGui->addSlider("e2", -5, 5, 4);
    f2Slider = surfaceGui->addSlider("f2", -5, 5, 4);
    a3Slider = surfaceGui->addSlider("a3", -5, 5, 4);
    b3Slider = surfaceGui->addSlider("b3", -5, 5, 4);
    c3Slider = surfaceGui->addSlider("c3", -5, 5, 4);
    d3Slider = surfaceGui->addSlider("d3", -5, 5, 4);
    e3Slider = surfaceGui->addSlider("e3", -5, 5, 4);
    f3Slider = surfaceGui->addSlider("f3", -5, 5, 4);

    generateSurface = surfaceGui->addButton("Générer la surface");
    deleteSurface = surfaceGui->addButton("Supprimer la surface");

    generateSurface->onButtonEvent(this, &datGUI::onButtonEvent);
    deleteSurface->onButtonEvent(this, &datGUI::onButtonEvent);

    a1Slider->bind(a1);
    b1Slider->bind(b1);
    c1Slider->bind(c1);
    d1Slider->bind(d1);
    e1Slider->bind(e1);
    f1Slider->bind(f1);
    a2Slider->bind(a2);
    b2Slider->bind(b2);
    c2Slider->bind(c2);
    d2Slider->bind(d2);
    e2Slider->bind(e2);
    f2Slider->bind(f2);
    a3Slider->bind(a3);
    b3Slider->bind(b3);
    c3Slider->bind(c3);
    d3Slider->bind(d3);
    e3Slider->bind(e3);
    f3Slider->bind(f3);
}

void datGUI::update()
{
    if (currentSelectedShape != nullptr) {
        angle = primitiveAngle->getValue();
        currentSelectedShape->xObject = primitiveR->getValue() * cos(angle * PI / 180);
        currentSelectedShape->yObject = primitiveR->getValue() * sin(angle * PI / 180);

        currentSelectedShape->dimObject = primitiveProportion->getValue();
        currentSelectedShape->angleRotation = primitiveRotation->getValue();

        if(primitiveRGBorHSB->getChecked())
        {
            currentSelectedShape->shapeColorObject = ofColor(primitiveRedRGBfill->getValue(), primitiveGreenRGBfill->getValue(), primitiveBlueRGBfill->getValue());
            currentSelectedShape->borderColorObject = ofColor(primitiveRedRGBstroke->getValue(), primitiveGreenRGBstroke->getValue(), primitiveBlueRGBstroke->getValue());
        }
        else
        {
            currentSelectedShape->shapeColorObject = ofColor(primitiveHueHSBfill->getValue(), primitiveBrightnessHSBfill->getValue(), primitiveSaturationHSBfill->getValue());
            currentSelectedShape->borderColorObject = ofColor(primitiveHueHSBstroke->getValue(), primitiveBrightnessHSBstroke->getValue(), primitiveSaturationHSBstroke->getValue());
        }

        rotateShape();
    }


}

void datGUI::rotateShape() {
    if (currentSelectedShape->verticles != nullptr) {
        for (int i = 0; i < sizeof(currentSelectedShape->verticles); i++) {

            currentSelectedShape->verticles[i].x = (cos(currentSelectedShape->angleRotation * PI / 180) * currentSelectedShape->verticles[i].x +
                                                   -sin(currentSelectedShape->angleRotation * PI / 180) * currentSelectedShape->verticles[i].y);

            currentSelectedShape->verticles[i].y = (sin(currentSelectedShape->angleRotation * PI / 180) * currentSelectedShape->verticles[i].x +
                                                    cos(currentSelectedShape->angleRotation * PI / 180) * currentSelectedShape->verticles[i].y);
        }
    }
}

/*
    Sert � r�cup�rer � partir de la primitive actuellement s�lectionn�e
    les valeurs de celle-ci.
*/
void datGUI::updatePrimitiveGUI() {
    if (currentSelectedShape != nullptr) {
        primitiveGui->setVisible(true);

        r = currentSelectedShape->r;

        angle = currentSelectedShape->angleOrigine;
        proportion = currentSelectedShape->dimObject;
        rotation = currentSelectedShape->angleRotation;

        redRGBFillValue = currentSelectedShape->shapeColorObject.r;
        greenRGBFillValue = currentSelectedShape->shapeColorObject.g;
        blueRGBFillValue = currentSelectedShape->shapeColorObject.b;

        redRGBStrokeValue = currentSelectedShape->borderColorObject.r;
        greenRGBStrokeValue = currentSelectedShape->borderColorObject.g;
        blueRGBStrokeValue = currentSelectedShape->borderColorObject.b;


        hueHSBFillValue = currentSelectedShape->shapeColorObject.getHue();
        brightnessHSBFillValue = currentSelectedShape->shapeColorObject.getBrightness();
        saturationHSBFillValue = currentSelectedShape->shapeColorObject.getSaturation();

        hueHSBStrokeValue = currentSelectedShape->borderColorObject.getHue();
        brightnessHSBStrokeValue = currentSelectedShape->borderColorObject.getBrightness();
        saturationHSBStrokeValue = currentSelectedShape->borderColorObject.getSaturation();
    }
}

void datGUI::draw()
{

}

void datGUI::onButtonEvent(ofxDatGuiButtonEvent e)
{
    if (e.target == boutonImporter) {
        importImage();
    }
    else if (e.target == boutonExporter) {
        exportImage();
    }
    else if (e.target == primitiveBoutonSupprimer) {
        currentSelectedShape->hasBeenDeleted = true;
    }
    else if (e.target == boutonGenerate3D) {

        gui->setVisible(false);
        primitiveGui->setVisible(false);
        surfaceGui->setVisible(false);
        generate3D();
        threeDeeGui->setVisible(true);

    }
    else if (e.target == generateSurface) {
        surface->setA1(a1);
        surface->setA2(a2);
        surface->setA3(a3);
        surface->setB1(b1);
        surface->setB2(b2);
        surface->setB3(b3);
        surface->setC1(c1);
        surface->setC2(c2);
        surface->setC3(c3);
        surface->setD1(d1);
        surface->setD2(d2);
        surface->setD3(d3);
        surface->setE1(e1);
        surface->setE2(e2);
        surface->setE3(e3);
        surface->setF1(f1);
        surface->setF2(f2);
        surface->setF3(f3);
        surface->generatePoints();
    }
    else if (e.target == backto2DButton) {
        gui->setVisible(true);
        primitiveGui->setVisible(true);
        surfaceGui->setVisible(true);
        is3DMode = false;
        threeDeeGui->setVisible(false);
    }
    else if (e.target == deleteSurface) {
        surface->clearSurface();
    }
}

void datGUI::onDropdownEvent(ofxDatGuiDropdownEvent e)
{
    vector<ofxDatGuiComponent*> onglets = e.target->children;

    if (e.target == listeDeroulanteFormes) {

        choosePoint = false;
        chooseLine = false;
        chooseCircle = false;
        chooseTriangle = false;
        chooseSquare = false;
        chooseHouse = false;
        chooseTree = false;
        chooseCatmull = false;

        if (onglets.at(e.child)->getName() == dot_name) {
            choosePoint = true;
        }
        else if (onglets.at(e.child)->getName() == brush_name) {
            chooseLine = true;
        }
        else if (onglets.at(e.child)->getName() == circle_name) {
            chooseCircle = true;
        }
        else if (onglets.at(e.child)->getName() == square_name) {
            chooseSquare = true;
        }
        else if (onglets.at(e.child)->getName() == triangle_name) {
            chooseTriangle = true;
        }
        else if (onglets.at(e.child)->getName() == tree_name) {
            chooseTree = true;
        }
        else if (onglets.at(e.child)->getName() == house_name) {
            chooseHouse = true;
        }
        else if (onglets.at(e.child)->getName() == catmull_name) {
            chooseCatmull = true;
        }

	}
	else if (e.target == threeDeeFiltres) {
		if (onglets.at(e.child)->getName() == tint_name) {
			chosenFilter = 1.0f;
		}
		else if (onglets.at(e.child)->getName() == edgedetect_name) {
			chosenFilter = 2.0f;
		}
		else if (onglets.at(e.child)->getName() == edgeenhance_name) {
			chosenFilter = 3.0f;
		}
		else if (onglets.at(e.child)->getName() == blur_name) {
			chosenFilter = 4.0f;
		}
		else if (onglets.at(e.child)->getName() == sharpen_name) {
			chosenFilter = 5.0f;
		}
	}
	else if (e.target == threeDeeChoixTextures) {
		if (onglets.at(e.child)->getName() == importer_texture) {
			chooseImportTexture = true;
		}
		if (onglets.at(e.child)->getName() == procedural_texture) {
			chooseProceduralTexture = true;
		}
	}
	
}

void datGUI::onLight1TypeDropDownEvent(ofxDatGuiDropdownEvent e)
{
    vector<ofxDatGuiComponent*> onglets = e.target->children;

    ofLog() << "Light 1 event";

    if (onglets.at(e.child)->getName() == lumiere_aucune) {
        Illumination::getInstance()->light_enable[0] = false;
    }else{
        Illumination::getInstance()->light_enable[0] = true;
    }

    if (onglets.at(e.child)->getName() == lumiere_ambiante) {
        Illumination::getInstance()->light[0].setAmbientColor(ofFloatColor::azure);
    }
    else if (onglets.at(e.child)->getName() == lumiere_directionnelle) {
        Illumination::getInstance()->light[0].setDirectional();
    }
    else if (onglets.at(e.child)->getName() == lumiere_ponctuelle) {
        Illumination::getInstance()->light[0].setPointLight();
    }
    else if (onglets.at(e.child)->getName() == lumiere_projecteur) {
        Illumination::getInstance()->light[0].setSpotlight();
    }
}

void datGUI::onLight2TypeDropDownEvent(ofxDatGuiDropdownEvent e)
{
    vector<ofxDatGuiComponent*> onglets = e.target->children;

    if (onglets.at(e.child)->getName() == lumiere_aucune) {
        Illumination::getInstance()->light_enable[1] = false;
    }else{
        Illumination::getInstance()->light_enable[1] = true;
    }

    if (onglets.at(e.child)->getName() == lumiere_ambiante) {
        Illumination::getInstance()->light[1].setAmbientColor(ofFloatColor::azure);
    }
    else if (onglets.at(e.child)->getName() == lumiere_directionnelle) {
        Illumination::getInstance()->light[1].setDirectional();
    }
    else if (onglets.at(e.child)->getName() == lumiere_ponctuelle) {
        Illumination::getInstance()->light[1].setPointLight();
    }
    else if (onglets.at(e.child)->getName() == lumiere_projecteur) {
        Illumination::getInstance()->light[1].setSpotlight();
    }
}

void datGUI::onLight3TypeDropDownEvent(ofxDatGuiDropdownEvent e)
{
    vector<ofxDatGuiComponent*> onglets = e.target->children;

    if (onglets.at(e.child)->getName() == lumiere_aucune) {
        Illumination::getInstance()->light_enable[2] = false;
    }else{
        Illumination::getInstance()->light_enable[2] = true;
    }

    if (onglets.at(e.child)->getName() == lumiere_ambiante) {
        Illumination::getInstance()->light[2].setAmbientColor(ofFloatColor::azure);
    }
    else if (onglets.at(e.child)->getName() == lumiere_directionnelle) {
        Illumination::getInstance()->light[2].setDirectional();
    }
    else if (onglets.at(e.child)->getName() == lumiere_ponctuelle) {
        Illumination::getInstance()->light[2].setPointLight();
    }
    else if (onglets.at(e.child)->getName() == lumiere_projecteur) {
        Illumination::getInstance()->light[2].setSpotlight();
    }
}

void datGUI::onLight4TypeDropDownEvent(ofxDatGuiDropdownEvent e)
{
    vector<ofxDatGuiComponent*> onglets = e.target->children;

    if (onglets.at(e.child)->getName() == lumiere_aucune) {
        Illumination::getInstance()->light_enable[3] = false;
    }else{
        Illumination::getInstance()->light_enable[3] = true;
    }

    if (onglets.at(e.child)->getName() == lumiere_ambiante) {
        Illumination::getInstance()->light[3].setAmbientColor(ofFloatColor::azure);
    }
    else if (onglets.at(e.child)->getName() == lumiere_directionnelle) {
        Illumination::getInstance()->light[3].setDirectional();
    }
    else if (onglets.at(e.child)->getName() == lumiere_ponctuelle) {
        Illumination::getInstance()->light[3].setPointLight();
    }
    else if (onglets.at(e.child)->getName() == lumiere_projecteur) {
        Illumination::getInstance()->light[3].setSpotlight();
    }
}

void datGUI::onMateriauDropDownEvent(ofxDatGuiDropdownEvent e)
{
    vector<ofxDatGuiComponent*> onglets = e.target->children;

    if (onglets.at(e.child)->getName() == materiaux_materiau1) {
        currentSelectedShape->m_material = Illumination::MaterialType::materiau1;
    }
    else if (onglets.at(e.child)->getName() == materiaux_materiau2) {
        currentSelectedShape->m_material = Illumination::MaterialType::materiau2;
    }
    else if (onglets.at(e.child)->getName() == materiaux_materiau3) {
        currentSelectedShape->m_material = Illumination::MaterialType::materiau3;
    }


}

void datGUI::onIlluminationShaderDropDownEvent(ofxDatGuiDropdownEvent e)
{
    vector<ofxDatGuiComponent*> onglets = e.target->children;

    if (onglets.at(e.child)->getName() == illumination_lambert) {
        currentSelectedShape->m_shader = Illumination::ShaderType::lambert;
    }
    else if (onglets.at(e.child)->getName() == illumination_Gouraud) {
        currentSelectedShape->m_shader = Illumination::ShaderType::gouraud;
    }
    else if (onglets.at(e.child)->getName() == illumination_Phong) {
        currentSelectedShape->m_shader = Illumination::ShaderType::phong;
    }
    else if (onglets.at(e.child)->getName() == illumination_BlinnPhong) {
        currentSelectedShape->m_shader = Illumination::ShaderType::blinn_phong;
    }


}

void datGUI::onSliderEvent(ofxDatGuiSliderEvent e)
{

}

void datGUI::importImage()
{
    ofFileDialogResult result = ofSystemLoadDialog("Load file");
    if (result.bSuccess) {
        string path = result.getPath();
        cout << path;
        image.load(path);
        imageLoaded = true;
    }
}

void datGUI::exportImage()
{
    automaticScreenShot.grabScreen(0, 0, ofGetWidth(), ofGetHeight());
    automaticScreenShot.save("screenshot.png");
}

void datGUI::generate3D()
{
    is3DMode = true;
}

bool datGUI::getChoosePoint() {
    return choosePoint;
}

bool datGUI::getChooseLine() {
    return chooseLine;
}

bool datGUI::getChooseCircle() {
    return chooseCircle;
}

bool datGUI::getChooseTriangle() {
    return chooseTriangle;
}

bool datGUI::getChooseSquare() {
    return chooseSquare;
}

bool datGUI::getChooseTree() {
    return chooseTree;
}

bool datGUI::getChooseHouse() {
    return chooseHouse;
}

bool datGUI::get3DMode() {
    return is3DMode;
}

bool datGUI::getchooseCatmull()
{
    return chooseCatmull;
}

float datGUI::getChosenFilter() {
	return chosenFilter;
}

bool datGUI::getChosenImportTexture() {
	return chooseImportTexture;
}

bool datGUI::getChosenProceduralTexture() {
	return chooseProceduralTexture;
}

void datGUI::setChoosePoint(bool choosePoint) {
    this->choosePoint = choosePoint;
}

void datGUI::setChooseLine(bool chooseLine) {
    this->chooseLine = chooseLine;
}

void datGUI::setChooseCircle(bool chooseCircle) {
    this->chooseCircle = chooseCircle;
}

void datGUI::setChooseTriangle(bool chooseTriangle) {
    this->chooseTriangle = chooseTriangle;
}

void datGUI::setChooseSquare(bool chooseSquare) {
    this->chooseSquare = chooseSquare;
}

void datGUI::setChooseTree(bool chooseTree) {
    this->chooseTree = chooseTree;
}

void datGUI::setChooseHouse(bool chooseHouse) {
    this->chooseHouse = chooseHouse;
}

void datGUI::setChooseCatmull(bool chooseCatmull)
{
    this->chooseCatmull = chooseCatmull;
}

void datGUI::setChosenImportTexture(bool chooseImportTexture) {
	this->chooseImportTexture = chooseImportTexture;
}

void datGUI::setChosenProceduralTexture(bool chooseProceduralTexture)
{
	this->chooseProceduralTexture = chooseProceduralTexture;
}

bool datGUI::getImageLoaded() {
    return imageLoaded;
}

ofImage datGUI::getImage()
{
    return image;
}


void datGUI::setSurface(Surface * surface)
{
    this->surface = surface;
}

bool datGUI::isCubemapMode()
{
    return cubemap;
}

