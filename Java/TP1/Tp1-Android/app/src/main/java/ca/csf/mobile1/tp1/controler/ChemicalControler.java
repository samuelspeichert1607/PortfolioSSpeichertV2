package ca.csf.mobile1.tp1.controler;

import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompound;
import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompoundFactory;
import ca.csf.mobile1.tp1.chemical.element.ChemicalElementFactory;
import ca.csf.mobile1.tp1.chemical.element.ChemicalElementRepository;
import ca.csf.mobile1.tp1.view.ChemicalCompoundCalculatorConsoleView;

/**
 * Created by Devilclown1607 on 2017-02-28.
 */

public class ChemicalControler implements Listener
{
    private ChemicalElementRepository chemicalElementRepository = null;
    private ChemicalCompoundFactory chemicalCompoundFactory = null;
    private ChemicalCompoundCalculatorConsoleView consoleView = null;

    public ChemicalControler(ChemicalElementRepository repository, ChemicalCompoundCalculatorConsoleView chemicalCompoundCalculatorConsoleView)
    {
        chemicalElementRepository = repository;
        chemicalCompoundFactory = new ChemicalCompoundFactory(chemicalElementRepository);
        consoleView = chemicalCompoundCalculatorConsoleView;
        addListener();
        //consoleView.writeToConsole(consoleView.readFromConsole());
    }

    public void view()
    {
        System.out.println("Entrez un symbole chimique.");
        consoleView.readFromConsolePublic();
    }

    public void addListener()
    {
        consoleView.addListener(this);
    }

    public void onDataEntered()
    {
        try
        {
            ChemicalCompound chemicalCompound = chemicalCompoundFactory.createFromString(consoleView.getInput());
            consoleView.setChemicalCompound(chemicalCompound);
        }
        catch (Exception e)
        {

        }


    }



}
