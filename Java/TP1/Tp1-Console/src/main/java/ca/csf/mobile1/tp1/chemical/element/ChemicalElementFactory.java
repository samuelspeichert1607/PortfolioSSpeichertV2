package ca.csf.mobile1.tp1.chemical.element;

import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompoundFactory;

/**
 * Fabrique d'éléments du tableau périodique.
 *
 * @author Benjamin Lemelin
 * @author Daniel Huot
 * @see ChemicalElement
 */
public class ChemicalElementFactory {

    /**
     * Construit un nouveau {@link ChemicalElement} à partir d'une chaine de caractères, formatée ainsi :
     * <br />
     * <br />
     * <code>
     *     NOM, SYMBOLE, NUMÉRO, POIDS
     * </code>
     * <br />
     * <br />
     * Par exemple :
     * <br />
     * <br />
     * <code>
     *     Hydrogene,H,1,1.00794
     * </code>
     * <br />
     * <br />
     * Les 4 parties doivent être séparées par une virgule. <code>NOM</code> et <code>SYMBOLE</code> doivent être une chaine de caractère.
     * <code>NUMÉRO</code> doit être un entier positif et <code>POIDS</code> doit être un nombre décimal compatible avec le type {@link Double}.
     * Si le format est incorrect, une {@link IllegalArgumentException} est lancée.
     * @param string La chaine de caractère à transformer en {@link ChemicalElement}.
     * @return {@link ChemicalElement} créé à partir de la chaine de caractère reçue
     * @throws IllegalArgumentException Lorsque la chaine de caractères ne respecte pas le format demandé.
     */
    public ChemicalElement createFromString(String string) throws Exception {
        //TODO : À partir de la string, créer un ChemicalElement.
        //       Cette String est en 4 parties :
        //              1. Le nom
        //              2. Le symbole
        //              3. Le numéro
        //              4. Le poids
        //      Chaque partie est séparée par une virgule.
        ChemicalElement chemicalElement = null;

        String[] words = string.split(",");
        chemicalElement = new ChemicalElement(words[0], words[1], Integer.parseInt(words[2]), Double.parseDouble(words[3]));

        if(words.length != 4)
        {
            throw new IllegalArgumentException();
        }
        if(string == " ")
        {
            throw new IllegalArgumentException();
        }
        if(string == null)
        {
            throw new IllegalArgumentException();
        }
        if(string == "")
        {
            throw new IllegalArgumentException();
        }

        return chemicalElement;
    }

}
