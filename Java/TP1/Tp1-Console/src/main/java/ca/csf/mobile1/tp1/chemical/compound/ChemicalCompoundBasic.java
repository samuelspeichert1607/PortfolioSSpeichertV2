package ca.csf.mobile1.tp1.chemical.compound;
        import ca.csf.mobile1.tp1.chemical.element.ChemicalElement;
import ca.csf.mobile1.tp1.chemical.element.ChemicalElement;

/**
 * Created by Samuel Speichert on 2017-02-20.
 */

public class ChemicalCompoundBasic implements ChemicalCompound
{
    private ChemicalElement chemicalElement;

    /**
     * Constructeur de ChemicalCompoundBasic
     * @param element ChemicalElement element
     */
    public ChemicalCompoundBasic(ChemicalElement element)
    {
        chemicalElement = element;
    }

    /**
     * Fonction qui va retourner la masse du composé chimique.
     * @return Poids du composé chimique.
     */
    public double getWeight()
    {
        return chemicalElement.getWeight();
    }


}
