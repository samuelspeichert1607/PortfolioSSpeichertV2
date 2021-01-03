package ca.csf.mobile1.tp1.chemical.compound;

import org.junit.Test;

import ca.csf.mobile1.tp1.chemical.element.ChemicalElement;

import static org.junit.Assert.assertEquals;

public class ChemicalCompoundBasicTest {

    @Test
    public void weightOfChemicalCompoundIsWeightOfChemicalElement() {
        ChemicalElement chemicalElement = new ChemicalElement("Hydrogène", "H", 1, 1.5D);

        ChemicalCompoundBasic chemicalCompound = new ChemicalCompoundBasic(chemicalElement);

        assertEquals(1.5D, chemicalCompound.getWeight(), 0.000001D);
    }

}