package ca.csf.mobile1.tp1.chemical.element;

import ca.csf.mobile1.tp1.chemical.element.ChemicalElement;
import ca.csf.mobile1.tp1.chemical.element.ChemicalElementFactory;
import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class ChemicalElementFactoryTest  {

    private ChemicalElementFactory chemicalElementFactory;

    @Before
    public void before() {
        chemicalElementFactory = new ChemicalElementFactory();
    }

    @Test
    public void canCreateChemicalElementFromCommasSeparatedString() throws Exception {
        ChemicalElement chemicalElement = chemicalElementFactory.createFromString("Hydrogène,H,1,1.00794");

        assertEquals("Hydrogène", chemicalElement.getName());
        assertEquals("H", chemicalElement.getSymbol());
        assertEquals(1, chemicalElement.getNumber());
        assertEquals(1.00794D, chemicalElement.getWeight(), 0.000001D);
    }

    @Test(expected = IllegalArgumentException.class)
    public void cannotCreateChemicalElementFromNullString() throws Exception {
        chemicalElementFactory.createFromString(null);
    }

    @Test(expected = IllegalArgumentException.class)
    public void cannotCreateChemicalElementFromEmptyString() throws Exception {
        chemicalElementFactory.createFromString("");
    }

    @Test(expected = IllegalArgumentException.class)
    public void cannotCreateChemicalElementFromWhitespaceString() throws Exception {
        chemicalElementFactory.createFromString("           ");
    }

    @Test(expected = IllegalArgumentException.class)
    public void cannotCreateChemicalElementIfPartsAreMissingFromString() throws Exception {
        chemicalElementFactory.createFromString("Hydrogène,H,1");
    }

    @Test(expected = IllegalArgumentException.class)
    public void cannotCreateChemicalElementIfPartsAreNotOfTheRightType() throws Exception {
        chemicalElementFactory.createFromString("1.00794,Hydrogène,H,1");
    }

}