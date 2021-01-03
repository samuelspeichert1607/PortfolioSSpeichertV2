package ca.csf.mobile1.tp1.chemical.element;

import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.assertNull;
import static org.junit.Assert.assertSame;

public class ChemicalElementRepositoryTest {

    private ChemicalElementRepository chemicalElementRepository;

    @Before
    public void before() {
        chemicalElementRepository = new ChemicalElementRepository();
    }

    @Test
    public void canAddAndGetChemicalElementBySymbol() {
        ChemicalElement chemicalElement = new ChemicalElement("Hydroène", "H", 1, 1.5D);

        chemicalElementRepository.add(chemicalElement);

        assertSame(chemicalElement, chemicalElementRepository.get("H"));
    }

    @Test
    public void returnsNullIfChemicalElementIsNotInRepository() {
        assertNull(chemicalElementRepository.get("H"));
    }

}