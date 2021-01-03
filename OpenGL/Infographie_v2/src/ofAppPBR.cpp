#include "ofAppPBR.h"

//--------------------------------------------------------------
void ofAppPBR::setup(){	
	cam.setupPerspective(false, 60, 1, 5000);

	scene = bind(&ofAppPBR::renderScene, this);

    cubeMap.load("PanoParis.jpg", 1024, true, "filteredMapCache");
	cubeMap.setEnvLevel(0.3);

    pbr.setup(scene, &cam, 1024);
    pbr.setCubeMap(&cubeMap);
	pbr.setDrawEnvironment(true);

	// directionalLight
	directionalLight.setup();
	directionalLight.setLightType(LightType_Directional);
	directionalLight.setShadowType(ShadowType_Soft);
	pbr.addLight(&directionalLight);
	pbr.setUsingCameraFrustomForShadow(false);
	pbr.setDirectionalShadowBBox(0, 0, 0, 1000, 1000, 1000);

	// material
	ofDisableArbTex();
	groundBaseColor.load("pbrTextures/basecolor.png");
	groundMetallic.load("pbrTextures/metallic.png");
	groundRoughness.load("pbrTextures/roughness.png");
	groundNormal.load("pbrTextures/normal.png");
	groundMaterial.baseColorMap = &groundBaseColor.getTexture();
	groundMaterial.metallicMap = &groundMetallic.getTexture();
	groundMaterial.roughnessMap = &groundRoughness.getTexture();
	groundMaterial.normalMap = &groundNormal.getTexture();
}

//--------------------------------------------------------------
void ofAppPBR::update(){
	// update Lights
	directionalLight.setPosition(500 * cos(ofGetElapsedTimef()), 500, 500 * sin(ofGetElapsedTimef()));
	directionalLight.lookAt(glm::vec3(0), glm::vec3(0.0, 1.0, 0.0));

	// update light parameters
	directionalLight.setEnable(true);
	directionalLight.setColor(ofColor(255,255,255));
}

//--------------------------------------------------------------
void ofAppPBR::draw()
{
	pbr.updateDepthMaps();
	cam.begin();
	pbr.renderScene();

	cam.end();
}

//--------------------------------------------------------------
void ofAppPBR::renderScene(){
	ofEnableDepthTest();
	glEnable(GL_CULL_FACE);
	glCullFace(GL_FRONT);
	pbr.beginDefaultRenderer();
    {	
		material.roughness = 1;
		material.metallic = 0.1;
		material.begin(&pbr);
		ofDrawSphere(350, 0, 450, 95);
		material.end();

		material.roughness = 0.1;
		material.metallic = 1;
		material.begin(&pbr);
		ofDrawSphere(350, 0, 250, 95);
		material.end();


		
		/* TEST */
		material.roughness = 0.1;
		material.metallic = 1;
		material.begin(&pbr);
		ofDrawBox(350 - 190, -190 , 190, 190);
		material.end();


		material.roughness = 0.5;
		material.metallic = 0.5;
		material.begin(&pbr);
		ofDrawBox(350 + 190, - 190, 190, 190);
		material.end();
	}
	pbr.endDefaultRenderer();
	glDisable(GL_CULL_FACE);
	ofDisableDepthTest();
}

//--------------------------------------------------------------
void ofAppPBR::keyPressed(int key){

}

//--------------------------------------------------------------
void ofAppPBR::keyReleased(int key){

}

//--------------------------------------------------------------
void ofAppPBR::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofAppPBR::mouseDragged(int x, int y, int button){

}

//--------------------------------------------------------------
void ofAppPBR::mousePressed(int x, int y, int button){

}

//--------------------------------------------------------------
void ofAppPBR::mouseReleased(int x, int y, int button){

}

//--------------------------------------------------------------
void ofAppPBR::mouseEntered(int x, int y){

}

//--------------------------------------------------------------
void ofAppPBR::mouseExited(int x, int y){

}

//--------------------------------------------------------------
void ofAppPBR::windowResized(int w, int h){

}

//--------------------------------------------------------------
void ofAppPBR::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void ofAppPBR::dragEvent(ofDragInfo dragInfo){ 

}
