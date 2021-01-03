package ca.csf.mobile1.tp1.chemical;

import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;


import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompound;
import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompoundFactory;
import ca.csf.mobile1.tp1.chemical.compound.EmptyFormulaException;
import ca.csf.mobile1.tp1.chemical.compound.EmptyParenthesisException;
import ca.csf.mobile1.tp1.chemical.compound.IllegalCharacterException;
import ca.csf.mobile1.tp1.chemical.compound.MisplacedExponentException;
import ca.csf.mobile1.tp1.chemical.compound.MissingClosingParenthesisException;


import ca.csf.mobile1.tp1.chemical.element.ChemicalElementRepository;

/**
 * Created by Devilclown1607 on 2017-02-21.
 */

public class Main
{
    public static void main(String[] args) throws Exception
    {
        ChemicalElementRepository repository = new ChemicalElementRepository();

        ChemicalCompoundFactory factory = new ChemicalCompoundFactory(repository);
        System.out.println("Entrez un symbole chimique.");

        try
        {
            Scanner reader = new Scanner(System.in);
            String text = reader.nextLine();
            ChemicalCompound chemicalCompound = factory.createFromString(text);

            if(text == "")
            {
                throw new EmptyFormulaException();
            }
            if(text.contains("()"))
            {
                throw new EmptyParenthesisException();
            }
            if(Character.isDigit(text.charAt(0)))
            {
                throw new MisplacedExponentException();
            }
            if((text.contains("(") && !text.contains(")")) ||
                    (text.contains(")") && !text.contains("(")))
            {
                throw new MissingClosingParenthesisException();
            }

            System.out.println(chemicalCompound.getWeight());
        }
        catch (EmptyFormulaException e)
        {
            System.out.println(e.getMessage());
        }


    }
}
