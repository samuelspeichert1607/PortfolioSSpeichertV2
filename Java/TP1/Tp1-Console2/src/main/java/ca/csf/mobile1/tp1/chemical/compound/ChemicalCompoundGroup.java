package ca.csf.mobile1.tp1.chemical.compound;

import java.util.ArrayList;

/**
 * Created by Samuel Speichert on 2017-02-20.
 */

public class ChemicalCompoundGroup implements ChemicalCompound
{
    private ArrayList<ChemicalCompound> compounds;

    public ChemicalCompoundGroup(ArrayList<ChemicalCompound> compounds)
    {
        this.compounds = compounds;
    }

    /**
     * Fonction qui va retourner la masse du groupe des composés chimique, tout additionnée.
     * @return Poids du composé chimique, multiplié par son exponent.
     */
    public double getWeight()
    {
        double weightOfGroup = 0.0D;
        for (int i = 0; i < compounds.size(); i++)
        {
            weightOfGroup += compounds.get(i).getWeight();
        }
        return weightOfGroup;
    }
}
