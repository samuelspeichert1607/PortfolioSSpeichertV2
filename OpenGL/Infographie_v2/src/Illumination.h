#pragma once

#include "ofShader.h"
#include "ofLight.h"
#include "ofMain.h"

class Materiau
{
public:
    Materiau(glm::vec3 ambiant, glm::vec3 diffuse, glm::vec3 specular, float brightness);
    glm::vec3 ambiant;
    glm::vec3 diffuse;
    glm::vec3 specular;
    float brightness;
};

class Illumination
{
public:

    enum class ShaderType {none, color_fill, lambert, gouraud, phong, blinn_phong};
    enum class MaterialType {materiau1, materiau2, materiau3};

    Illumination();
    static Illumination* getInstance();
    void enable();
    void disable();


    void shaderBegin(ShaderType shader, MaterialType material);
    void shaderEnd(ShaderType shader);

    void drawLights();
    ofLight light[4];
    bool light_enable[4];

private:


    ofShader shaderLambert;
    ofShader shaderGouraud;
    ofShader shaderPhong;
    ofShader shaderBlinPhong;

    Materiau materiau1;
    Materiau materiau2;
    Materiau materiau3;


};


