#include "Illumination.h"
#include "ofMain.h"
#include <strstream>

Materiau::Materiau(glm::vec3 ambiant, glm::vec3 diffuse, glm::vec3 specular, float brightness)
{
    this->ambiant = ambiant;
    this->diffuse = diffuse;
    this->specular = specular;
    this->brightness = brightness;

}

static Illumination* m_singleton = nullptr;


Illumination::Illumination():
   materiau1(glm::vec3(0.1,0.2,0.3), glm::vec3(0.1,0.2,0.3), glm::vec3(0.1,0.2,0.3), 0.2),
   materiau2(glm::vec3(0.4,0.5,0.6), glm::vec3(0.4,0.5,0.6), glm::vec3(0.4,0.5,0.6), 0.5),
   materiau3(glm::vec3(0.7,0.8,0.9), glm::vec3(0.7,0.8,0.9), glm::vec3(0.7,0.8,0.9), 0.7)
{
   if(m_singleton != nullptr) return;
   m_singleton = this;



   shaderLambert.load("shader/lambert_330_vs.glsl","shader/lambert_330_fs.glsl");
   shaderGouraud.load("shader/gouraud_330_vs.glsl","shader/gouraud_330_fs.glsl");
   shaderPhong.load("shader/phong_330_vs.glsl", "shader/phong_330_fs.glsl");
   shaderBlinPhong.load("shader/blinn_phong_330_vs.glsl","shader/blinn_phong_330_fs.glsl");

    light[0].setDiffuseColor(ofFloatColor(0.7f, 0.1f, 0.1f));
    light[0].setSpecularColor(ofColor(0.0f, 0.0f, 0.0f));
    light[0].setAmbientColor(ofColor(1.0f, 1.0f, 1.0f));
    //light[0].setPosition(0,0,-1);
    light[0].lookAt(glm::vec3(0,0,0));
    light[0].setDirectional();


    light[1].setDiffuseColor(ofFloatColor(1.0f, 0.0f, 0.0f));
    light[1].setSpecularColor(ofColor(0.0f, 0.0f, 0.0f));
    light[1].setAmbientColor(ofColor(1.0f, 1.0f, 1.0f));
    light[1].setPosition(0,0,-1);
    light[1].lookAt(glm::vec3(0,0,0));
    light[1].setDirectional();

    light[2].setDiffuseColor(ofFloatColor(0.7f, 0.1f, 0.1f));
    light[2].setSpecularColor(ofColor(0.0f, 0.0f, 0.0f));
    light[2].setAmbientColor(ofColor(1.0f, 1.0f, 1.0f));
    light[2].setPosition(0,10,-1);
    light[2].lookAt(glm::vec3(0,0,0));
    light[2].setDirectional();

    light[3].setDiffuseColor(ofFloatColor(0.7f, 0.1f, 0.1f));
    light[3].setSpecularColor(ofColor(0.0f, 0.0f, 0.0f));
    light[3].setAmbientColor(ofColor(1.0f, 1.0f, 1.0f));
    light[3].setPosition(10,10,-1);
    light[3].lookAt(glm::vec3(0,0,0));
    light[3].setDirectional();

    light_enable[0] = false;
    light_enable[1] = false;
    light_enable[2] = false;
    light_enable[3] = false;


}

Illumination* Illumination::getInstance()
{
    return m_singleton;
}

void Illumination::enable()
{
    ofEnableLighting();

    for(size_t i = 0; i != 4; i++)
    {
        if(light_enable[i] == true)
        {
            light[0].enable();
        }

    }
}

void Illumination::disable()
{
    for(size_t i = 0; i != 4; i++)
    {
        if(light_enable[i] == true)
        {
            light[0].enable();
        }
    }
    ofDisableLighting();
}



