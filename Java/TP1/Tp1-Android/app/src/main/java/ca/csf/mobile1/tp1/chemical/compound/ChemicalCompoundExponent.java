package ca.csf.mobile1.tp1.chemical.compound;

/**
 * Created by Samuel Speichert on 2017-02-20.
 */

public class ChemicalCompoundExponent implements ChemicalCompound
{
    private ChemicalCompound compound;
    private double exponent;

    /**
     *Constructeur de ChemicalCompoundExponent
     * @param compound Composé chimique dont l'on va multiplier avec un exponent.
     * @param exponent L'exponent en question.
     */
    public ChemicalCompoundExponent(ChemicalCompound compound, double exponent)
    {
        this.compound = compound;
        this.exponent = exponent;
    }

    /**
     * Fonction qui va retourner la masse du composé chimique.
     * @return Poids du composé chimique, multiplié par son exponent.
     */
    public double getWeight()
    {
        return compound.getWeight() * exponent;
    }

}
