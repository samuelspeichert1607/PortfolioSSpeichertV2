package ca.csf.mobile1.tp1.chemical.compound;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import ca.csf.mobile1.tp1.chemical.element.ChemicalElement;
import ca.csf.mobile1.tp1.chemical.element.ChemicalElementRepository;

import ca.csf.mobile1.tp1.chemical.compound.EmptyFormulaException;
import ca.csf.mobile1.tp1.chemical.compound.EmptyParenthesisException;
import ca.csf.mobile1.tp1.chemical.compound.IllegalCharacterException;
import ca.csf.mobile1.tp1.chemical.compound.MisplacedExponentException;
import ca.csf.mobile1.tp1.chemical.compound.MissingClosingParenthesisException;
import ca.csf.mobile1.tp1.chemical.compound.UnknownChemicalElementException;

/**
 * Created by Samuel Speichert on 2017-02-20.
 */

public class ChemicalCompoundFactory
{
    private ChemicalElementRepository elements;

    /**
     * Constructeur de ChemicalCompoundFactory
     * @param elements : ceci va assigner le "elements" de cette classe au "elements" rentré en paramètre.
     */
    public ChemicalCompoundFactory(ChemicalElementRepository elements)
    {
        this.elements = elements;
    }

    /**
     * Cette fonction va s'occuper de séparer le composant chimique en plusieurs sous-composants pour mieux
     * calculer sa masse par la suite.
     * @param string Le composant chimique rentré au clavier par l'utilisateur.
     * @return Le composant chimique conçu.
     */
    public ChemicalCompound createFromString(String string) throws Exception
    {

        ChemicalElement element = elements.get(string);
        ChemicalCompound chemicalCompound = new ChemicalCompoundBasic(element);

        ChemicalCompound previousChemicalCoumpound = null;

        ArrayList<ChemicalCompound> chemicalCompoundList = new ArrayList<ChemicalCompound>();

        ChemicalCompound chemicalCompoundGroup = null; //= new ChemicalCompoundGroup(chemicalCompoundArrayDeque);
        String elementString = "";

        try
        {


            if(string == "" || string == " ")
            {
                throw new EmptyFormulaException();
            }
            if(string.contains("()"))
            {
                throw new EmptyParenthesisException();
            }
            if(Character.isDigit(string.charAt(0)))
            {
                throw new MisplacedExponentException();
            }
            //if((string.contains(")") && !string.contains("(")))
            //{
            //    throw new IllegalClosingParenthesisException();
            //}
            //if((string.contains("(") && !string.contains(")")))
            //{
            //    throw new MissingClosingParenthesisException();
            //}

            string = string + " ";

            for(int i = 0; i < string.length(); i++)
            {
                //Composant chimique basique

                    if (Character.isUpperCase(string.charAt(i)) == true)
                    {
                        elementString = "";

                        if(i != string.length())
                        {
                                if (Character.isLowerCase(string.charAt(i + 1))) //Si c'est un composant chimique basique de deux caractères
                                {
                                    elementString = string.substring(i, i + 2);
                                    string = string + " ";
                                }
                                else { //Si c'est un composant chimique basique d'un seul caractère
                                    elementString = Character.toString(string.charAt(i));
                                    string = string + " ";
                                }
                        }

                        element = elements.get(elementString);

                        if(element == null)
                        {
                            throw new UnknownChemicalElementException(elementString);
                        }

                        chemicalCompound = new ChemicalCompoundBasic(element);
                        previousChemicalCoumpound = chemicalCompound;
                        chemicalCompoundList.add(chemicalCompound);
                        chemicalCompoundGroup = new ChemicalCompoundGroup(chemicalCompoundList);
                    }
                    else if (Character.isDigit(string.charAt(i)))
                    {
                        elementString = string.substring(i, i + 1);
                        chemicalCompoundList.remove(previousChemicalCoumpound);
                        chemicalCompound = new ChemicalCompoundExponent(previousChemicalCoumpound, Double.parseDouble(elementString));
                        chemicalCompoundList.add(chemicalCompound);
                        chemicalCompoundGroup = new ChemicalCompoundGroup(chemicalCompoundList);
                    }
                    else if(string.charAt(i) == '(')
                    {
                        int rightBracketPosition = 0;
                        for(int j = string.length()-1; j > i; j--)
                        {
                            if(string.charAt(j) == ')')
                            {
                                //(string.substring(i,j).contains(")") == true)
                                //{
                                    rightBracketPosition = j;
                                //    break;
                                //}
                                //else
                                //{
                                    break;
                                //}

                            }
                        }
                        elementString = string.substring(i + 1, rightBracketPosition);
                        chemicalCompound = createFromString(elementString);

                        //chemicalCompoundList.add(chemicalCompound); DÉCOMMENTÉ POUR DE BON
                        //chemicalCompoundGroup = new ChemicalCompoundGroup(chemicalCompoundList); DÉCOMMENTÉ POUR DE BON
                    }
            }
        }
        catch(Exception e)
        {
            System.out.println(e.getMessage());
        }


        return chemicalCompoundGroup;
    }
}