void Illumination::shaderBegin(ShaderType shader, MaterialType material)
{
    glm::vec3 ambiant;
    glm::vec3 diffuse;
    glm::vec3 specular;
    float brightness;

    int type[4];

    for(size_t i = 0; i != 4; i++)
    {
        if(light[i].getIsDirectional())
        {
            type[i] = 1;
        }
        else if(light[i].getIsSpotlight())
        {
            type[i] = 2;
        }
        else if(light[i].getIsPointLight())
        {
            type[i] = 3;
        }
        else
        {
            type[i] = 0; //ambiant light
        }

    }

    switch(material)
    {
    case MaterialType::materiau1:
        ambiant = this->materiau1.ambiant;
        diffuse = this->materiau1.diffuse;
        specular = this->materiau1.specular;
        brightness = this->materiau1.brightness;
        break;

    case MaterialType::materiau2:
        ambiant = this->materiau2.ambiant;
        diffuse = this->materiau2.diffuse;
        specular = this->materiau2.specular;
        brightness = this->materiau2.brightness;
        break;

    case MaterialType::materiau3:
        ambiant = this->materiau3.ambiant;
        diffuse = this->materiau3.diffuse;
        specular = this->materiau3.specular;
        brightness = this->materiau3.brightness;
        break;
    }

    switch (shader)
      {
        case ShaderType::none:
            break;
        case ShaderType::color_fill:
          /*
          shader->begin();
          shader->setUniform3f("color", 1.0f, 1.0f, 0.0f);
          shader->end();
          */
          break;

        case ShaderType::lambert:


          shaderLambert.begin();
          shaderLambert.setUniform3f("color_ambient", ambiant);
          shaderLambert.setUniform3f("material_color_diffuse", diffuse);

          shaderLambert.setUniform3f("light1_color_diffuse", glm::vec3(light[0].getDiffuseColor()[0],light[0].getDiffuseColor()[1],light[0].getDiffuseColor()[2]));
          shaderLambert.setUniform3f("light1_position", glm::vec4(light[0].getGlobalPosition(), 0.0f) * ofGetCurrentMatrix(OF_MATRIX_MODELVIEW));
          shaderLambert.setUniform1i("light1_enable", light_enable[0]);
          shaderLambert.setUniform1i("light1_type", type[0]);
          shaderLambert.setUniform3f("light1_attenuation", glm::vec3(light[0].getAttenuationConstant(),light[0].getAttenuationLinear(), light[0].getAttenuationQuadratic()));
          shaderLambert.setUniform3f("light1_spotDirection", light[0].getLookAtDir());
          shaderLambert.setUniform1f("light1_spotCutoff", light[0].getSpotlightCutOff());
          shaderLambert.setUniform1f("light1_spotExponent", light[0].getSpotConcentration());

          shaderLambert.setUniform3f("light2_color_diffuse", glm::vec3(light[1].getDiffuseColor()[1],light[1].getDiffuseColor()[1],light[1].getDiffuseColor()[2]));
          shaderLambert.setUniform3f("light2_position", glm::vec4(light[1].getGlobalPosition(), 0.0f) * ofGetCurrentMatrix(OF_MATRIX_MODELVIEW));
          shaderLambert.setUniform1i("light2_enable", light_enable[1]);
          shaderLambert.setUniform1i("light2_type", type[1]);
          shaderLambert.setUniform3f("light2_attenuation", glm::vec3(light[1].getAttenuationConstant(),light[1].getAttenuationLinear(), light[1].getAttenuationQuadratic()));
          shaderLambert.setUniform3f("light2_spotDirection", light[1].getLookAtDir());
          shaderLambert.setUniform1f("light2_spotCutoff", light[1].getSpotlightCutOff());
          shaderLambert.setUniform1f("light2_spotExponent", light[1].getSpotConcentration());

          shaderLambert.setUniform3f("light3_color_diffuse", glm::vec3(light[2].getDiffuseColor()[0],light[2].getDiffuseColor()[1],light[2].getDiffuseColor()[2]));
          shaderLambert.setUniform3f("light3_position", glm::vec4(light[2].getGlobalPosition(), 0.0f) * ofGetCurrentMatrix(OF_MATRIX_MODELVIEW));
          shaderLambert.setUniform1i("light3_enable", light_enable[2]);
          shaderLambert.setUniform1i("light3_type", type[2]);
          shaderLambert.setUniform3f("light3_attenuation", glm::vec3(light[2].getAttenuationConstant(),light[2].getAttenuationLinear(), light[2].getAttenuationQuadratic()));
          shaderLambert.setUniform3f("light3_spotDirection", light[2].getLookAtDir());
          shaderLambert.setUniform1f("light3_spotCutoff", light[2].getSpotlightCutOff());
          shaderLambert.setUniform1f("light3_spotExponent", light[2].getSpotConcentration());

          shaderLambert.setUniform3f("light4_color_diffuse", glm::vec3(light[3].getDiffuseColor()[0],light[3].getDiffuseColor()[1],light[3].getDiffuseColor()[2]));
          shaderLambert.setUniform3f("light4_position", glm::vec4(light[3].getGlobalPosition(), 0.0f) * ofGetCurrentMatrix(OF_MATRIX_MODELVIEW));
          shaderLambert.setUniform1i("light4_enable", light_enable[3]);
          shaderLambert.setUniform1i("light4_type", type[3]);
          shaderLambert.setUniform3f("light4_attenuation", glm::vec3(light[3].getAttenuationConstant(),light[3].getAttenuationLinear(), light[3].getAttenuationQuadratic()));
          shaderLambert.setUniform3f("light4_spotDirection", light[3].getLookAtDir());
          shaderLambert.setUniform1f("light4_spotCutoff", light[3].getSpotlightCutOff());
          shaderLambert.setUniform1f("light4_spotExponent", light[3].getSpotConcentration());



          break;

        case ShaderType::gouraud:

          shaderGouraud.begin();
          shaderGouraud.setUniform3f("color_ambient", ambiant);
          shaderGouraud.setUniform3f("color_diffuse", diffuse);
          shaderGouraud.setUniform3f("color_specular", specular);
          shaderGouraud.setUniform1f("brightness", brightness);
          shaderGouraud.setUniform3f("light_position", glm::vec4(light[0].getGlobalPosition(), 0.0f) * ofGetCurrentMatrix(OF_MATRIX_MODELVIEW));

          break;

        case ShaderType::phong:

          shaderPhong.begin();
          shaderPhong.setUniform3f("color_ambient", ambiant);
          shaderPhong.setUniform3f("color_diffuse", diffuse);
          shaderPhong.setUniform3f("color_specular", specular);
          shaderPhong.setUniform1f("brightness", brightness);
          shaderPhong.setUniform3f("light_position", glm::vec4(light[0].getGlobalPosition(), 0.0f) * ofGetCurrentMatrix(OF_MATRIX_MODELVIEW));

          break;

        case ShaderType::blinn_phong:


          shaderBlinPhong.begin();
          shaderBlinPhong.setUniform3f("color_ambient", ambiant);
          shaderBlinPhong.setUniform3f("color_diffuse", diffuse);
          shaderBlinPhong.setUniform3f("color_specular", specular);
          shaderBlinPhong.setUniform1f("brightness", brightness);
          shaderBlinPhong.setUniform3f("light_position", glm::vec4(light[0].getGlobalPosition(), 0.0f) * ofGetCurrentMatrix(OF_MATRIX_MODELVIEW));


          break;

        default:
          break;
      }
}

