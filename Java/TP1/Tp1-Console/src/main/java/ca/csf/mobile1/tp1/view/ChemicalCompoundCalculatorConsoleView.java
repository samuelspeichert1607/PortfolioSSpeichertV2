package ca.csf.mobile1.tp1.view;

import java.io.*;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;
import java.util.Scanner;

import ca.csf.mobile1.tp1.chemical.compound.ChemicalCompound;
import ca.csf.mobile1.tp1.controler.Listener;

public class ChemicalCompoundCalculatorConsoleView {

    private final BufferedReader consoleInput;
    private final BufferedWriter consoleOuput;
    private final ArrayList<Listener> listeners = new ArrayList<Listener>();
    private ChemicalCompound chemicalCompound = null;
    public String input = "";

    public ChemicalCompoundCalculatorConsoleView() {
        this.consoleInput = new BufferedReader(new InputStreamReader(System.in));
        this.consoleOuput = new BufferedWriter(new OutputStreamWriter(System.out));

        //writeToConsole(readFromConsole());

    }

    //TODO : À compléter

    public void readFromConsolePublic()
    {
        readFromConsole();
    }

    private String readFromConsole() {
        try
        {
            Scanner reader = new Scanner(System.in);
            String text = reader.nextLine();
            input = text;

            for (Listener listener : listeners) {
                listener.onDataEntered();
            }

            writeToConsole(input);

            return consoleInput.readLine();
        } catch (IOException e) {
            e.printStackTrace();
            return "";
        }
    }

    private void writeToConsole(String string) {
        try {
            System.out.println("Le poids de ce composé chimique est : " + chemicalCompound.getWeight());
            consoleOuput.write(string);
            consoleOuput.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void show() {
        System.out.println("Entrez une formule chimique :");
        readFromConsole();
    }

    public void addListener(Listener listener)
    {
        listeners.add(listener);
    }

    public String getInput()
    {
        return input;
    }
    public void setChemicalCompound(ChemicalCompound chemicalCompound)
    {
        this.chemicalCompound = chemicalCompound;
    }

}