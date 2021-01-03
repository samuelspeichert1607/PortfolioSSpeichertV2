package ca.csf.mobile1.tp1.chemical.compound;

import org.junit.Test;

import java.util.ArrayList;

import ca.csf.mobile1.tp1.chemical.element.ChemicalElement;

import static org.junit.Assert.assertEquals;

public class ChemicalCompoundGroupTest {

    @Test
    public void canComputeWeightFromElementsComposingTheChemicalCompound()
    {
        ChemicalCompound chemicalCompound1 = new ChemicalCompoundBasic(new ChemicalElement("Hydrogene", "H", 1, 1.5D));
        ChemicalCompound chemicalCompound2 = new ChemicalCompoundBasic(new ChemicalElement("Helium", "He", 2, 0.5D));
        ChemicalCompound chemicalCompound3 = new ChemicalCompoundBasic(new ChemicalElement("Lithium", "Li", 3, 2.0D));
        ArrayList<ChemicalCompound> compounds = new ArrayList<ChemicalCompound>();
        compounds.add(chemicalCompound1);
        compounds.add(chemicalCompound2);
        compounds.add(chemicalCompound3);
        ChemicalCompoundGroup chemicalCompound = new ChemicalCompoundGroup(compounds);

        assertEquals(4.0D, chemicalCompound.getWeight(), 0.0D);
    }

}