void Illumination::drawLights()
{
    for(size_t i = 0; i != 4; i++)
    {
        if(light_enable[i] == true)
        {
             std::stringstream name;

             name << "Light " << i << " ";


             ofTrueTypeFont font;
             font.load("data/ofxbraits/fonts/Verdana.ttf",16);

             if(light[i].getIsPointLight()){
                 name << "point";
                 font.drawString(name.str(), light[i].getPosition()[0] + ofGetWidth() / 2.0f, light[i].getPosition()[1] + ofGetHeight() / 2);
             }
             else if(light[i].getIsDirectional()){
                 name << "directional";
                 font.drawString(name.str(), light[i].getPosition()[0] + ofGetWidth() / 2.0f, light[i].getPosition()[1] + ofGetHeight() / 2);
             }
             else if(light[i].getIsSpotlight()){
                 name << "spot";
                 font.drawString(name.str(), light[i].getPosition()[0] + ofGetWidth() / 2.0f, light[i].getPosition()[1] + ofGetHeight() / 2);
             }
             else{ //is ambient color
                 name << "ambient";
                 font.drawString(name.str(), ofGetWidth() / 2.0f, ofGetHeight() / 2);
             }



             ofDrawEllipse(light[i].getPosition()[0] + ofGetWidth() / 2.0f,light[i].getPosition()[1] + ofGetHeight() / 2, 5, 5);
        }
    }
}

void Illumination::shaderEnd(ShaderType shader)
{
    switch (shader)
      {
        case ShaderType::none:
          break;

        case ShaderType::color_fill:
          /*

          shader->end();
          */
          break;

        case ShaderType::lambert:

          shaderLambert.end();
          break;

        case ShaderType::gouraud:

          shaderGouraud.end();
          break;

        case ShaderType::phong:

          shaderPhong.end();
          break;

        case ShaderType::blinn_phong:


          shaderBlinPhong.end();
          break;

        default:
          break;
      }
}



